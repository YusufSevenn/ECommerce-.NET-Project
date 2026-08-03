using Microsoft.AspNetCore.Identity;

namespace ECommerce.Core.Entities
{
    public class User : IdentityUser<string>
    {
        //IdentityUser içerisinde olan alanlar dışında istediğimiz diğer alanları ekliyoruz.
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public DateOnly Birthday { get; set; }
    }
}