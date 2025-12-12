CREATE FUNCTION GetGamesByPrice2
(
    @maxPrice int = 1000
)
RETURNS TABLE AS RETURN
(
    SELECT *
	FROM Game
	Where Price < @maxPrice
)

SELECT *
FROM GetGamesByPices2(1500)


CREATE FUNCTION GetGamesCount
(
    @categoryId int
)
RETURNS INT
AS
BEGIN
    DECLARE @count INT
    SELECT @count = COUNT(*)
	FROM Game
	WHERE CategoryId = @categoryId;
	RETURN @count
END

SELECT dbo.GetGamesCount(1)


CREATE PROCEDURE dbo.Sample_Procedure 
    @param1 int = 0,
    @param2 int  
AS
    SELECT @param1,@param2 
RETURN 