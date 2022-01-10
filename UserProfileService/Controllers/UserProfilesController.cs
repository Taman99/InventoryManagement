﻿#nullable disable
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        // GET: api/UserProfiles/5
        [HttpGet("{userId}")]
        public ActionResult<UserProfile> GetUserProfile()
        {
            try
            {
                var userId = getUserIdFromJwtToken();
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
        [HttpPost]
        public ActionResult<UserProfile> CreateUserProfile(UserProfile userProfile)
        {
            
            try
            {
                var userId = getUserIdFromJwtToken();
                
                var isCreated = _repo.CreateUserProfile(userId , userProfile);
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
