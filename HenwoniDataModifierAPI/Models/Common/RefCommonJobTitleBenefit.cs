
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using HenwoniDataModifierAPI.Models;
using HenwoniDataModifierAPI.Models.Employment;
using HenwoniDataModifierAPI.Models.Location;

namespace HenwoniDataModifierAPI.Common.Models
{
	public class RefCommonJobTitleBenefit : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		[Key]
		public long Id { get; set; }
		public string SystemName { get; set; }
		public string? Code { get; set; }
		public string Title { get; set; }
		public string? Excerpt { get; set; }

		private string _description;
		public string? Description
		{
			get { return _description; }
			set
			{
				_description = value;
				OnPropertyChanged("SourceId");
			}
        }
        public virtual ApplicationUser? Author { get; set; }
        public bool Approved { get; set; }
        public double Rating { get; set; }
        [JsonIgnore]
        public virtual Language? Language { get; set; }
        public virtual RefCommonJobTitleBenefit? Parent { get; set; }
    }
}
