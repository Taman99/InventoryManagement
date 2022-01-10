using System;
using System.Collections.Generic;

namespace UserProfileService.Entities
{
    public partial class UserProfile
    {
        public string UserId { get; set; } = null!;
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public string? CompanyName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? UserEmail { get; set; }
    }
}
