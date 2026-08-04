using ECommerce.Core.Entities;

namespace ECommerce.Application.AInterfaces
{
    public interface ITokenService
    {
        //Kullanıcı bilgsini alıp geriye JWT token string'i dönecek
        Task<string> CreateTokenAsync(User user);
    }
}