using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SizesService.Context;
using SizesService.Entities;
using SizesService.Repository;

namespace SizesService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    [EnableCors("CorsPolicy")]
    public class SizesController : ControllerBase
    {
        private readonly ISizesRepository _repo;

        public SizesController(ISizesRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Size>> GetSizes()
        {
            var sizes = _repo.GetSizes();
            return Ok(sizes);
        }

        // GET: api/Sizes/5
        /// <summary>
        /// Get sizes by product id
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>Category</returns>
        [HttpGet("{productId}")]
        public ActionResult<IEnumerable<Size>> GetSizeByProductId(int productId)
        {
            try
            {
                var size = _repo.GetSizeByProductId(productId);
                return Ok(size);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT: api/Sizes/5
        /// <summary>
        /// Update size details
        /// </summary>
        /// <param name="id">size id</param>
        /// <param name="Size"> Size object</param>
        /// <returns> status code </returns>
        [HttpPut("{sizeId}")]
        public IActionResult UpdateSize(int sizeId, Size size)
        {
            if (sizeId != size.SizeId)
            {
                return BadRequest();
            }

            try
            {
                var isUpdated = _repo.UpdateSize(size);
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine("Error while Updating : " + e.Message);
                if (!SizeExists(sizeId))
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

        // POST: api/Sizes
        /// <summary>
        ///  Create new size
        /// </summary>
        /// <param name="Size"> Size object</param>
        /// <returns> Size object </returns>
        [HttpPost]
        public ActionResult<Size> CreateSize(Size size)
        {
            try
            {
                var isCreated = _repo.CreateSize(size);

                if (isCreated)
                {
                    return CreatedAtAction("CreateSize", new { sizeId = size.SizeId }, size);
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while creating : " + e.Message);
                return BadRequest();
            }

        }

        // DELETE: api/Sizes/5
        /// <summary>
        /// Delete existing size
        /// </summary>
        /// <param name="id"> size id </param>
        /// <returns> status code   </returns>
       
        [HttpDelete("{sizeId}")]
        public IActionResult DeleteSizeBySizeId(int sizeId)
        {
            var isDeleted = _repo.DeleteSizeBySizeId(sizeId);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Sizes/5
        /// <summary>
        /// Delete existing size by ProductId
        /// </summary>
        /// <param name="id"> product id </param>
        /// <returns> status code   </returns>
        [HttpDelete("{productId}")]
        public IActionResult DeleteSizesByProductId(int productId)
        {
            var isDeleted = _repo.DeleteSizesByProductId(productId);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool SizeExists(int sizeId)
        {
            var exists = _repo.SizeExists(sizeId);
            return exists;
        }

    }
}
