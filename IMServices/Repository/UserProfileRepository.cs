using IMServices.Context;
using IMServices.Entities;
using Microsoft.EntityFrameworkCore;
using IMServices.Repository;

namespace IMServices.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly InventoryManagementContext _context;

        public UserProfileRepository(InventoryManagementContext context)
        {
            _context = context;
        }

        // if present, Get user profile from DB , else, create new user profile
        public UserProfile GetUserProfileByUserId(string userId, string userEmail)
        {
            var userProfile = _context.UserProfiles.Find(userId);
            if(userProfile != null)
            {
                return userProfile;
            }

            var emptyUserProfile = new UserProfile();
            
            CreateUserProfile(userId, userEmail, emptyUserProfile);
            return emptyUserProfile;
            
        }

        // Create new user profile in DB
        public bool CreateUserProfile(string userId, string userEmail, UserProfile userProfile)
        {
            userProfile.UserId = userId;
            userProfile.UserEmail = userEmail;
            _context.UserProfiles.Add(userProfile);
            return Commit();
        }

        // Update user profile in DB
        public bool UpdateUserProfile(UserProfile userProfile)
        {
            _context.Entry(userProfile).State = EntityState.Modified;
            return Commit();
        }   

        public bool UserExists(string userId)
        {
            var user = _context.UserProfiles.Find(userId);
            var exists = user != null;
            return exists;
        }

        // Save changes to DB
        private bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

    }
}
