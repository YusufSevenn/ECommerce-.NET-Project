using Microsoft.AspNetCore.Identity;

namespace ECommerce.Core.Entities
{
    public class Role : IdentityRole<String>
    {
        //IdentityRole sınıfından gelen Id, Name, NormalizedName ve ConcurrencyStamp bulunur.
        //Bunlar yeterlidir.
    }
}