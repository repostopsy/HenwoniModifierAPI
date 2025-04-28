using System.Text;

namespace HenwoniDataModifierAPI.Utilities
{
	public static class StringHelper
	{
		public static string GenerateRandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var random = new Random();
			var randomString = new StringBuilder();

			for (int i = 0; i < length; i++)
			{
				randomString.Append(chars[random.Next(chars.Length)]);
			}

			return randomString.ToString();
		}

		public static string GenerateRandomString2(int length)
		{
			string guidString = Guid.NewGuid().ToString("N");
			return guidString.Substring(0, length);
		}
	}
}
