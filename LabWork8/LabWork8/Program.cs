using LabWork8;
using LabWork8.Repository;

DatabaseContext database = new("mssql", "ispp3114", "ispp3114", "3114");
GenreRepository genreRepository = new(database);
VisitorRepository visitorRepository = new(database);

try
{
    Console.WriteLine("---Жанры---");
    var genries = await genreRepository.GetAllAsync();
    foreach (var item in genries)
        Console.WriteLine(item.Name);

    Console.WriteLine("---Жанр---");
    var genre = await genreRepository.GetByIdAsync(1);
    Console.WriteLine(genre.Name);

    Console.WriteLine("---Пользователь---");
    var visitor = await visitorRepository.GetByIdAsync(1);
    Console.WriteLine(visitor.Name);

    Console.WriteLine("---Пользователи---");
    var visitors = await visitorRepository.GetAllAsync();
    foreach (var item in visitors)
        Console.WriteLine(item.Name);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
