using Newtonsoft.Json;
using Provider = Editor.CustomizedConverterProvider;

namespace Editor
{    
    public static class Serialization
    {
        private static string Serialize(PrimitiveCollection collection)
        {
            return JsonConvert.SerializeObject(((IAccessor)collection).Primitives,
                Formatting.Indented, Provider.Settings);
        }

        public static void SaveToFile(PrimitiveCollection collection, string filePath)
        {
            string json = Serialize(collection);
            File.WriteAllText(filePath, json);
        }
    }
}
