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
using System.Web.Http.Cors;

namespace Veterinary.Controllers
{

    [EnableCors(origins: "http://localhost:54641", headers: "*", methods: "*")]
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

        [HttpGet]
        [Route("api/products/category/{IDCategory}")]
        public async Task<object> GetProductsByCategory(Guid IDCategory)
        {
            try
            {
                IEnumerable<Product> products = await _productService.GetProductByCategory(IDCategory);
                return Ok(products);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("api/products/{IDProduct}")]
        public async Task<object> GetProductsById(Guid IDProduct)
        {
            try
            {
                Product product = await _productService.GetProductById(IDProduct);
                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region Update
        [HttpPut]
        [Route("api/products/{IDProduct}")]
        public async Task<object> UpdateProduct(Guid IDProduct, [FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product p = await _productService.UpdateProduct(product);
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

        #region Delete

        [HttpDelete]
        [Route("api/products/{IDProduct}")]
        public async Task<object> DeleteProduct(Guid IDProduct, [FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product p = await _productService.DeleteProduct(product);
                    return Ok(p);
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
    }
}