using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MIW_ProductService.Core.Services.Interfaces;
using MIW_ProductService.Dal.Contexts;
using MIW_ProductService.Dal.Models;

namespace MIW_ProductService.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly ProductContext _productContext;

        public ProductService(ILogger<ProductService> logger, ProductContext productContext)
        {
            _logger = logger;
            _productContext = productContext;
        }
        
        public async Task<List<Product>> GetAll()
        {
            try
            {
                return await _productContext.Products.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Product> Get(long id)
        {
            try
            {
                return await _productContext.FindAsync<Product>(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Product> Create(Product product)
        {
            try
            {
                await _productContext.AddAsync(product);
                await _productContext.SaveChangesAsync();
                return product;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}