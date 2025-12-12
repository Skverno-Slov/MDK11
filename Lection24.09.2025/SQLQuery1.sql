--CREATE TRIGGER TrGamesRowsCount
--    ON Game
--    FOR DELETE, INSERT, UPDATE
--    AS
--      PRINT 'Количество измененных строк: ' + CAST (@@ROWCOUNT AS VARCHAR(10))

--UPDATE Game
--Set Price += 1;


--CREATE TRIGGER TrSavePrice
--    ON Game
--    AFTER UPDATE
--    AS
--	  IF UPDATE(Price)
--	    INSERT INTO GamePrice(GameId, OldPrice)
--	    SELECT GameId, Price
--	    FROM deleted;

--UPDATE Game
--Set Price += 10
--where GameId < 3

--select *
--from GamePrice


--CREATE TRIGGER TrSaveCategory
--	ON Category
--	AFTER DELETE
--	AS
--		INSERT INTO DeletedCategory(CategoryId, [Name])
--		SELECT CategoryId, [Name]
--		FROM deleted;

--insert into category(name) values('рогалик'), ('jRPG')


CREATE TRIGGER TrDeleteGame
    ON Game
    INSTEAD OF DELETE
    AS
	  UPDATE Game
	  SET IsDeleted=1
	  Where GameId IN (SELECT GameId
						FROM deleted)