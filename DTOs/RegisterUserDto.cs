namespace eCommerce.DTOs
{
    public class RegisterUserDto
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? ProfileImage { get; set; }
        public string? Role { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public bool TermsAccepted { get; set; }
    }
}
