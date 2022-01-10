using UserProfileService.Context;
using UserProfileService.Entities;
using Microsoft.EntityFrameworkCore;
using UserProfileService.Repository;

namespace UserProfileService.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly InventoryManagementContext _context;

        public UserProfileRepository(InventoryManagementContext context)
        {
            _context = context;
        }

        // if present, Get user profile from DB , else, create new user profile
        public UserProfile GetUserProfileByUserId(string userId)
        {
            var userProfile = _context.UserProfiles.Find(userId);
            if(userProfile != null)
            {
                return userProfile;
            }

            var emptyUserProfile = new UserProfile();
            
            CreateUserProfile(userId , emptyUserProfile);
            return emptyUserProfile;
            
        }

        // Create new user profile in DB
        public bool CreateUserProfile(string userId, UserProfile userProfile)
        {
            userProfile.UserId = userId;
            _context.UserProfiles.Add(userProfile);
            return Commit();
        }

        // Update user profile in DB
        public bool UpdateUserProfile(UserProfile userProfile)
        {
            _context.Entry(userProfile).State = EntityState.Modified;
            return Commit();
        }   

        // Save changes to DB
        private bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

    }
}
