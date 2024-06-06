using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace CRUD_Operations_Core.Models
{
    public class PersonDetail
    {
        [Key]
        public int Id { get; set; }
        public long RowNumber { get; set; }

        [Required]
        [MaxLength(75)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(75)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-Mail is not Valid")]
        public string EmailId { get; set; }


        [Required]
        [ForeignKey("City")]
        [DisplayName("City")]
        public int CityId { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DOB { get; set; }



        [MaxLength(10)]
        [Display(Name = ("Gender"))]
        [Required(ErrorMessage = "Please select Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = ("Active"))]
        public bool Active { get; set; }


        [Required(ErrorMessage = "Please choose the Customer Photo")]
        [Column(TypeName = "varchar(MAX)")]

        public string PhotoUrl { get; set; }


        public int cId { get; set; }

        public string cName { get; set; }

        public int cnId { get; set; }

        public string cnName { get; set; }

    }
}

