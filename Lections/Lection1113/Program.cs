using Lection1113;

Console.WriteLine("JWT");

AuthService service = new();
var accessToken = service.GenerateToken(123, "user1");
Console.WriteLine(accessToken);

if (service.IsValidToken(accessToken))
    Console.WriteLine("SWAGA");
else
    Console.WriteLine("NO SWAGA");