using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public ICollection<Item>? items { get; set; }
    }
}
