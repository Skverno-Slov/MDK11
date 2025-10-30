using Lection1024.Models;

namespace Lection1024.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Category { get; set; } = null!;

        public decimal Price { get; set; }
    }

    public static class GameExtensions
    {
        public static GameDto ToDto(this Game g) 
            => new()
            {
                Id = g.GameId,
                Name = g.Name,
                Category = g.Category.Name,
                Price = g.Price,
            };
    }
}
