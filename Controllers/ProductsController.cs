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
using Veterinary.Interfaces;
using Veterinary.ViewModels;
using Veterinary.Services;
using Newtonsoft.Json;

namespace Veterinary.Controllers
{
    public class ProductsController : ApiController
    {
        private static readonly VeterinaryEntities _context = new VeterinaryEntities();
        private readonly IProductService _productService = new ProductService(_context);
        private readonly ICategoryService _categoryService = new CategoryService(_context);


        /*
        //preguntar como hacer la inyeccion de dependencias
        public ProductsController(VeterinaryEntities context,
                                  IProductService productService,
                                  ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        */
        #region Create
        [HttpPost]
        [Route("api/products/create")]
        [JsonIgnore]
        public async Task<object> CreateProduct([FromBody] ProductViewModel productViewModel)
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
                        IDProduct = Guid.NewGuid(),
                        IDCategoria = productViewModel.IDCategoria,
                        Category = await _categoryService.GetCategoryById(productViewModel.IDCategoria)
                    };
                    await _productService.CreateProduct(product);
                    return Ok<Product>(product);
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
            return BadRequest(ModelState.ToString());
        }
        #endregion

        #region Read
        [HttpGet]
        [Route("api/products")]
        public async Task<object> GetProducts()
        {
            try
            {
                IEnumerable<Product> products = await _productService.GetProducts();
                return Ok(products);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

    }
}