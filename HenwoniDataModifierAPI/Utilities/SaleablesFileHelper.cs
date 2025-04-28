using System.Text.RegularExpressions;

namespace HenwoniDataModifierAPI.Utilities
{
	public static class SaleablesFileHelper
	{
		public  static string CleanFileName(string filename)
		{
			// return Regex.Replace(input, "<.*?>", String.Empty);
			string extension = Path.GetExtension(filename);

			// Find last occurance
			int place = filename.LastIndexOf(extension);
			string str1 = filename.Remove(place, extension.Length).Insert(place, "");

			Regex reg = new("[.*'\",_&#^@]");
			str1 = reg.Replace(str1, "_");
			return str1 + extension;

		}
	}
}
