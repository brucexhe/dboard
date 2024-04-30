using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dboard.Handlers
{
    /// <summary>
	/// 设置Json默认DateTime格式化
	/// </summary>
	public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        private readonly string Format;
        public DateTimeJsonConverter(string format)
        {
            Format = format;
        }
        public override void Write(Utf8JsonWriter writer, DateTime date, JsonSerializerOptions options)
        {
            writer.WriteStringValue(date.ToString(Format));
        }
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), Format, null);
        }
    }
}
