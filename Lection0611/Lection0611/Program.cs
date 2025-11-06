using Lection0611;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("passwords");

var login = "admin";
var password = "123";
using var context = new AppDbContext();
var user = context.Users.FirstOrDefault(u => u.Login == login);

if (user is null)
{
    Console.WriteLine("Not fount");
    return;
}

if (user.LockedUntil.HasValue && user.LockedUntil >= DateTime.UtcNow)
{
    Console.WriteLine($"too early. wait {user.LockedUntil}");
    return;
}

if (user.Password != password)
{
    user.FailedLoginAttempts++;
    if (user.FailedLoginAttempts >= 3)
        user.LockedUntil = DateTime.UtcNow.AddMinutes(1);
    context.SaveChanges();
    Console.WriteLine($"incorrect");
    return;
}

static void ComputeHash()
{
    var salt = "Minecraft";
    var password = "qwerty" + salt;
    byte[] bytes = Encoding.UTF8.GetBytes(password);

    //MD5 algo = MD5.Create();
    SHA384 algo = SHA384.Create();

    var hashBytes = algo.ComputeHash(bytes);
    var hash = Convert.ToBase64String(hashBytes); // base64
    hash = Convert.ToHexString(hashBytes);        // hex: 0-9A-F
}

static void CopmuteBcryptHash()
{
    var password = "qwerty";
    var hash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 15, BCrypt.Net.HashType.SHA512);
    Console.WriteLine(hash);

    hash = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    Console.WriteLine(hash);

    hash = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 15, BCrypt.Net.HashType.SHA512);
    Console.WriteLine(hash);

    var input = "qwerty";
    var isCorrect = BCrypt.Net.BCrypt.EnhancedVerify(input, hash);
    Console.WriteLine(isCorrect);
}

static async Task InsertData()
{
    var users = new List<User>()
{
    new() { Login = "admin", Password="qwerty"},
    new() { Login = "manager", Password="123"},
    new() { Login = "customer", Password="1"},
};
    var context = new AppDbContext();
    context.Users.AddRange(users);
    await context.SaveChangesAsync();
    return context;
}