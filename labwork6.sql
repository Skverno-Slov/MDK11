--Задание 1
ALTER TRIGGER TrSaveChangedEmail
    ON Visitor
    AFTER UPDATE
    AS
    IF UPDATE(Email)
		INSERT INTO EmailChange(VisitorId, Email)
		SELECT VisitorId, Email
		FROM deleted

--Задание 2
CREATE TRIGGER TrDeleteMovie
    ON Movie
    INSTEAD OF DELETE
    AS
		UPDATE Movie
		SET IsDeleted = 1
		WHERE MovieId IN(SELECT MovieId
						FROM deleted)
--Задание 3

CREATE TRIGGER TrSaveVisitor
    ON Visitor
    AFTER DELETE
    AS
    INSERT INTO DeleteVisitor(VisitorId, Phone, [Name], Birthday, Email)
	SELECT *
	FROM deleted;
--Задание 4

CREATE TRIGGER TrChangedPrice
    ON Session
    INSTEAD OF INSERT
    AS
		INSERT INTO Session (MovieId, HallId, Price, StartDate, Is3d)
		SELECT
			MovieId, HallId,
			CASE
				WHEN Price < 100 THEN 100
				ELSE Price
				END,
			StartDate, Is3d
		FROM inserted;
