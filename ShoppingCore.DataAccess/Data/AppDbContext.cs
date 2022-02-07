using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingCore.Model;
using ShoppingCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCore.DataAccess
{
    public class AppDbContext:IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet <Product> Products { get; set; }
        public DbSet <ProductType> ProductType{ get; set; }
        public DbSet <ApplicationUser> ApplicationUsers { get; set; }
        public DbSet <ShoppingCart> ShoppingCarts{ get; set; }
        public DbSet <OrderHeader>  OrderHeaders{ get; set; }
        public DbSet <OrderDetail>  OrderDetails{ get; set; }
    }
}
