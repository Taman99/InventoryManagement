#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserProfileService.Context;
using UserProfileService.Entities;
using UserProfileService.Repository;

namespace UserProfileService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfilesController : ControllerBase
    {
        private readonly IUserProfileRepository _repo ;

        public UserProfilesController(IUserProfileRepository repo)
        {
            _repo = repo;
        }


        // GET: api/UserProfiles/5
        [HttpGet("{userId}")]
        public ActionResult<UserProfile> GetUserProfile(string userId)
        {
            try
            {
                var userProfile = _repo.GetUserProfileByUserId(userId);
                return userProfile;
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
           
        }

        // PUT: api/UserProfiles/5
        [HttpPut("{userId}")]
        public IActionResult UpdateUserProfile(string userId, UserProfile userProfile)
        {
            if (userId != userProfile.UserId)
            {
                return BadRequest();
            }

            try
            {
                var isUpdated = _repo.UpdateUserProfile(userProfile);
                if (isUpdated)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return Conflict();
            }

        }

        // POST: api/UserProfiles
        [HttpPost]
        public ActionResult<UserProfile> CreateUserProfile(UserProfile userProfile)
        {
            
            try
            {
                var isCreated = _repo.CreateUserProfile(userProfile);
                if (isCreated)
                {
                    return CreatedAtAction("GetUserProfile", new { userId = userProfile.UserId }, userProfile);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                
                return Conflict();
               
            }          
        }

    }
}
