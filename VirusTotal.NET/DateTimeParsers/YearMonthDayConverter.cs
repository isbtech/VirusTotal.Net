using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VirusTotalNET.Exceptions;

namespace VirusTotalNET.DateTimeParsers
{
    public class YearMonthDayConverter : DateTimeConverterBase
    {
        private readonly CultureInfo _culture = new CultureInfo("en-us");
        private const string DateFormatString = "yyyyMMdd";

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.DateFormatString = DateFormatString;
            writer.WriteValue(value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return DateTime.MinValue;

            if (!(reader.Value is string))
                throw new InvalidDateTimeException("Invalid datetime from VirusTotal. Tried to parse: " + reader.Value);

            string value = (string)reader.Value;

            DateTime result;
            if (DateTime.TryParseExact(value, DateFormatString, _culture, DateTimeStyles.AllowWhiteSpaces, out result))
                return result;

            throw new InvalidDateTimeException("Invalid datetime from VirusTotal. Tried to parse: " + value);
        }
    }
}