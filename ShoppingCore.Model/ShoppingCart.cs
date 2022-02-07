using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ShoppingCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCore.Model
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }

        [Required]
        [Range(1,100, ErrorMessage ="Please Select a value between 1 to 100")]
        public int Count { get; set; }
        [ForeignKey("ApplicationUserId")]
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [NotMapped]
        public double Price { get; set; }
    }
}
