﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'System.Text.Json' then do:
//
//    using QuickType;
//
//    var CSCDCountry = CSCDCountry.FromJson(jsonString);
#nullable enable
#pragma warning disable CS8618
#pragma warning disable CS8601
#pragma warning disable CS8603

namespace HenwoniDataModifierAPI.Data.External.CountriesStatesCitiesDatabase
{
	using System;
	using System.Collections.Generic;

	using System.Text.Json;
	using System.Text.Json.Serialization;
	using System.Globalization;

	public partial class CSCDCountry
	{
		[JsonPropertyName("id")]
		public long Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("iso3")]
		public string ISO3 { get; set; }

		[JsonPropertyName("iso2")]
		public string ISO2 { get; set; }

		[JsonPropertyName("numeric_code")]
		public string NumericCode { get; set; }

		[JsonPropertyName("phone_code")]
		public string PhoneCode { get; set; }

		[JsonPropertyName("capital")]
		public string Capital { get; set; }

		[JsonPropertyName("currency")]
		public string Currency { get; set; }

		[JsonPropertyName("currency_name")]
		public string CurrencyName { get; set; }

		[JsonPropertyName("currency_symbol")]
		public string CurrencySymbol { get; set; }

		[JsonPropertyName("tld")]
		public string Tld { get; set; }

		[JsonPropertyName("native")]
		public string Native { get; set; }

		[JsonPropertyName("region")]
		public string Region { get; set; }
		// public Region Region { get; set; }

		[JsonPropertyName("subregion")]
		public string Subregion { get; set; }

		[JsonPropertyName("nationality")]
		public string Nationality { get; set; }

		[JsonPropertyName("timezones")]
		public Timezone[] TimeZones { get; set; }

		[JsonPropertyName("translations")]
		public Translations Translations { get; set; }

		[JsonPropertyName("latitude")]
		public string Latitude { get; set; }

		[JsonPropertyName("longitude")]
		public string Longitude { get; set; }

		[JsonPropertyName("emoji")]
		public string Emoji { get; set; }

		[JsonPropertyName("emojiU")]
		public string EmojiU { get; set; }

		[JsonPropertyName("states")]
		public State[] States { get; set; }
	}

	public partial class State
	{
		[JsonPropertyName("id")]
		public long Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("state_code")]
		public string StateCode { get; set; }

		[JsonPropertyName("latitude")]
		public string Latitude { get; set; }

		[JsonPropertyName("longitude")]
		public string Longitude { get; set; }

		[JsonPropertyName("type")]
		public string Type { get; set; }

		[JsonPropertyName("cities")]
		public City[] Cities { get; set; }
	}

	public partial class City
	{
		[JsonPropertyName("id")]
		public long Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("latitude")]
		public string Latitude { get; set; }

		[JsonPropertyName("longitude")]
		public string Longitude { get; set; }
	}

	public partial class Timezone
	{
		[JsonPropertyName("zoneName")]
		public string ZoneName { get; set; }

		[JsonPropertyName("gmtOffset")]
		public long GmtOffset { get; set; }

		[JsonPropertyName("gmtOffsetName")]
		public string GmtOffsetName { get; set; }

		[JsonPropertyName("abbreviation")]
		public string Abbreviation { get; set; }

		[JsonPropertyName("tzName")]
		public string TzName { get; set; }
	}

	public partial class Translations
	{
		[JsonPropertyName("kr")]
		public string Kr { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[JsonPropertyName("pt-BR")]
		public string PtBr { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[JsonPropertyName("pt")]
		public string Pt { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[JsonPropertyName("nl")]
		public string Nl { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[JsonPropertyName("hr")]
		public string Hr { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[JsonPropertyName("fa")]
		public string Fa { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[JsonPropertyName("de")]
		public string De { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[JsonPropertyName("es")]
		public string Es { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[JsonPropertyName("fr")]
		public string Fr { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[JsonPropertyName("ja")]
		public string Ja { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[JsonPropertyName("it")]
		public string It { get; set; }

		[JsonPropertyName("cn")]
		public string Cn { get; set; }

		[JsonPropertyName("tr")]
		public string Tr { get; set; }
	}

	public enum Region { Africa, Americas, Asia, Empty, Europe, Oceania, Polar };

	public partial class CSCDCountry
	{
		public static CSCDCountry[] FromJson(string json) => JsonSerializer.Deserialize<CSCDCountry[]>(json, HenwoniDataModifierAPI.Data.External.CountriesStatesCitiesDatabase.Converter.Settings);
	}

	public static class Serialize
	{
		public static string ToJson(this CSCDCountry[] self) => JsonSerializer.Serialize(self, HenwoniDataModifierAPI.Data.External.CountriesStatesCitiesDatabase.Converter.Settings);
	}

	internal static class Converter
	{
		public static readonly JsonSerializerOptions Settings = new(JsonSerializerDefaults.General)
		{
			Converters =
			{
				RegionConverter.Singleton,
				new DateOnlyConverter(),
				new TimeOnlyConverter(),
				IsoDateTimeOffsetConverter.Singleton
			},
		};
	}

	internal class RegionConverter : JsonConverter<Region>
	{
		public override bool CanConvert(Type t) => t == typeof(Region);

		public override Region Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var value = reader.GetString();
			switch (value)
			{
				case "":
					return Region.Empty;
				case "Africa":
					return Region.Africa;
				case "Americas":
					return Region.Americas;
				case "Asia":
					return Region.Asia;
				case "Europe":
					return Region.Europe;
				case "Oceania":
					return Region.Oceania;
				case "Polar":
					return Region.Polar;
			}
			throw new Exception("Cannot unmarshal type Region");
		}

		public override void Write(Utf8JsonWriter writer, Region value, JsonSerializerOptions options)
		{
			switch (value)
			{
				case Region.Empty:
					JsonSerializer.Serialize(writer, "", options);
					return;
				case Region.Africa:
					JsonSerializer.Serialize(writer, "Africa", options);
					return;
				case Region.Americas:
					JsonSerializer.Serialize(writer, "Americas", options);
					return;
				case Region.Asia:
					JsonSerializer.Serialize(writer, "Asia", options);
					return;
				case Region.Europe:
					JsonSerializer.Serialize(writer, "Europe", options);
					return;
				case Region.Oceania:
					JsonSerializer.Serialize(writer, "Oceania", options);
					return;
				case Region.Polar:
					JsonSerializer.Serialize(writer, "Polar", options);
					return;
			}
			throw new Exception("Cannot marshal type Region");
		}

		public static readonly RegionConverter Singleton = new RegionConverter();
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
#pragma warning restore CS8618
#pragma warning restore CS8601
#pragma warning restore CS8603
