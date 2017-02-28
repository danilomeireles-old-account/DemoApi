using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DemoApi.Models
{
    public class ShoppingCart
    {
        // One-To-One: CustomerId is the PK of ShoppingCart:
        [Key, ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }        
    }
}