using IMServices.Entities;

namespace IMServices.Repository
{
    public interface IUserProfileRepository
    {
        UserProfile GetUserProfileByUserId(string userId, string userEmail);

        bool CreateUserProfile(string userId, string userEmail, UserProfile userProfile);

        bool UpdateUserProfile(UserProfile userProfile);

        public bool UserExists(string userId);
    }
}
