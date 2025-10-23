using LabWork7;

Console.WriteLine(DataAccessLayer.ConnectionString);

DataAccessLayer.ChangeSettings("mssql", "ispp3114", "ispp3114", "3114");
Console.WriteLine(DataAccessLayer.ConnectionString);

if (DataAccessLayer.TryOpenConnection())
    Console.WriteLine("Подключение уcпешно открыто");
else
    Console.WriteLine("Ошибка подключения.");


Console.WriteLine($"Строк изменено: {await DataAccessLayer.ExecuteSqlAsync("UPDATE Game SET Price += 1;")}");

Console.WriteLine($"Объект: {await DataAccessLayer.GetObjectAsync("SELECT MAX(Price) FROM Game")}");



