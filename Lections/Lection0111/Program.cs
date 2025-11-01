using Lection0111.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

var client = new HttpClient();
string baseUrl = "https://api.escuelajs.co/api/v1/";
client.BaseAddress = new Uri(baseUrl);

var jsonOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
};
var category = new Category { Name = "test777", Image = "https://placeimg.com/640/480/any" };
var json = JsonSerializer.Serialize(category, jsonOptions);
var content = new StringContent(json, Encoding.UTF8, "application/json");

var response = await client.PostAsync("categories", content);
response.EnsureSuccessStatusCode();

// получение объекта из ответа
if (response.IsSuccessStatusCode) 
{ 
    var responseJson = await response.Content.ReadAsStringAsync();
    category = JsonSerializer.Deserialize<Category>(responseJson, jsonOptions);
}


Console.WriteLine();
//using var response = await _client.GetAsync(…);
//response.EnsureSuccessStatusCode();

//var content = await response.Content.ReadAsStringAsync();
//var result = JsonSerializer.Deserialize<тип>(content, _jsonOptions);


//using HttpResponseMessage response = await TestGet(client);

(HttpResponseMessage response1, HttpResponseMessage response2, HttpResponseMessage response3) = await TestApi(client);

static async Task<(HttpResponseMessage response1, HttpResponseMessage response2, HttpResponseMessage response3)> TestApi(HttpClient client)
{
    var categories = await client.GetFromJsonAsync<List<Category>>("categories");

    int id = 41;
    var category = await client.GetFromJsonAsync<Category>($"categories/{id}");

    category = new Category { Name = "test425267692281337", Image = "https://placeimg.com/640/480/any" };
    using var response1 = await client.PostAsJsonAsync("categories", category);
    response1.EnsureSuccessStatusCode();

    var result = await response1.Content.ReadFromJsonAsync<Category>();

    category.Name = "newName";
    using var response2 = await client.PutAsJsonAsync($"categories/{category.Id}", category);
    response2.EnsureSuccessStatusCode();

    using var response3 = await client.DeleteAsync($"categories/{category.Id}");
    response3.EnsureSuccessStatusCode();
    return (response1, response2, response3);
}

static async Task<HttpResponseMessage> TestGet(HttpClient client)
{
    var response = await client.GetAsync("categories");
    response.EnsureSuccessStatusCode();

    var jsonOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase, //с маленькой буквы
        WriteIndented = true, //для отступов
    };

    var content = await response.Content.ReadAsStringAsync();
    var result = JsonSerializer.Deserialize<List<Category>>(content, jsonOptions);
    return response;
}