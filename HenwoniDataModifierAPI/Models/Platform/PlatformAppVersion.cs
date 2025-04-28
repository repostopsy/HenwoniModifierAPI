using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenwoniDataModifierAPI.Models.Platform
{
	public class PlatformAppVersion
	{
		public long Id { get; set; }
		public double Version { get; set; }
		public string FilePath { get; set; }
		public string VersionName { get; set; }
		public DateTime ReleaseDate { get; set; }
		public bool IsDeleted { get; set; } = false;
	}
}
