using System.ComponentModel.DataAnnotations;

namespace DemoApi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]                
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]        
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }
    }
}