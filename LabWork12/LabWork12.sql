CREATE PROCEDURE dbo.GetVisitorTicketsByPhone
    @PhoneNumber NVARCHAR(11)
AS
    SELECT t.*
    FROM Ticket t
    JOIN Visitor v ON t.VisitorId = v.VisitorId
    WHERE v.Phone = @PhoneNumber;

CREATE PROCEDURE dbo.CreateVisitor
    @Phone NVARCHAR(11),
    @VisitorId INT OUTPUT
AS
    INSERT INTO Visitor (Phone)
        VALUES (@Phone)
        SET @VisitorId = SCOPE_IDENTITY();

CREATE FUNCTION dbo.GetSessionsByMovieId(@MovieId INT)
    RETURNS TABLE
AS
    RETURN(
    SELECT s.*
    FROM [Session] s
    JOIN Movie m ON s.MovieId = m.MovieId
    WHERE s.MovieId = @MovieId);