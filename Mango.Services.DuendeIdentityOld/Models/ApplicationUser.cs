using Microsoft.AspNetCore.Identity;

namespace Mango.Services.DuendeIdentityOld.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
