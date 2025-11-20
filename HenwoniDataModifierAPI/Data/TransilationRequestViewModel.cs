using HenwoniDataModifierAPI.Models.Location;
using HenwoniDataModifierAPI.Models;

namespace HenwoniDataModifierAPI.Areas.User.ViewModels
{
    public class TransilationRequestViewModel
    {
        public string? Title { get; set; }
        public string? Excerpt { get; set; }
        public string Text { get; set; }
        public string? Language { get; set; }
        public long? ServerId { get; set; }
        public long? ServerParentId { get; set; }
    }
}
