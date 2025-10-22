using Microsoft.Data.SqlClient;
using System.Data;

Console.WriteLine("Разработка клиента");


//var connectionStirng = "..."; // или ConnectionStringBuilder
//using IDbConnection connection = new SqlConnection(connectionStirng);
//string query = "INSERT INTO Catergory(title) OUTPUT INSERTED.CategoryId VALUES(@CategoryId)";

//connection.МетодDapper(query, new { парам1 = знач1, парам2 = знач2... });
//connection.МетодDapper(query, объект); // свойства объекта - параметры для запроса

//await connection.МетодDapperAsync(...); // асинхронный вызов