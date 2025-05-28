using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Editor
{
    public class CustomizedConverter<T> : JsonConverter<T>
        where T : PrimitiveTemplate
    {
        private readonly Func<JsonReader, JsonSerializer, T> funcRead;
        private readonly Action<JsonWriter, T, JsonSerializer> funcWrite;

        public CustomizedConverter(
            Func<JsonReader, JsonSerializer, T> newFuncRead,
            Action<JsonWriter, T, JsonSerializer> newFuncWrite)
        {
            funcRead = newFuncRead;
            funcWrite = newFuncWrite;
        }

        public override T ReadJson(JsonReader reader, Type objectType, T? value, bool hasExistingValue, JsonSerializer serializer)
        {
            return funcRead(reader, serializer);
        }

        public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
        {
            if (value == null) return;
            funcWrite(writer, value, serializer);
        }
    }

    public static class CustomizedConverterProvider
    {
        public static JsonConverter CustomizedConverter { get; }
        public static JsonSerializerSettings Settings { get; }

        static CustomizedConverterProvider()
        {
            Settings = new JsonSerializerSettings
            {
                ContractResolver = new CustomizedContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            };

            CustomizedConverter = new CustomizedConverter<PrimitiveTemplate>
            (
                (JsonReader reader, JsonSerializer serializer) =>
                {
                    JObject jo = JObject.Load(reader);
                    string? typeName = jo["ObjectType"]?.ToString();
                    if (typeName is null)
                        throw new JsonException("No such field 'ObjectType' in JSON object");

                    Type? objectType = AppDomain.CurrentDomain.GetAssemblies()
                        .SelectMany(asm => asm.GetTypes())
                        .FirstOrDefault(t => t.Name.Equals(typeName, StringComparison.Ordinal));
                    if (objectType is null)
                        throw new ArgumentNullException($"No such type {typeName} in current domain");

                    var tempSerializer = new JsonSerializer
                    {
                        ContractResolver = serializer.ContractResolver,
                        Formatting = serializer.Formatting
                    };

                    PrimitiveTemplate? objectInstance = jo["ObjectInstance"]?.ToObject(objectType, tempSerializer) as PrimitiveTemplate;
                    if (objectInstance is null)
                        throw new ArgumentNullException($"Can not create an instance of such type {typeName}");

                    return objectInstance;
                },
                (JsonWriter writer, PrimitiveTemplate value, JsonSerializer serializer) =>
                {
                    var tempSerializer = new JsonSerializer
                    {
                        ContractResolver = serializer.ContractResolver,
                        Formatting = serializer.Formatting,
                    };

                    JObject objectInstance = JObject.FromObject(value, tempSerializer);

                    JObject jo = new JObject
                    {
                        ["ObjectType"] = value.GetType().Name,
                        ["ObjectInstance"] = objectInstance
                    };
                    jo.WriteTo(writer);
                }
            );

            Settings.Converters.Add(CustomizedConverter);
        }
    }       
}