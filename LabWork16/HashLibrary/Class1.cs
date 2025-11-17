using System.Security.Cryptography;
using System.Text;

namespace AuthLibrary
{
    public class AuthService
    {
        const string salt = "TRollFace";
        var saltedPassword = password + salt;
        byte[] bytes = Encoding.UTF8.GetBytes(saltedPassword);

        SHA384 algo = SHA384.Create();

        var hashBytes = algo.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
    }
}
