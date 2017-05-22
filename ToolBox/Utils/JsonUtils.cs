using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ToolBox.Utils
{
    public static class JsonUtils
    {
        public static void Serialize<T>(TextWriter writer, T value)
        {
            writer.Required();

            var jsonWriter = new JsonTextWriter(writer);

            GetSerializer().Serialize(jsonWriter, value, typeof(T));

            writer.Flush();
        }

        public static string Serialize<T>(T value)
        {
            using (var writer = new StringWriter())
            {
                Serialize(writer, value);
                return writer.ToString();
            }
        }

        public static T Deserialize<T>(TextReader reader)
        {
            reader.Required();

            var jsonReader = new JsonTextReader(reader);

            try
            {
                return GetSerializer().Deserialize<T>(jsonReader);
            }
            catch (JsonSerializationException e)
            {
                throw new SerializationException(e.Message, e);
            }
        }

        public static T Deserialize<T>(string serializedObject)
        {
            serializedObject.Required();

            using (var reader = new StringReader(serializedObject))
            {
                return Deserialize<T>(reader);
            }
        }

        private static JsonSerializer GetSerializer()
        {
            return JsonSerializer.Create(new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
            });
        }
    }
}
