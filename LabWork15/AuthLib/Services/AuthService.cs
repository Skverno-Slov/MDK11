using AuthLib.Contexts;
using AuthLib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace AuthLib.Services
{
    public class AuthService(CinemaDbContext context)
    {
        readonly CinemaDbContext _context = context;
        string? _login;
        string? _password;
        CinemaUser? _user;
        const int Attempt = 3;
        const int Duration = 60;

        public string? Login
        {
            get => _login;
            set => _login = value;
        }

        public string? Password
        {
            get => _password;
            set => _password = ComputeHash(value);
        }

        public bool RegistrateUser()
        {
            _user = Authentication();
            if (IsUserExists())
                return false;

            var role = _context.CinemaUserRoles
                .FirstOrDefault(r => r.Name == "посетитель");

            _context.Add(new CinemaUser
            {
                Login = Login,
                HashPassword = Password,
                RoleId = role.RoleId
            });

            _context.SaveChanges();

            return true;
        }

        CinemaUser? Authentication()
        {
            return _context.CinemaUsers
                .FirstOrDefault(u => u.Login == Login);
        }

        public CinemaUser? AuthorizationUser()
        {
            _user = Authentication();
            if (!IsUserExists())
                return null;
            if (IsUserLocked())
                return null;
            if (!IsCorrectPassword())
                return null;

            SuccessLogin();

            return _user;
        }

        void SuccessLogin()
        {
            _user.FailedLoginAttempts = 0;
            _user.LockedUntil = null;
            _context.SaveChanges();
        }

        bool IsUserExists()
        {
            if (_user is null)
                return false;
            return _context.CinemaUsers.Any(u => u.Login == _user.Login);
        }

        bool IsUserLocked()
        {
            if (_user.LockedUntil.HasValue && _user.LockedUntil <= DateTime.UtcNow)
            {
                _user.FailedLoginAttempts = 0;
                _user.LockedUntil = null;
                return false;
            }
            return _user.LockedUntil.HasValue;
        }

        string ComputeHash(string password)
        {
            const string salt = "Minecraft";
            var saltedPassword = password + salt;
            byte[] bytes = Encoding.UTF8.GetBytes(saltedPassword);

            SHA384 algo = SHA384.Create();

            var hashBytes = algo.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }

        bool IsCorrectPassword()
        {
            if (Password != _user.HashPassword)
            {
                _user.FailedLoginAttempts++;
                if (_user.FailedLoginAttempts >= Attempt)
                    _user.LockedUntil = DateTime.UtcNow.AddSeconds(Duration);
                _context.SaveChanges();
                return false;
            }
            return true;
        }

        public async Task<CinemaUserRole> GetUserRole()
        {

            var role = await _context.CinemaUserRoles
                .Include(r => r.CinemaUsers)
                .FirstOrDefaultAsync(r => r.CinemaUsers.Any(u => u.Login == Login));

            return role;
        }

        public List<string> GetUserPrivileges()
        {
            var user = _context.CinemaUsers
                .Include(u => u.Role)
                .ThenInclude(r => r.Privileges)
                .FirstOrDefault(u => u.Login == Login);

            if (user is null)
                return new();

            return user.Role.Privileges.Select(p => p.Name).ToList();
        }

        public bool IsDataCorrect()
        {
            return !Login.IsNullOrEmpty()
                && !Password.IsNullOrEmpty();
        }

        public async Task<List<string>> GetRolePrivilege(CinemaUserRole userRole)
        {
            var role = await _context.CinemaUserRoles
                .Include(r => r.Privileges)
                .FirstOrDefaultAsync(r => r.RoleId == userRole.RoleId);

            if (role is null)
                return new();

            return role.Privileges.Select(p => p.Name).ToList();
        }

        public List<CinemaUserRole> GetRoles()
            => _context.CinemaUserRoles.ToList();

        public List<CinemaUser> GetUsers()
            => _context.CinemaUsers.ToList();

        public void ChangeUserRoleAsync(CinemaUser user, CinemaUserRole role)
        {
            if (!_context.CinemaUsers.Any(u => u.UserId == user.UserId))
                return;

            user.RoleId = role.RoleId;
            _context.CinemaUsers.Update(user);
            _context.SaveChanges();
        }
    }
}
