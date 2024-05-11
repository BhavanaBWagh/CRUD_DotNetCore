using System.ComponentModel.DataAnnotations;

namespace CRUD_Operations_Core.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(3)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        
        [MaxLength(100)]
        public string Currency { get; set; }
    }
}
