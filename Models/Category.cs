using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNetMasteryProject.Models
{
    public class Category
    {
        [Key]
        public int Category21Id { get; set; }
        [Required]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
