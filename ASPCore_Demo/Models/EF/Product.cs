using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPCore_Demo.Models.EF
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Name Product")]
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(30, ErrorMessage = "Maximum 30 characters only.")]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [DisplayName("Amount")]
        [Column(TypeName = "int")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [DisplayName("Price")]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }

        [DisplayName("Description")]
        [Column(TypeName = "nvarchar(MAX)")]
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateCreate { get; set; }
    }
}
