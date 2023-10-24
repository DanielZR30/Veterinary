using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinary.Models;

namespace Veterinary.Interfaces
{
    internal interface ICategoryService
    {
        Task<Category> CreateCategory(Category category);

        Task<Category> GetCategoryById(Guid categoryId);

        Task<IEnumerable<Category>> GetCategories();
    }
}
