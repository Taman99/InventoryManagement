#nullable disable
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMServices.Entities;
using IMServices.Repository;

namespace IMServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("CorsPolicy")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;

        //Repository
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }

        //Get Bearer token for authorization
        private string getUserIdFromJwtToken()
        {
            var bearerToken = Request.Headers["Authorization"].ToString();
            var token = bearerToken.Split(' ')[1];
            var jwtToken = new JwtSecurityToken(token);
            var userId = jwtToken.Subject;
            return userId;
        }

        // API call for getting all products according to merchant id
        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var userId = getUserIdFromJwtToken();
            var products = _repo.GetProducts(userId);
            return Ok(products);
        }

        // API call for getting individual products
        // GET: api/Products/5
        [HttpGet("{productId}")]
        public ActionResult<Product> GetProduct(string productId)
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

        //API call for editing products
        // PUT: api/Products/5
        [HttpPut("{productId}")]
        public IActionResult PutProduct(string productId, Product product)
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
        
        //API call for Creating products
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

        //API call for Deleting products
        // DELETE: api/Products/5
        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(string productId)
        {
            var isDeleted = _repo.DeleteProduct(productId);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }

        //Function to check if product exists
        private bool ProductExists(string productId)
        {
            var exists = _repo.ProductExists(productId);
            return exists;
        }
    }
}
