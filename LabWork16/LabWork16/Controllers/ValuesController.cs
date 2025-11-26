using LabWork16.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using LabWork16.Service;
using LabWork16.Contexts;

namespace LabWork16.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController(CinemaDbContext context) : ControllerBase
    {
        private readonly CinemaDbContext _context = context;
        private readonly AuthServices _authServices = new();

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return ["value1", "value2"];
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("auth/register")]
        public async Task<IActionResult> Register([FromBody] LoginRequest request)
        {
            var password = request.Password;
            var login = request.Login;

            if (string.IsNullOrWhiteSpace(login))
                return BadRequest("Логинне может быть пустыми");

            if (string.IsNullOrWhiteSpace(password))
                return BadRequest("Пароль не может быть пустыми");

            if (_authServices.IsUserExists(login, _context))
                return BadRequest("Пользователь с таким логином уже существует");

            var passwordHash = _authServices.HashPassword(password);

            var user = new CinemaUser
            {
                Login = login,
                HashPassword = passwordHash,
                RoleId = _context.CinemaUserRoles
                .FirstOrDefault(r => r.Name == "Посетитель").RoleId
            };

            await _context.CinemaUsers.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPost("auth/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            const string InvalidUserMessage = "Неверный логин или пароль";

            var login = request.Login;
            var password = request.Password;

            if (!_authServices.IsUserExists(login, _context))
                BadRequest(InvalidUserMessage);

            var user = _context.CinemaUsers
                .FirstOrDefault(u => u.Login == login);

            if (user == null)
                return BadRequest();

            if (!_authServices.VerifyPassword(password, user.HashPassword))
                return Unauthorized(InvalidUserMessage);

            var token = _authServices.GenerateToken(user, _context);

            return Ok(new TokenResponse { Token = token });
        }
    }
}
