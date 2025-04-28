

using System.ComponentModel.DataAnnotations;

namespace HenwoniDataModifierAPI.Models.Location
{
	public interface ILocation
	{
		[Key]
		public long Id { get; set; }
		public string Name { get; set; }
		public string SystemName { get; set; }
	}
}
