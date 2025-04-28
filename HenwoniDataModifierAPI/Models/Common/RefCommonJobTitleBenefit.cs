
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
	}
}
