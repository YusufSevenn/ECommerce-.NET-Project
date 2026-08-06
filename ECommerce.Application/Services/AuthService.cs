using ECommerce.Application.DTOs;
using ECommerce.Application.AInterfaces;
using ECommerce.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;

        // Dependency Injection
        public AuthService(UserManager<User> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            // Aynı e-posta ile kayıtlı kullanıcı var mı kontrolü
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return new AuthResponseDto
                {
                    IsSuccessful = false,
                    Message = "Bu e-posta adresi zaten kullanılıyor."
                };
            }

            //Yeni kullanıcı nesnesini oluşturma
            var user = new User
            {
                Email = registerDto.Email,
                UserName = registerDto.Username,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Address = registerDto.Address,
                Gender = registerDto.Gender,
                Birthday = registerDto.Birthday
                //Şifreyi buraya yazmıyoruz, UserManager onu hash'leyerek kaydedecek
            };

            // Kullanıcıyı kaydetme işlemi 
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return new AuthResponseDto
                {
                    IsSuccessful = true,
                    Message = "Kullanıcı başarıyla oluşturuldu."
                };
            }

            //Hata varsa Identity'nin döndüğü ilk hatayı mesaj olarak veriyoruz
            return new AuthResponseDto
            {
                IsSuccessful = false,
                Message = result.Errors.FirstOrDefault()?.Description ?? "Kayıt işlemi başarısız oldu."
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            // Kullanıcıyı e-posta üzerinden bulma
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return new AuthResponseDto
                {
                    IsSuccessful = false,
                    Message = "Kullanıcı bulunamadı."
                };
            }

            // Şifre kontrolü (Identity şifreyi kendi hash algoritmasıyla karşılaştırır)
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordValid)
            {
                return new AuthResponseDto
                {
                    IsSuccessful = false,
                    Message = "Şifre hatalı."
                };
            }

            //Şifre doğruysa Token üretimi 
            var token = await _tokenService.CreateTokenAsync(user);

            return new AuthResponseDto
            {
                IsSuccessful = true,
                Message = "Giriş başarılı.",
                Token = token
            };
        }
    }
}