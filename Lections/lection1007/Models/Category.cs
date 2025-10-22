using System.ComponentModel.DataAnnotations.Schema;

namespace lection1007.Models
{
    [Table("Category")]
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;

        public IEnumerable<Game>? Games { get; set; }
    }
}
