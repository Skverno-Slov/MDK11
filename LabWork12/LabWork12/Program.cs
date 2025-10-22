using LabWork12.Contexts;
using LabWork12.Services;

const int MovieId = 30;
try
{
    using var context = new AppDbContext();
    var movieService = new MovieService(context);
    var sessionService = new SessionService(context);
    var ticketService = new TicketService(context);
    var visitorService = new VisitorService(context);

    Console.WriteLine("Task 1. Список фильмов");
    var movies = await movieService.GetMoviesAsync("Name");
    movies.ForEach(x => Console.WriteLine($"{x.Name} {x.Year}"));

    Console.WriteLine("Task 2. Получение фильма");
    Console.WriteLine("Введите название фильма");
    string? movieName = Console.ReadLine();
    Console.WriteLine("Введите год выхода фильма");
    short.TryParse(Console.ReadLine(), out short movieYear);
    movies = await movieService.GetMoviesByNameAndYearAsync(movieName, movieYear);
    movies.ForEach(x => Console.WriteLine($"{x.Name} {x.Year}"));

    Console.WriteLine("Task 3. Увелечкение цены и вывод кол-л затронутых строк.");
    Console.WriteLine(await sessionService.AddPricesByHallIdAsync(1, 100));

    Console.WriteLine("Task 4. Жанры фильма");
    var genres = await movieService.GetGenresByMovieIdAsync(MovieId);
    foreach(var genre in genres)
        Console.WriteLine(genre);

    Console.WriteLine("Task 5. Дата и время киносеанса");
    var sessionDateTime = await ticketService.GetSessionDateTimeByTicketIdAsync(1);
    Console.WriteLine(sessionDateTime);

    Console.WriteLine("Task 6.1 Фильмы из диопазона б-г");
    movies = await movieService.GetMovieFromRangeAsync('б', 'г');

    Console.WriteLine("Task 6.2 Минимальная цена фильма");
    Console.WriteLine(await sessionService.GetMinMoviePriceAsync(MovieId));
    Console.WriteLine("Максимальная цена фильма");
    Console.WriteLine(await sessionService.GetMinMoviePriceAsync(MovieId));
    Console.WriteLine("Средняя цена фильма");
    Console.WriteLine(await sessionService.GetAverageMoviePriceAsync(MovieId));

    Console.WriteLine("Task 7. Билеты посетителя");
    var tickets = await ticketService.GetTicketsByPhoneAsync("89009145564");
    tickets.ForEach(t => Console.WriteLine($"ряд:{t.Row} место:{t.Seat} id билета:{t.TicketId}"));

    Console.WriteLine("Task 8. ID созданого пользователя");
    Console.WriteLine(await visitorService.CreateVisitorByPhone("89000000002"));

    Console.WriteLine("Task 9. Киносеансы фильма");
    var sessions = await sessionService.GetSessionsByMovieIdAsync(MovieId);
    sessions.ForEach(t => Console.WriteLine($"Id сеанса:{ t.SessionId} Id фильма: {t.MovieId} Цена:{t.Price}"));
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}