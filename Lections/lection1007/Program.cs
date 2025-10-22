using lection1007.Contexts;
using lection1007.DTOs;
using lection1007.Filters;
using lection1007.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

Console.WriteLine("Дикий ORM");
Console.WriteLine();

var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();
optionsBuilder.UseSqlServer("Data Source=mssql;Initial Catalog=ispp311User ID=ispp3114;Password=3114;Trust Server Certificate=True");

optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
optionsBuilder.LogTo(message => Console.WriteLine(message), LogLevel.Information);

using var context = new StoreDbContext(optionsBuilder.Options);

var games = context.Games
    .Join(context.Categories,
        g => g.CategoryId,
        c => c.CategoryId,
        (g, c) => new
        {
            g.GameId,
            g.Name,
            CategoryName = c.Name
        });
Console.WriteLine(games.ToQueryString());
Debug.WriteLine(games.ToQueryString());
Trace.WriteLine(games.ToQueryString());


var hasUndeletedGames = context.Games
    .Any(g => !g.IsDeleted);

GroupBy(context);

SelectDto(context);

Sort(context);

FilterBy(context);

Filter(context);

Pagination(context);

//Include(context, games);

//var games = context.Games
//    .Include(g => g.Category)
//    .Select(g => g.ToDto());

//var games = context.Games
//    .Select(g => new GameDto
//    {
//        Title = g.Name,
//        Price = g.Price,
//        Tax = g.Price * 0.2m,
//        Category = g.Category?.Name ?? ""
//    });

//var games = context.Games
//    .Select(g => new
//    {
//        Title = g.Name,
//        g.Price,
//        Tax = g.Price * 0.2m,
//        Category = g.Category?.Name ?? ""
//    });

//var categoryService = new CategoryService(context);
//var categories = await categoryService.GetCategoriesAsync();
//foreach (var category in categories)
//    Console.WriteLine(category.Name);

//await AddCategory(context);

//await RemoveCategory(context);

//update
//1
//await UpdateCategoryFromDB(context);
//2
//UpdateCategory(context);

//context.Games
//    .Where(g => g.GameId > 4)
//    .ExecuteDelete();

//context.Games
//    .Where(g => g.CategoryId == 2)
//    .ExecuteUpdate(setters => setters
//        .SetProperty(g => g.IsDeleted, g => false)
//        .SetProperty(g => g.KeysAmount, g => g.KeysAmount > 100 ? 120 : 75));

//GetList(context);
//try
//{
//    await GetValue(context);
//}
//catch
//{
//    Console.WriteLine("trash");
//}

static async Task GetValue(AppDbContext context)
{
    var game = context.Games.Find(1);
    game = await context.Games.FindAsync(2);

    game = context.Games.FirstOrDefault(g => g.GameId > 2);
    game = await context.Games.FirstOrDefaultAsync(g => g.GameId > 2);


    game = context.Games.SingleOrDefault(g => g.GameId == 2);
    game = await context.Games.SingleOrDefaultAsync(g => g.GameId > 2);
}

static void GetList(AppDbContext context)
{
    var categorise = context.Categories;
    foreach (var category in categorise)
        Console.WriteLine($"{category.CategoryId} {category.Name}");

    Console.WriteLine();

    var games = context.Games;
    foreach (var game1 in games)
        Console.WriteLine($"{game1.GameId} {game1.Name}");
}

static async Task AddCategory(AppDbContext context)
{
    var category = new Category()
    {
        Name = "casual"
    };
    context.Categories.Add(category);
    context.SaveChanges();
}

static async Task RemoveCategory(AppDbContext context)
{
    var category = context.Categories.Find(6);
    if (category is not null)
    {
        context.Categories.Remove(category);

        context.SaveChanges();
        await context.SaveChangesAsync();
    }
}

static async Task UpdateCategoryFromDB(AppDbContext context)
{
    var category = context.Categories.Find(1);
    if (category is null)
        throw new ArgumentException("Category is not found");
    category.Name = "arcade";
    context.SaveChanges();
    await context.SaveChangesAsync();
}

static void UpdateCategory(AppDbContext context)
{
    var category = new Category()
    {
        CategoryId = 1,
        Name = "Аркада"
    };
    context.Categories.Update(category);
    context.SaveChanges();
}

static void Include(StoreDbContext context, DbSet<Game> games)
{
    var result = context.Games
        .Include(g => g.Category);
    foreach (var x in games)
        Console.WriteLine($"{x.Name} {x.Category?.Name}");
    Console.WriteLine(result.ToQueryString());
    Console.WriteLine();

    var categories = context.Categories
        .Include(c => c.Games);
    foreach (var x in categories)
        Console.WriteLine($"{x.Name} {x.Games?.Count()}");
    Console.WriteLine(result.ToQueryString());
    Console.WriteLine();
}

static void Pagination(StoreDbContext context)
{
    int pageSize = 3;
    int currentPage = 4;
    var games = context.Games
        .Skip(pageSize * (currentPage - 1))
        .Take(pageSize);
    foreach (var game in games)
        Console.WriteLine(game.Name);

    Console.WriteLine(games.ToQueryString());
    Console.WriteLine();

    int rowsCount = context.Games.Count();
    //var 1
    int pagesCount = rowsCount / pageSize;
    if (rowsCount % pageSize > 0)
        pagesCount++;
    //var 2
    pagesCount = (int)Math.Ceiling(1.0 * rowsCount / pageSize);
}

static void Filter(StoreDbContext context)
{
    var games = context.Games.AsQueryable();
    if (true) //заменить условие
        games = games.Where(g => g.Price < 500);
    if (true) //заменить условие
        games = games.Where(g => g.Name.Contains("a"));


    Console.WriteLine(games.ToQueryString());
}

static void FilterBy(StoreDbContext context)
{
    GameFilter gameFilter = new()
    {
        Price = 500,
        Category = "RPG"
    };

    var games = context.Games.AsQueryable();
    if (gameFilter.Price is not null)
        games = games.Where(g => g.Price < gameFilter.Price);
    if (gameFilter.Name is not null)
        games = games.Where(g => g.Name == gameFilter.Name);
    if (gameFilter.Category is not null)
        games = games.Where(g => g.Category.Name == gameFilter.Category);
}

static void Sort(StoreDbContext context)
{
    var games = context.Games
        .OrderByDescending(g => g.Price);

    games = context.Games
        .OrderByDescending(g => EF.Property<object>(g, "Name"));

    foreach (var game in games)
        Console.WriteLine($"{game.Name} {game.Price}");
}

static void SelectDto(StoreDbContext context)
{
    var titles = context.Games
        .Select(g => g.Name);
    foreach (var title in titles)
        Console.WriteLine(title);

    var games = context.Games
        .Select(GameExtension.ToDto);

    foreach (var g in games)
        Console.WriteLine($"{g.Title} {g.Price} {g.Tax} {g.Category}");
}

static void GroupBy(StoreDbContext context)
{
    var catrgories = context.Games
        .GroupBy(g => g.Category!.Name)
        .Select(group => new
        {
            group.Key,
            GamesCount = group.Count()
        });

    Console.WriteLine(catrgories.ToQueryString());

    var catrgories2 = context.Games
        .GroupBy(g => new { g.Category!.Name, g.IsDeleted })
        .Select(group => new
        {
            CategoryName = group.Key.Name,
            group.Key.IsDeleted,
            GamesCount = group.Count(g => g.IsDeleted)
        });

    Console.WriteLine(catrgories2.ToQueryString());
}