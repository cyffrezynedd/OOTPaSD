using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Editor
{
    public class CustomizedValueProvider : IValueProvider
    {
        private readonly FieldInfo fieldInfo;
        public CustomizedValueProvider(FieldInfo newFieldInfo)
        {
            fieldInfo = newFieldInfo;
        }

        public object? GetValue(object target) => fieldInfo.GetValue(target);

        public void SetValue(object target, object? value) => fieldInfo.SetValue(target, value);
    }

    public class CustomizedContractResolver : DefaultContractResolver
    {
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            if (objectType is null)
                throw new ArgumentNullException(nameof(objectType));

            BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            var fields = objectType.GetFields(flags).Cast<MemberInfo>();
            var properties = objectType.GetProperties(flags).Cast<MemberInfo>();
            return fields.Concat(properties).ToList();
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (member is PropertyInfo pi)
            {
                if (!pi.CanWrite || pi.GetSetMethod(true) == null)
                {
                    FieldInfo? backingField = pi.DeclaringType?.GetField
                        ($"<{pi.Name}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);

                    if (backingField != null)
                    {
                        property.Writable = true;
                        property.ValueProvider = new CustomizedValueProvider(backingField);
                    }
                }
                else if (pi.GetSetMethod(true) != null)
                {
                    property.Writable = true;
                }
            }
            else if (member is FieldInfo fi)
            {
                property.Writable = true;
            }

            return property;
        }
    }
}
