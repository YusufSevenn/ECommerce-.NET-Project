namespace ECommerce.Application.DTOs
{
    public class AuthResponseDto
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}