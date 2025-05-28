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

            try
            {
                var result = JsonConvert.DeserializeObject<List<PrimitiveTemplate>>(json, Provider.Settings);
                if (result == null || result.Count == 0)
                    throw new JsonException("No compulsory data in JSON");

                return result;
            }
            catch (JsonException ex)
            {
                throw new JsonException(ex.Message);
            }
        }

        public static List<PrimitiveTemplate> LoadFromFile(string filePath)
        {
            var result = new List<PrimitiveTemplate>();
            string json = File.ReadAllText(filePath);
            try
            {
                return Deserialize(json);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (JsonException ex)
            {
                throw new JsonException(ex.Message);
            }
        }
    }
}