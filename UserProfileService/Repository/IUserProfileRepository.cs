using UserProfileService.Entities;

namespace UserProfileService.Repository
{
    public interface IUserProfileRepository
    {
        UserProfile GetUserProfileByUserId(string userId);

        bool CreateUserProfile(UserProfile userProfile);

        bool UpdateUserProfile(UserProfile userProfile);
    }
}
