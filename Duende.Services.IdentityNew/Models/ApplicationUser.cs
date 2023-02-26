using Microsoft.AspNetCore.Identity;

namespace Duende.Services.IdentityNew.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
