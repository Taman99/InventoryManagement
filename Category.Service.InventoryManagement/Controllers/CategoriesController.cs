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

namespace Category.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryContext _context;

        public CategoriesController(CategoryContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        /// <summary>
        /// Get list of all categories
        /// </summary>
        /// <returns>List of categories</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblCategory>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        /// <summary>
        /// Get category by category id
        /// </summary>
        /// <param name="id">category id</param>
        /// <returns>Category</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TblCategory>> GetTblCategory(int id)
        {
            var tblCategory = await _context.Categories.FindAsync(id);

            if (tblCategory == null)
            {
                return NotFound();
            }

            return tblCategory;
        }

        // PUT: api/Categories/5
        /// <summary>
        /// Update category information
        /// </summary>
        /// <param name="id">category id</param>
        /// <param name="tblCategory"> Category object</param>
        /// <returns> status code </returns>
       [HttpPut("{id}")]
        public async Task<IActionResult> PutTblCategory(int id, TblCategory tblCategory)
        {
            if (id != tblCategory.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(tblCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblCategoryExists(id))
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
        public async Task<ActionResult<TblCategory>> PostTblCategory(TblCategory tblCategory)
        {
            _context.Categories.Add(tblCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblCategory", new { id = tblCategory.CategoryId }, tblCategory);
        }

        // DELETE: api/Categories/5
        /// <summary>
        /// Delete existing Category
        /// </summary>
        /// <param name="id"> category id </param>
        /// <returns> status code   </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblCategory(int id)
        {
            var tblCategory = await _context.Categories.FindAsync(id);
            if (tblCategory == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(tblCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblCategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
