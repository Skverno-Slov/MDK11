using Lection0611;
using System.Security.Cryptography;
using System.Text;

Console.WriteLine("passwords");

static void LockUser()
{
    var login = "admin";
    var password = "123";
    using var context = new AppDbContext();
    var user = context.Users.FirstOrDefault(u => u.Login == login);

    if (user is null)
    {
        Console.WriteLine("not found");
        return;
    }
    // проверка, что пользователь заблокирован
    if (IsUserLocked(user))
    {
        Console.WriteLine($"locked until {user.LockedUntil:HH:mm:ss}");
        return;
    }

    // проверка, что попытка аутентификации неуспешна
    if (IsCorrectPassword(password, user))
    {
        Console.WriteLine("incorrect password");
        context.SaveChanges();
        return;
    }

    SuccessLogin(user);
    context.SaveChanges();

    Console.WriteLine("welcome");
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

static async Task<AppDbContext> InsertData()
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

static bool IsUserLocked(User user)
{
    if (user.LockedUntil.HasValue && user.LockedUntil <= DateTime.UtcNow)
    {
        user.FailedLoginAttempts = 0;
        user.LockedUntil = null;
        return false;
    }
    return user.LockedUntil.HasValue;
}

static bool IsCorrectPassword(string password, User user)
{
    int attempts = 3;
    int duration = 30;
    if (user.Password != password)
    {
        user.FailedLoginAttempts++;
        if (user.FailedLoginAttempts >= attempts)
            user.LockedUntil = DateTime.UtcNow.AddSeconds(duration);
        return false;
    }
    return true;
}

static void SuccessLogin(User user)
{
    user.LastAccess = DateTime.UtcNow;
    user.FailedLoginAttempts = 0;
}