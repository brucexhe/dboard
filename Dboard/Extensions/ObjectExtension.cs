using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Dboard
{
    public static class ObjectExtension
    {
        static JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public static string ToJson(this Object obj)
        {
            if (obj == null)
            {
                return "null";
            }
            return JsonSerializer.Serialize(obj, options);
        }
    }
}
