using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Veterinary.Interfaces;
using Veterinary.Models;

namespace Veterinary.Services
{
    public class ProductService : IProductService
    {
        private readonly VeterinaryEntities _context;

        public ProductService(VeterinaryEntities context)
        {
            _context = context;
        }
        public async Task<Product> CreateProduct(Product product)
        {
            Product p = await _context.Product.FirstOrDefaultAsync(pr => pr.ProductName == product.ProductName);
            if(p == null)
            {
                p = _context.Product.Add(product);   
                await _context.SaveChangesAsync();
            }

            return p;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(Guid categoryId)
        {
            return await _context.Product.Where(p => p.IDCategoria.Equals(categoryId)).ToListAsync();
        }

        public async Task<Product> GetProductById(Guid productId)
        {
            return await _context.Product.FirstOrDefaultAsync(p => p.IDProduct.Equals(productId));
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Product.ToListAsync();
        }
    }
}