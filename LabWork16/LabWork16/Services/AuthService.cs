using LabWork16.Contexts;
using LabWork16.Models;
using Microsoft.IdentityModel.Tokens;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace LabWork16.Service
{
    public class AuthServices
    {
        private readonly string _secretKey = "12345678123456781234567812345678";

        private readonly string _issuer = "LabWork16";
        private readonly string _audience = "myapp-users";

        public string GenerateToken(CinemaUser user, CinemaDbContext context)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var roleName = context.CinemaUserRoles.FirstOrDefault(r => r.RoleId == user.RoleId).Name;

            var claims = new Claim[]
            {
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(ClaimTypes.Name, user.Login),
            new(ClaimTypes.Role, roleName),
            };

            var token = new JwtSecurityToken(
                signingCredentials: credentials,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                issuer: _issuer,
                audience: _audience
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumbers = new byte[32];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(randomNumbers);
            return Convert.ToBase64String(randomNumbers);
        }

        public bool IsValidToken(string token)
        {
            try
            {
                var tokenParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
                    ValidateLifetime = true,

                    ValidateIssuer = true,
                    ValidIssuer = _issuer,
                    ValidateAudience = true,
                    ValidAudience = _audience,

                };

                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, tokenParameters, out SecurityToken validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string HashPassword(string password)
            => BCrypt.Net.BCrypt.EnhancedHashPassword(password, 5, BCrypt.Net.HashType.SHA512);

        public bool VerifyPassword(string password, string hashedPassword)
            => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword, BCrypt.Net.HashType.SHA512);

        public bool IsUserExists(string login, CinemaDbContext context)
            => context.CinemaUsers.Any(u => u.Login == login);
    }
}
