using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FindnChitChat.Model
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRole { get; set; }
    }
}