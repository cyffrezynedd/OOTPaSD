using System.Reflection;

namespace Editor
{
    public static class PrimitiveFactory
    {
        private static readonly Dictionary<string, ConstructorInfo> constructors;
        private static readonly Func<ParameterInfo[], object[]> parametersResolver = default!;

        static PrimitiveFactory()
        {
            constructors = new Dictionary<string, ConstructorInfo>();

            Assembly assembly = Assembly.GetExecutingAssembly();
            var primitiveTypes = assembly.GetTypes()
                .Where(t => !t.IsAbstract && typeof(PrimitiveTemplate).IsAssignableFrom(t));

            foreach (var type in primitiveTypes)
            {
                var c = type.GetConstructors();
                ConstructorInfo target = c.FirstOrDefault(constructor =>
                {
                    ParameterInfo[] parameters = constructor.GetParameters();
                    return parameters.Length >= 2 &&
                           parameters[0].ParameterType == typeof(PrimitiveStyle) &&
                           parameters[1].ParameterType == typeof(Point);
                }) ?? c.OrderBy(constructor => constructor.GetParameters().Length).First();

                string key = type.Name;
                constructors[key] = target;
                parametersResolver = GetResolver();
            }
        }

        public static PrimitiveTemplate CreateInstance(string typeName)
        {
            if (!constructors.ContainsKey(typeName))
                throw new ArgumentException($"{typeName} not found in fabric method");

            ConstructorInfo c = constructors[typeName];
            ParameterInfo[] paramInfo = c.GetParameters();
            var parameters = parametersResolver(paramInfo);

            try
            {
                object instance = c.Invoke(parameters);
                return instance as PrimitiveTemplate
                    ?? throw new InvalidOperationException("Not a PrimitiveTemplate");
            }
            catch (TargetInvocationException ex)
            {
                throw new InvalidOperationException("Error while creating instance", ex);
            }
        }

        private static object? GetValueByType<T>(this T instance, Type targetType) where T : struct
        {
            var property = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(p => p.PropertyType == targetType);
            if (property != null)
            {
                return property.GetValue(instance);
            }

            return null;
        }

        private static Func<ParameterInfo[], object[]> GetResolver()
        {
            return (ParameterInfo[] constructorParams) =>
            {
                List<object> finalParameters = new List<object>();
                foreach (var paramInfo in constructorParams)
                {
                    object? value = ParametersManager.CurrentParameters.GetValueByType(paramInfo.ParameterType);
                    if (value == null)
                    {
                        throw new ArgumentException($"No such value for {paramInfo.ParameterType.FullName}");
                    } 
                    finalParameters.Add(value);
                }
                return finalParameters.ToArray();
            };
        }
    }
}
