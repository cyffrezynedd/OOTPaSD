using System.Reflection;

namespace Editor
{
    public static class PrimitiveFactory
    {
        private static readonly Dictionary<string, ConstructorInfo> constructors;
        private static readonly Dictionary<string, ParameterInfo[]> constructorParams;

        static PrimitiveFactory()
        {
            constructors = new Dictionary<string, ConstructorInfo>();
            constructorParams = new Dictionary<string, ParameterInfo[]>();

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
                constructorParams[key] = target.GetParameters();
            }
        }


        public static ParameterInfo[] GetConstructorParameters(string typeName)
        {
            if (!constructorParams.ContainsKey(typeName))
                throw new ArgumentException($"{typeName} not found in fabric method");
            return constructorParams[typeName];
        }

        public static PrimitiveTemplate CreateInstance(string typeName, params object[] parameters)
        {
            if (!constructors.ContainsKey(typeName))
                throw new ArgumentException($"{typeName} not found in fabric method");

            ConstructorInfo c = constructors[typeName];
            ParameterInfo[] paramInfo = c.GetParameters();

            if (paramInfo.Length != parameters.Length)
                if (typeName.Equals("Polygon", StringComparison.OrdinalIgnoreCase) &&
                    paramInfo.Length == 3 && parameters.Length == 2)
                {
                    object[] newParams = new object[3];
                    newParams[0] = parameters[0];
                    newParams[1] = parameters[1];
                    newParams[2] = ParametersManager.GetAngleCount();

                    parameters = newParams;
                }
                else
                {
                    throw new ArgumentException($"Invalid number of parameters. Expected {paramInfo.Length} but got {parameters.Length}.");
                }

            for (int i = 0; i < paramInfo.Length; i++)
            {
                if (parameters[i] != null && !paramInfo[i].ParameterType.IsAssignableFrom(parameters[i].GetType()))
                    throw new ArgumentException($"Invalid parameter type");
            }

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
    }
}
