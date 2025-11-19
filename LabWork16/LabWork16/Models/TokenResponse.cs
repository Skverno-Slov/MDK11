namespace LabWork16.Models
{
    public class TokenResponse
    {
        public string Token { get; set; } = null!;
        public string? RefreshToken { get; set; }
    }
}
