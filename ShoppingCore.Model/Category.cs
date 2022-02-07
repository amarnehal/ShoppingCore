using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCore.Models
{
    public class Category
    {   
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string  Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage = "Display Order must be between 1 to 100")]
        public int DisplayOrder { get; set; }
        public List <Product> Products { get; set; }
    }
}
