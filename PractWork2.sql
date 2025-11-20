--Task1
CREATE FUNCTION GetPointsByUserId
(
	@visitorId INT
)
RETURNS INT
AS
BEGIN
	DECLARE @totalMinutes INT;

	SELECT @totalMinutes = SUM(Movie.Duration)
	FROM Ticket
		INNER JOIN [Session] ON Ticket.SessionId = [Session].SessionId
		INNER JOIN Movie ON [Session].MovieId = Movie.MovieId
	WHERE Ticket.VisitorId = @visitorId;

	RETURN ISNULL(@totalMinutes, 0);
END

SELECT *,
	dbo.GetPointsByUserId(visitorId) AS [Total points]
FROM Visitor;

--Task2
ALTER FUNCTION GetMovieByGenre
(
    @name nvarchar(50)
)
RETURNS TABLE AS RETURN
(
    SELECT 
        Movie.MovieId,
        Movie.[Name],
        STRING_AGG(Genre.[Name], ', ') AS Genres
    FROM Movie
		INNER JOIN MovieGenre ON Movie.MovieId = MovieGenre.MovieId
		INNER JOIN Genre ON MovieGenre.GenreId = Genre.GenreId
    GROUP BY Movie.MovieId, Movie.[Name]
	HAVING CHARINDEX(@name, STRING_AGG(Genre.[Name], ', ')) > 0
)

SELECT *
FROM dbo.GetMovieByGenre('Драма1');

--Task3 
ALTER PROCEDURE CreateTicket
    @phone char(11),
    @sessionId int,
	@row tinyint,
	@seat tinyint,
	@ticketId int OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Ticket (VisitorId, SessionId, [Row], Seat)
	SELECT VisitorId, @sessionId, @row, @seat
	FROM Visitor
	WHERE Phone = @phone;
	SET @ticketId = SCOPE_IDENTITY();
END

DECLARE @ticketId int;
EXEC CreateTicket '89009142564', 1, 2, 4, @ticketId OUTPUT;
SELECT @ticketId AS ticketid;

--Task 4
ALTER PROCEDURE CreateHall
    @hallNumber tinyint,
	@rowsNumber tinyint,
	@seatsNumber tinyint,
	@cinema nvarchar(50) = 'Титан-арена'
AS
BEGIN
	SET NOCOUNT ON;
	IF EXISTS
	(
		SELECT Cinema, HallNumber
		FROM Hall
		WHERE Cinema = @cinema AND HallNumber = @hallNumber
	)
	BEGIN
		UPDATE Hall
		SET RowsNumber = @rowsNumber,
		SeatsNumber = @seatsNumber
		WHERE Cinema = @cinema AND HallNumber = @hallNumber;
	END
	ELSE
	BEGIN
		INSERT INTO Hall (Cinema, HallNumber, RowsNumber, SeatsNumber)
		VALUES (@cinema, @hallNumber, @rowsNumber, @seatsNumber);
	END
END

EXEC CreateHall 2, 8, 15;

SELECT *
FROM Hall;

--Task 5

CREATE FUNCTION GetMoviesByCinemaToday
(
    @param1 int,
    @param2 char(5)
)
RETURNS TABLE AS RETURN
(
    SELECT @param1 AS c1,
	       @param2 AS c2
)
