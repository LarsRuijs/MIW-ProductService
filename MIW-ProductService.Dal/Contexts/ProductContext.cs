using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MIW_ProductService.Dal.Models;

namespace MIW_ProductService.Dal.Contexts
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }
    }
}