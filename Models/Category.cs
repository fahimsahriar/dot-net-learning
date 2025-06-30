using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotNetMasteryProject.Models
{
    public class Category
    {
        [Key]
        public int Category21Id { get; set; }
        [Required]
<<<<<<< HEAD
        [MaxLength(30)]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage ="Display order can not be negative and greater than 100")]
=======
        public string Name { get; set; }

        [DisplayName("Display Order")]
>>>>>>> 9c34fb2ba4548b23c03c212b8dcb78b998505cdf
        public int DisplayOrder { get; set; }
    }
}
