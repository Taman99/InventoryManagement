#nullable disable
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Context;
using ProductService.Entities;
using ProductService.Repository;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("CorsPolicy")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        private string getUserIdFromJwtToken()
        {
            var bearerToken = Request.Headers["Authorization"].ToString();
            var token = bearerToken.Split(' ')[1];
            var jwtToken = new JwtSecurityToken(token);
            var userId = jwtToken.Subject;
            return userId;
        }


        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var userId = getUserIdFromJwtToken();
            var products = _repo.GetProducts(userId);
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{productId}")]
        public ActionResult<Product> GetProduct(int productId)
        {
            try
            {
                var product = _repo.GetProductById(productId);
                return product;
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        // PUT: api/Products/5
        [HttpPut("{categoryId}")]
        public IActionResult PutProduct(int productId, Product product)
        {
            if (productId != product.ProductId)
            {
                return BadRequest();
            }

            try
            {
                var isUpdated = _repo.UpdateProduct(product);
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine("Error while Updating : " + e.Message);
                if (!ProductExists(productId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Products
         [HttpPost]
        public ActionResult<Product> PostProduct(Product product)
        {
            try
            {
                var userId = getUserIdFromJwtToken();
                var isCreated = _repo.CreateProduct(product, userId);

                if (isCreated)
                {
                    return CreatedAtAction("GetProduct", new { productId = product.ProductId }, product);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while creating : " + e.Message);
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(int productId)
        {
            var isDeleted = _repo.DeleteProduct(productId);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool ProductExists(int productId)
        {
            var exists = _repo.ProductExists(productId);
            return exists;
        }
    }
}
