using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinary.Models;

namespace Veterinary.Interfaces
{
    internal interface IProductService
    {
        Task<Product> CreateProduct(Product product);

        Task<Product> GetProductById(Guid productId);

        Task<IEnumerable<Product>> GetProductByCategory(Guid categoryId);

        Task<IEnumerable<Product>> GetProducts();
        
    }
}
