using System.Collections.Generic;
using System.Threading.Tasks;
using MIW_ProductService.Dal.Models;

namespace MIW_ProductService.Core.Services.Interfaces
{
    public interface IProductService
    {
        public Task<List<Product>> GetAll();
        public Task<Product> Get(long id);
        public Task<Product> Create(Product product);
    }
}