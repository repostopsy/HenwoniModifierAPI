using System.IO;
using Microsoft.AspNetCore.Http;

namespace HenwoniDataModifierAPI.Utilities
{
    public class FileHelper
    {
        public string GetFileExtension(IFormFile file)
        {
            if (file == null) return null;
            string fileName = file.FileName; // Get the file name
            string extension = Path.GetExtension(fileName); // Extract the extension
            return extension;
        }
    }
}
