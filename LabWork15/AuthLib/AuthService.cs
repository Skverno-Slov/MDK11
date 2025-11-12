using AuthLib.Contexts;
using AuthLib.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthLib
{
    public class AuthService(CinemaDbContext context)
    {
        CinemaDbContext _context = context;
        string? _login;
        string? _password;
        CinemaUser? _user;

        public string Login 
        {
            get => _login;
            set => _login = value;
        }

        public string Password 
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
                Login = _login,
                HashPassword = _password,
                RoleId = role.RoleId
            });

            _context.SaveChanges();

            return true;
        }

        CinemaUser? Authentication()
        {
            return _context.CinemaUsers
                .FirstOrDefault(u => u.Login == _login);
        }

        public CinemaUser? AuthorizationUser()
        {
            _user = Authentication();
            if (!IsUserExists())
                return null;
            if (!IsCorrectPassword())
                return null;
            if (LockUser())
                return null;

            return _user;
        }

        void SuccessLogin()
        {
            _user.FailedLoginAttempts = 0;
            _user.LockedUntil = null;
        }

        bool IsUserExists()
        {
            if(_user is null) 
                return false;
            return (_context.CinemaUsers.Any(u => u.Login == _user.Login));
        }

        bool LockUser()
        {
            if (IsUserLocked())
            {
                SuccessLogin();
                _context.SaveChanges();
                return true;
            }
                
            return false;
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
            const int attempts = 3;
            const int duration = 60;
            if (_password != _user.HashPassword)
            {
                _user.FailedLoginAttempts++;
                if (_user.FailedLoginAttempts >= attempts)
                    _user.LockedUntil = DateTime.UtcNow.AddSeconds(duration);
                return false;
            }
            return true;
        }

        public async Task<string> GetUserRole()
        {

            var user = await _context.CinemaUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Login == _login);

            return user?.Role?.Name;
        }

        public List<string> GetUserPrivileges() 
        {
            var user = _context.CinemaUsers
                .Include(u => u.Role)
                .ThenInclude(r => r.Privileges)
                .FirstOrDefault(u => u.Login == _login);

            if (user is null)
                return new();

            return user.Role.Privileges.Select(p => p.Name).ToList();
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
    }
}
