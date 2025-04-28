using System.Text.RegularExpressions;

namespace HenwoniDataModifierAPI.Utilities
{
	public static class Extensions
	{
		public static List<TType> Concat<TType>(params List<TType>[] lists)
		{
			var result = lists.Aggregate(new List<TType>(), (x, y) => x.Concat(y).ToList());
			return result;
		}
		public static void AddToFront<T>(this List<T> list, T item)
		{
			// omits validation, etc.
			list.Insert(0, item);
		}

		public static string GenerateSlug(this string phrase)
		{
			string str = phrase.RemoveAccent().ToLower();
			// invalid chars           
			str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
			// convert multiple spaces into one space   
			str = Regex.Replace(str, @"\s+", " ").Trim();
			// cut and trim 
			str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
			str = Regex.Replace(str, @"\s", "-"); // hyphens   
			return str;
		}

		public static string ReplaceLastOccurrence(this string source, string find, string replace)
		{
			int place = source.LastIndexOf(find);

			if (place == -1)
				return source;

			return source.Remove(place, find.Length).Insert(place, replace);
		}

		public static string RemoveAccent(this string txt)
		{
			byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
			return System.Text.Encoding.ASCII.GetString(bytes);
		}

		public static string StripHTML(this string input)
		{
			return Regex.Replace(input, "<.*?>", String.Empty);
		}

		public static string? Truncate(this string? value, int maxLength, string truncationSuffix = "…")
		{
			return value?.Length > maxLength
				? value.Substring(0, maxLength) + truncationSuffix
				: value;
		}

		private static Random random = new Random();
		public static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

	}
}
