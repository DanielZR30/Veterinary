using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Veterinary.Interfaces;
using Veterinary.Models;
using Veterinary.Services;
using Veterinary.ViewModels;

namespace Veterinary.Controllers
{

    [EnableCors(origins: "http://localhost:54641", headers: "*", methods: "*")]
    public class CategoriesController : ApiController
    {
        private static readonly VeterinaryEntities _context = new VeterinaryEntities();
        private readonly ICategoryService _categoryService = new CategoryService(_context);

        [HttpPost]
        [Route("api/categories/create")]
        public async Task<object> CreateCategory([FromBody] CategoryViewModel categoryViewModel)
        {
            try{
                Category category = new Category
                {
                    IDCategory = Guid.NewGuid(),
                    CategoryDescription = categoryViewModel.CategoryDescription
                };
                await _categoryService.CreateCategory(category);
                return Ok("Categoria agregada correctamente");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("api/categories")]
        public async Task<IHttpActionResult> GetCategories()
        {
            try
            {
                IEnumerable<Category> categories = await _categoryService.GetCategories();
                return Ok(categories);
            }catch(Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
