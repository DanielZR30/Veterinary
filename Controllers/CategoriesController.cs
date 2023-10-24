using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Veterinary.Interfaces;
using Veterinary.Models;
using Veterinary.Services;

namespace Veterinary.Controllers
{

    [EnableCors(origins: "http://localhost:50804", headers: "*", methods: "*")]
    public class CategoriesController : ApiController
    {
        private static readonly VeterinaryEntities _context = new VeterinaryEntities();
        private readonly ICategoryService _categoryService = new CategoryService(_context);

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
