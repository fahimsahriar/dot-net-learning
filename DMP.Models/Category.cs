using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DMP.Models
{
    public class Category
    {
        [Key]
        public int Category21Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage ="Display order can not be negative and greater than 100")]
        public int DisplayOrder { get; set; }
    }
}
