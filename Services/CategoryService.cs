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
    public class CategoryService : ICategoryService
    {

        private readonly VeterinaryEntities _context;

        public CategoryService(VeterinaryEntities context)
        {
            _context = context;
        }
        public async Task<Category> CreateCategory(Category category)
        {
            Category c = await _context.Category.FirstOrDefaultAsync(cat => cat.CategoryDescription.Equals(category.CategoryDescription));
            if (c == null)
            {
                _context.Category.Add(category);
                _context.SaveChanges();
                return category;

            }

            return c;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Category.OrderBy(c => c.CategoryDescription).ToListAsync();
        }

        public async Task<Category> GetCategoryById(Guid categoryId)
        {
            return await _context.Category.FirstOrDefaultAsync(c => c.IDCategory.Equals(categoryId));
        }
    }
}