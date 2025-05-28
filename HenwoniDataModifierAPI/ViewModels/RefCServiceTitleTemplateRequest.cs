using HenwoniDataModifierAPI.Models.Location;
using HenwoniDataModifierAPI.Models.Services.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HenwoniDataModifierAPI.ViewModels
{
    public class RefCServiceTitleTemplateRequest
    {
        public RefCServiceTitleTemplateRequest() { }

        public string? Parent { get; set; }
        public string SystemName { get; set; }
        public string Title { get; set; }
        public string? Excerpt { get; set; }
        public string Template { get; set; }
        public string? Notes { get; set; }
        public string RefCServiceTitle { get; set; }
        public string Language { get; set; }
        public virtual List<string> Tags { get; set; }
        public virtual List<string> Aliases { get; set; }
    }
}
