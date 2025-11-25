namespace eCommerce.Models
{
    public class RegisterUser
    {
        public required string Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? ProfileImage { get; set; }
        public string? Role { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public bool TermsAccepted { get; set; }
    }
}
