CREATE PROCEDURE dbo.GetGamesByPrice
    @maxPrice decimal(16,2)
AS
    SELECT *
	FROM Game
	WHERE Price <=@maxPrice;

dbo.GetGamesByPrice 500

EXEC dbo.GetGamesByPrice 500

CREATE PROCEDURE dbo.Addcategory
    @name nvarchar(100),
    @id int OUTPUT  
AS
BEGIN
	INSERT INTO Category([name]) VALUES(@name);
	SET @id = SCOPE_IDENTITY();
END

DECLARE	@id int

EXEC dbo.Addcategory N'хоррор3', @id OUTPUT

SELECT	@id 

SELECT * FROM Category


CREATE FUNCTION dbo.GetGamesByPices
(
   @min decimal(16,2),
   @max decimal(16,2)
)
RETURNS TABLE AS RETURN
(
    SELECT *
	FROM Game
	Where Price <= @max AND Price >= @min
)

Select *
From dbo.GetGamesByPices(50, 1500)