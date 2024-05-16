using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Operations_Core.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(3)]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [ForeignKey("CountryId")]

        public int CountryId { get; set; }

        [MaxLength(100)]
        [NotMapped]
        public string CountryName { get; set; }

        public virtual Country Country { get; set; }
    }
}
