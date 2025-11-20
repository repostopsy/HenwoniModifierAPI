namespace HenwoniDataModifierAPI.Areas.User.ViewModels
{
    public class AuthResponseViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public Guid? SafoyePublicId { get; set; }
        public string CryptKey { get; internal set; }
    }
}
