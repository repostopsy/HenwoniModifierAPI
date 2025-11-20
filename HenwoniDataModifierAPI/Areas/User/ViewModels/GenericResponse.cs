namespace HenwoniDataModifierAPI.Areas.User.ViewModels
{
    public class GenericResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public string ErrorName { get; set; }
        public List<string> Errors { get; set; }
        public string Redirect { get; set; }
    }
}
