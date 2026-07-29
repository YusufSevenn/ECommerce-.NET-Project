using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging; //Ilogger için gerekli

namespace ECommerce.API.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //Hatayı Serilog ile terminale ve logs dosyasına yazıyoruz
                _logger.LogError(ex, "Sistemde beklenmeyen bir hata oluştu! Request Path: {Path}", context.Request.Path);

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Dönüş tipinin JSON olacağını belirtiyoruz
            context.Response.ContentType = "application/json";

            //Hata tipini varsayılan olarak 500 (Internal Server Error) yapıyoruz
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            //Dışarıya dönülecek standart hata modeli 
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Sunucu tarafında beklenmeyen bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.",

                //Geliştirme aşamasında hatanın detayını görmek için bunu ekliyoruz.
                //Canlı ortamda (Production) güvenlik sebebiyle bu detay gizlenmelidi.
                Detailed = exception.Message
            };

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}