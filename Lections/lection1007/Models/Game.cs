using System.ComponentModel.DataAnnotations.Schema;

namespace lection1007.Models
{
    [Table("Game")]
    public class Game
    {
        public int GameId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public short KeysAmout { get; set; }

        public Category? Category { get; set; }
    }
}

