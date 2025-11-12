using AuthLib;
using AuthLib.Contexts;
using AuthLib.Models;

using var context = new CinemaDbContext();
var authService = new AuthService(context);

string login = "FirstUser";
string password = "qwerty";

authService.Login = login;
authService.Password = password;

authService.RegistrateUser();
authService.AuthorizationUser();

Console.WriteLine(authService.GetUserRole());
var privileges = authService.GetUserPrivileges();
foreach (var privilege in privileges)
    Console.WriteLine(privilege);

var role = context.CinemaUserRoles
                .FirstOrDefault(r => r.Name == "посетитель");

privileges = await authService.GetRolePrivilege(role);

foreach (var privilege in privileges)
    Console.WriteLine(privilege);


