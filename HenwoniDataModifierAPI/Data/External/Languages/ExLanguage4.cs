namespace HenwoniDataModifierAPI.Data.External.Languages.NExLanguage4
{
    using System;
    using System.Collections.Generic;

    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Globalization;

    public partial class ExLanguage4
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("charset")]
        public string Charset { get; set; }
    }

    public enum Charset { Armscii8, CharsetIso88591, Iso88591, Iso885913, Iso885915, Iso88592, Iso88593, Iso88595, Iso88596, Iso88598, Iso88599, Koi8U, Tis620, U13A0, Utf8 };

    public partial class ExLanguage4
    {
        public static Dictionary<string, ExLanguage4> FromJson(string json) => JsonSerializer.Deserialize<Dictionary<string, ExLanguage4>>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Dictionary<string, ExLanguage4> self) => JsonSerializer.Serialize(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerOptions Settings = new(JsonSerializerDefaults.General)
        {
            Converters =
            {
                CharsetConverter.Singleton,
                new DateOnlyConverter(),
                new TimeOnlyConverter(),
                IsoDateTimeOffsetConverter.Singleton
            },
        };
    }

    internal class CharsetConverter : JsonConverter<Charset>
    {
        public override bool CanConvert(Type t) => t == typeof(Charset);

        public override Charset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            switch (value)
            {
                case "ARMSCII-8":
                    return Charset.Armscii8;
                case "ISO-8859-1":
                    return Charset.CharsetIso88591;
                case "ISO-8859-13":
                    return Charset.Iso885913;
                case "ISO-8859-15":
                    return Charset.Iso885915;
                case "ISO-8859-2":
                    return Charset.Iso88592;
                case "ISO-8859-3":
                    return Charset.Iso88593;
                case "ISO-8859-5":
                    return Charset.Iso88595;
                case "ISO-8859-6":
                    return Charset.Iso88596;
                case "ISO-8859-8":
                    return Charset.Iso88598;
                case "ISO-8859-9":
                    return Charset.Iso88599;
                case "KOI8-U":
                    return Charset.Koi8U;
                case "TIS-620":
                    return Charset.Tis620;
                case "U+13A0":
                    return Charset.U13A0;
                case "UTF-8":
                    return Charset.Utf8;
                case "iso-8859-1":
                    return Charset.Iso88591;
            }
            throw new Exception("Cannot unmarshal type Charset");
        }

        public override void Write(Utf8JsonWriter writer, Charset value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case Charset.Armscii8:
                    JsonSerializer.Serialize(writer, "ARMSCII-8", options);
                    return;
                case Charset.CharsetIso88591:
                    JsonSerializer.Serialize(writer, "ISO-8859-1", options);
                    return;
                case Charset.Iso885913:
                    JsonSerializer.Serialize(writer, "ISO-8859-13", options);
                    return;
                case Charset.Iso885915:
                    JsonSerializer.Serialize(writer, "ISO-8859-15", options);
                    return;
                case Charset.Iso88592:
                    JsonSerializer.Serialize(writer, "ISO-8859-2", options);
                    return;
                case Charset.Iso88593:
                    JsonSerializer.Serialize(writer, "ISO-8859-3", options);
                    return;
                case Charset.Iso88595:
                    JsonSerializer.Serialize(writer, "ISO-8859-5", options);
                    return;
                case Charset.Iso88596:
                    JsonSerializer.Serialize(writer, "ISO-8859-6", options);
                    return;
                case Charset.Iso88598:
                    JsonSerializer.Serialize(writer, "ISO-8859-8", options);
                    return;
                case Charset.Iso88599:
                    JsonSerializer.Serialize(writer, "ISO-8859-9", options);
                    return;
                case Charset.Koi8U:
                    JsonSerializer.Serialize(writer, "KOI8-U", options);
                    return;
                case Charset.Tis620:
                    JsonSerializer.Serialize(writer, "TIS-620", options);
                    return;
                case Charset.U13A0:
                    JsonSerializer.Serialize(writer, "U+13A0", options);
                    return;
                case Charset.Utf8:
                    JsonSerializer.Serialize(writer, "UTF-8", options);
                    return;
                case Charset.Iso88591:
                    JsonSerializer.Serialize(writer, "iso-8859-1", options);
                    return;
            }
            throw new Exception("Cannot marshal type Charset");
        }

        public static readonly CharsetConverter Singleton = new CharsetConverter();
    }

    public class DateOnlyConverter : JsonConverter<DateOnly>
    {
        private readonly string serializationFormat;
        public DateOnlyConverter() : this(null) { }

        public DateOnlyConverter(string? serializationFormat)
        {
            this.serializationFormat = serializationFormat ?? "yyyy-MM-dd";
        }

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return DateOnly.Parse(value!);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
                => writer.WriteStringValue(value.ToString(serializationFormat));
    }

    public class TimeOnlyConverter : JsonConverter<TimeOnly>
    {
        private readonly string serializationFormat;

        public TimeOnlyConverter() : this(null) { }

        public TimeOnlyConverter(string? serializationFormat)
        {
            this.serializationFormat = serializationFormat ?? "HH:mm:ss.fff";
        }

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return TimeOnly.Parse(value!);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
                => writer.WriteStringValue(value.ToString(serializationFormat));
    }

    internal class IsoDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override bool CanConvert(Type t) => t == typeof(DateTimeOffset);

        private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

        private DateTimeStyles _dateTimeStyles = DateTimeStyles.RoundtripKind;
        private string? _dateTimeFormat;
        private CultureInfo? _culture;

        public DateTimeStyles DateTimeStyles
        {
            get => _dateTimeStyles;
            set => _dateTimeStyles = value;
        }

        public string? DateTimeFormat
        {
            get => _dateTimeFormat ?? string.Empty;
            set => _dateTimeFormat = (string.IsNullOrEmpty(value)) ? null : value;
        }

        public CultureInfo Culture
        {
            get => _culture ?? CultureInfo.CurrentCulture;
            set => _culture = value;
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            string text;


            if ((_dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal
                    || (_dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
            {
                value = value.ToUniversalTime();
            }

            text = value.ToString(_dateTimeFormat ?? DefaultDateTimeFormat, Culture);

            writer.WriteStringValue(text);
        }

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? dateText = reader.GetString();

            if (string.IsNullOrEmpty(dateText) == false)
            {
                if (!string.IsNullOrEmpty(_dateTimeFormat))
                {
                    return DateTimeOffset.ParseExact(dateText, _dateTimeFormat, Culture, _dateTimeStyles);
                }
                else
                {
                    return DateTimeOffset.Parse(dateText, Culture, _dateTimeStyles);
                }
            }
            else
            {
                return default(DateTimeOffset);
            }
        }


        public static readonly IsoDateTimeOffsetConverter Singleton = new IsoDateTimeOffsetConverter();
    }
}
