﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HenwoniDataModifierAPI.Data.External.Languages
{
	public class ExLanguage
	{
		[JsonPropertyName("code")]
		public string Code { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }
	}
}
