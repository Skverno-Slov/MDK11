using Lection1012.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Обратный инжиниринг");
//Scaffold-DbContext "Data Source=mssql;Initial Catalog=ispp3114;Persist Security Info=True;User ID=ispp3114;Password=3114;Encrypt=True;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer -Context MarketContext -ContextDir Contexts -OutputDir Models -Tables Game, Category