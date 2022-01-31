using System;
using System.Collections.Generic;

namespace IMServices.Entities
{
    public partial class UserProfile
    {
        public string UserId { get; set; } = null!;
        public string? UserFirstName { get; set; }
        public string? UserLastName { get; set; }
        public string? CompanyName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhoneNo { get; set; }
        public string? Gender { get; set; }
        public string? UserAddress { get; set; }
        public string? UserState { get; set; }
        public string? UserCountry { get; set; }
    }
}
