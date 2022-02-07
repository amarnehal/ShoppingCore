using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ShoppingCore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [ValidateNever]
        public string ImageUrl { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [Column(TypeName ="decimal(18,4)") ]
        [Required]
        public double Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [Required]
        public int ProductTypeId { get; set; }
        [ValidateNever]
        public ProductType ProductType { get; set; }
    }
}
