#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Category.Service.Context;
using Category.Service.Entities;
using Category.Service.Repository;

namespace Category.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repo;

        public CategoriesController(ICategoryRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Categories
        /// <summary>
        /// Get list of all categories
        /// </summary>
        /// <returns>List of categories</returns>
        [HttpGet]
        public  ActionResult<IEnumerable<TblCategory>> GetCategories()
        {
            var categories = _repo.GetCategories();
            return Ok(categories);
        }

        // GET: api/Categories/5
        /// <summary>
        /// Get category by category id
        /// </summary>
        /// <param name="id">category id</param>
        /// <returns>Category</returns>
        [HttpGet("{categoryId}")]
        public ActionResult<TblCategory> GetTblCategory(int categoryId)
        {
            try { 
            var category = _repo.GetCategoriesById(categoryId);
                return category;
            }
            catch(Exception)
            {
                return NotFound();
            }        
        }

        // PUT: api/Categories/5
        /// <summary>
        /// Update category information
        /// </summary>
        /// <param name="id">category id</param>
        /// <param name="tblCategory"> Category object</param>
        /// <returns> status code </returns>
        [HttpPut("{categoryId}")]
        public IActionResult PutTblCategory(int categoryId, TblCategory category)
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

            return NoContent();
        }

        // POST: api/Categories
        /// <summary>
        ///  Create new category
        /// </summary>
        /// <param name="tblCategory"> Category object</param>
        /// <returns> Category object </returns>
        [HttpPost]
        public ActionResult<TblCategory> PostTblCategory(TblCategory category)
        {
            try
            {
                var isCreated = _repo.CreateCategory(category);

                if (isCreated)
                {
                    return CreatedAtAction("GetTblCategory", new { categoryId = category.CategoryId }, category);
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
        public IActionResult DeleteTblCategory(int categoryId)
        {
            var isDeleted = _repo.DeleteCategory(categoryId);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool CategoryExists(int categoryId)
        {
            var exists = _repo.CategoryExists(categoryId);
            return exists;
        }
    }
}
