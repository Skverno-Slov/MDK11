using LabWork8;
using LabWork8.Model;
using LabWork8.Repository;
using System.Data;

DatabaseContext database = new("mssql", "ispp3114", "ispp3114", "3114");
GenreRepository genreRepository = new(database);
VisitorRepository visitorRepository = new(database);

try
{
    var genre = genreRepository.GetByIdAsync(1);
    var genries = genreRepository.GetAllAsync();

    var visitor = visitorRepository.GetByIdAsync(1);
    var visitirs = visitorRepository.GetAllAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message); 
}
