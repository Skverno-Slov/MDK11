--Task1

USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [ispp3114]    Script Date: 01.12.2025 9:32:17 ******/
CREATE LOGIN [ispp3114]
--Для безопасноти(пользователь, создавший скрипт не должен видеть пароль)
WITH PASSWORD=N'l1Yc9i/OoEFoeeNanI2FrhqvdD11McvqyvVhfBGM0Vg=',
DEFAULT_DATABASE=[ispp3114], DEFAULT_LANGUAGE=[русский],
--Проверка времени жизни пароля 
CHECK_EXPIRATION=OFF,
--Проверка надёжности пароля
CHECK_POLICY=OFF
GO

ALTER LOGIN [ispp3114] DISABLE
GO

USE [ispp3114]
GO

EXEC sp_adduser 'login2', 'user2';

EXEC sp_adduser 'login1', 'user1';

CREATE USER user3 FOR LOGIN login3 WITH DEFAULT_SCHEMA=[dbo];

CREATE USER user4 FOR LOGIN login4 WITH DEFAULT_SCHEMA=[dbo];

EXEC sp_addlogin 'ispp31142', 'Password!';

EXEC sp_addsrvrolemember 'ispp31142', 'securityadmin';

--Task2

EXEC sp_addrolemember 'db_owner', 'user1';

EXEC sp_addrolemember 'db_datareader', 'user2';

EXEC sp_addrolemember 'db_datawriter', 'user2';

EXEC sp_droprolemember 'db_datawriter', 'user2';

--Task3

GRANT DELETE, UPDATE ON   
    dbo.Ticket 
    TO user3;
	
GRANT SELECT ON
	dbo.Visitor
	TO user4;

GRANT UPDATE ON 
	dbo.Visitor ([Name], Email)
	TO user4;

DENY SELECT ON
	dbo.Visitor
	TO user2;

DENY UPDATE ON
	dbo.Visitor ([Name])
	TO user4;

--Task4

DECLARE @roleNumber INT;

SET @roleNumber = 1;

WHILE @roleNumber < 5
BEGIN
	EXEC('CREATE USER [reader' + @roleNumber + '] FOR LOGIN [reader' + @roleNumber + '] WITH DEFAULT_SCHEMA=dbo');
	EXEC sp_addrolemember 'reader' + CAST(@roleNumber AS VARCHAR(1)) , 'user1';
	SET @roleNumber += 1;
END

--Task5
