#nullable disable
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    [Authorize]
    [EnableCors("CorsPolicy")]
    public class UserProfilesController : ControllerBase
    {
        private readonly IUserProfileRepository _repo ;
    

        public UserProfilesController(IUserProfileRepository repo)
        {
            _repo = repo;
        }

        //get user id from JWT access token inside authoriztion property of HTTP request Header  
        private string getUserIdFromJwtToken()
        {
            var bearerToken = Request.Headers["Authorization"].ToString();
            var token = bearerToken.Split(' ')[1];
            var jwtToken = new JwtSecurityToken(token);
            var userId = jwtToken.Subject;        
            return userId;
        }

        //get user email from JWT access token 
        private string getUserEmailFromJwtToken()
        {
            string userEmail = null;
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {    
                userEmail = identity.FindFirst("https://example.com/email").Value;
            }
            return userEmail;
        }

       

        // GET: api/UserProfiles
        // Get user profile info of current user
        [HttpGet]
        public ActionResult<UserProfile> GetUserProfile()
        {
            try
            {
                var userId = getUserIdFromJwtToken();
                var userEmail = getUserEmailFromJwtToken();
                var userProfile = _repo.GetUserProfileByUserId(userId, userEmail);
                return userProfile;
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
           
        }

        // PUT: api/UserProfiles
        // Update user profile info 
        [HttpPut]
        public IActionResult UpdateUserProfile(UserProfile userProfile)
        {
            var userId = getUserIdFromJwtToken();
            if (userId != userProfile.UserId)
            {
                Console.WriteLine(userId);
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
        // Create new user profile with email taken from access token 
        [HttpPost]
        public ActionResult<UserProfile> CreateUserProfile(UserProfile userProfile)
        {
            
            try
            {
                var userId = getUserIdFromJwtToken();
                var userEmail = getUserEmailFromJwtToken();
                var isCreated = _repo.CreateUserProfile(userId , userEmail, userProfile);
                if (isCreated)
                {
                    return CreatedAtAction("GetUserProfile", new { }, userProfile);
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

        //GET: api/UserProfiles/userExists
        // check if user information exists in Database
        [HttpGet("UserExists")]
        public IActionResult UserExists()
        {
            try
            {
                var userId = getUserIdFromJwtToken();
                var exists = _repo.UserExists(userId);
                if (exists)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

    }
}
