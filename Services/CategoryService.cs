﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Veterinary.Models;

namespace Veterinary.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly VeterinaryEntities _context;
        public async Task<Category> CreateCategory(Category category)
        {
            Category c = await _context.Category.FirstOrDefaultAsync(cat => cat.CategoryDescription.Equals(category.CategoryDescription));
            if (c == null)
            {
                return _context.Category.Add(category);
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