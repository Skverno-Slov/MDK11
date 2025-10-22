using Lection1020.Contexts;
using Lection1020.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

Console.WriteLine("Выполнение SQL-запросов средствами ORM");
//Scaffold-DbContext "Data Source=mssql;Initial Catalog=ispp3114;Persist Security Info=True;User ID=ispp3114;Password=3114;Encrypt=True;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -ContextDir Contexts -Context GamesDbContext -Tables Game, Category
//"Data Source=mssql;Initial Catalog=ispp3100;Persist Security Info=True;User ID=ispp3100;Password=3100;Encrypt=True;Trust Server Certificate=True"

using var context = new GamesDbContext();

int minPrice = 500;
int maxPrice = 1500;

var games = context.Games
    .FromSql($"Select * From dbo.GetGamesByPices({minPrice}, {maxPrice})");
Console.WriteLine();
//AddCategory(context);
//GetGameByPrice(context);
//NewMethod(context);
//SqlQuery(context);
//Console.WriteLine();
//await FromSql(context);
//Console.WriteLine();

Console.WriteLine();
static async Task FromSql(GamesDbContext context)
{
    var games = context.Games
        .FromSql($"select * from game");
    Console.WriteLine(games.ToQueryString());

    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));

    Console.WriteLine();
    int price = 1000;
    games = context.Games
        .FromSql($"select * from game where price < {price}");
    Console.WriteLine(games.ToQueryString());
    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));

    Console.WriteLine();

    string columnName = "price";
    games = context.Games
        .FromSqlRaw($"select * from game order by {columnName}");
    Console.WriteLine(games.ToQueryString());
    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));

    Console.WriteLine();

    //string title = "SimCity";
    //games = context.Games
    //    .FromSqlRaw($"select * from game order by '{title}'");
    //Console.WriteLine(games.ToQueryString());
    //await games.ForEachAsync(g =>
    //    Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));

    //Console.WriteLine();

    //string title = "SimCity";
    //games = context.Games
    //    .FromSqlRaw($"select * from game where name = {0}", title);
    //Console.WriteLine(games.ToQueryString());
    //await games.ForEachAsync(g =>
    //    Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));

    Console.WriteLine();

    string title = "SimCity";
    games = context.Games
        .FromSqlRaw($"select * from game where name = @p0", title);
    Console.WriteLine(games.ToQueryString());
    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));

    Console.WriteLine();

    var sqlTitle = new SqlParameter("@title", "SimCity");
    games = context.Games
    .FromSqlRaw($"select * from game where name = @title", sqlTitle);
    Console.WriteLine(games.ToQueryString());
    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name} {g.Price} {g.CategoryId}"));

    Console.WriteLine();
}

static void SqlQuery(GamesDbContext context)
{
    var titles = context.Database
        .SqlQuery<string>($"select name from game");
    Console.WriteLine(titles.ToQueryString());
    Console.WriteLine();

    var minPrice = context.Database
        .SqlQuery<decimal>($"select min(price) as value from game")
        .FirstOrDefault();
    Console.WriteLine();

    //minPrice = context.Database
    //    .SqlQueryRaw<decimal>($"select min(price) as value from game")
    //    .FirstOrDefault();
    //Console.WriteLine();
}

static void NewMethod(GamesDbContext context)
{
    var games = context.Games
        .Where(g => EF.Functions.Like(g.Name, "[a-d]%"));

    decimal addingPrice = 0.5m;
    int changedRows = context.Database
        .ExecuteSql($"update game set price+={addingPrice}");
}

static void GetGameByPrice(GamesDbContext context)
{
    int price = 500;
    var games = context.Games
        .FromSql($"dbo.GetGamesByPrice {price}");
    Console.WriteLine();
}

static void AddCategory(GamesDbContext context)
{
    var id = new SqlParameter("@id", SqlDbType.Int)
    {
        Direction = ParameterDirection.Output
    };

    string category = "arcada2";
    context.Database
        .ExecuteSqlRaw($"dbo.Addcategory {category}, @id OUTPUT", id);
}