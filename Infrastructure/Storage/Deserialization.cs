using Newtonsoft.Json;
using Provider = Editor.CustomizedConverterProvider;

namespace Editor
{
    public static class Deserialization
    {
        public static List<PrimitiveTemplate> Deserialize(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentException("File is empty");

            var result = JsonConvert.DeserializeObject<List<PrimitiveTemplate>>(json, Provider.Settings);
            return result ?? throw new ArgumentNullException("No compulsory data in this JSON");
        }

        public static List<PrimitiveTemplate> LoadFromFile(string filePath)
        {
            var result = new List<PrimitiveTemplate>();
            string json = File.ReadAllText(filePath);
            return Deserialize(json);
        }
    }
}