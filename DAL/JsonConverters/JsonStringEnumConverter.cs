using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace DAL.JsonConverters
{
    public class JsonStringEnumConverter : JsonConverter<JobStatus>
    {
        public override JobStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();

            if (Enum.TryParse<JobStatus>(value, true, out JobStatus result))
            {
                return result;
            }

            throw new JsonException($"Unable to convert \"{value}\" to JobStatus enum.");
        }

        public override void Write(Utf8JsonWriter writer, JobStatus value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
