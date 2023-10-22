using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Veterinary.Models;
using Veterinary.Services;
using Veterinary.ViewModels;

namespace Veterinary.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : ApiController
    {
        private readonly VeterinaryEntities _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductsController(VeterinaryEntities context, ProductService productService, CategoryService categoryService)
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
        }

        #region Create
        [HttpPost]
        [Route("create")]
        // TODO: Autorizar roles
        public async Task<IActionResult> CreateCategory([FromBody] ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product product = new Product
                    {
                        ProductName = productViewModel.ProductName,
                        ProductDescription = productViewModel.ProductDescription,
                        ProductPrice = productViewModel.ProductPrice,
                        IDProduct = Guid.NewGuid().ToString(),
                        IDCategoria = productViewModel.IDCategoria.ToString(),
                        Category = await _categoryService.GetCategoryById(productViewModel.IDCategoria)
                    };
                    return (IActionResult)Ok();
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError("Duplicated", "Ya existe una categoría con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("Error", exception.Message);
                }
            }
            return (IActionResult)BadRequest(ModelState.ToString());
        }
        #endregion

    }
}