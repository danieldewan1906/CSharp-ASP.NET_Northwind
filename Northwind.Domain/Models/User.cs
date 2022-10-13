using Microsoft.AspNetCore.Identity;

namespace Northwind.Domain.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
