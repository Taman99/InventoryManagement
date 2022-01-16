    #nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CategoryService.Context;
using CategoryService.Entities;
using CategoryService.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using System.IdentityModel.Tokens.Jwt;

namespace CategoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("CorsPolicy")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repo;

        public CategoriesController(ICategoryRepository repo)
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

        // GET: api/Categories
        /// <summary>
        /// Get list of all categories
        /// </summary>
        /// <returns>List of categories</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            var userId = getUserIdFromJwtToken();
            var categories = _repo.GetCategories(userId);
            return categories.ToList();
        }

        // GET: api/Categories/5
        /// <summary>
        /// Get category by category id
        /// </summary>
        /// <param name="id">category id</param>
        /// <returns>Category</returns>
        [HttpGet("{categoryId}")]
        public ActionResult<Category> GetCategoryById(int categoryId)
        {
            try
            {
                var category = _repo.GetCategoryById(categoryId);
                return category;
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT: api/Categories/5
        /// <summary>
        /// Update category information
        /// </summary>
        /// <param name="id">category id</param>
        /// <param name="Category"> Category object</param>
        /// <returns> status code </returns>
        [HttpPut("{categoryId}")]
        public IActionResult PutCategory(int categoryId, Category category)
        {
            if (categoryId != category.CategoryId)
            {
                return BadRequest();
            }

            try
            {
                var IsUpdated = _repo.UpdateCategory(category);
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine("Error while Updating : " + e.Message);
                if (!CategoryExists(categoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Categories
        /// <summary>
        ///  Create new category
        /// </summary>
        /// <param name="Category"> Category object</param>
        /// <returns> Category object </returns>
        [HttpPost]
        public ActionResult<Category> PostCategory(Category category)
        {
            try
            {
                var userId = getUserIdFromJwtToken();
                category.UserId = userId;
                var isCreated = _repo.CreateCategory(category);

                if (isCreated)
                {
                    return CreatedAtAction("GetCategoryById", new { categoryId = category.CategoryId }, category);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while creating : " + e.Message);
                return BadRequest();
            }

        }

        // DELETE: api/Categories/5
        /// <summary>
        /// Delete existing Category
        /// </summary>
        /// <param name="id"> category id </param>
        /// <returns> status code   </returns>
        [HttpDelete("{categoryId}")]
        public IActionResult DeleteCategory(int categoryId)
        {
            var isDeleted = _repo.DeleteCategory(categoryId);
            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok();
        }

        private bool CategoryExists(int categoryId)
        {
            var exists = _repo.CategoryExists(categoryId);
            return exists;
        }
    }
}
