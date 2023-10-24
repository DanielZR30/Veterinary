using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinary.Models;
using Veterinary.ViewModels;

namespace Veterinary.Interfaces
{
    internal interface IProductService
    {
        Task<Product> CreateProduct(Product product);

        Task<Product> GetProductById(Guid productId);

        Task<IEnumerable<Product>> GetProductByCategory(Guid categoryId);

        Task<IEnumerable<Product>> GetProducts();

        Task<Product> UpdateProduct(Product product);

        Task<Product> DeleteProduct(Product product);

    }
}
