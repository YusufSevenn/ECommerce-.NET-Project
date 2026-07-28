using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerce.API.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        //İstek Controller'a düşmeden hemen önce çalışacak metot
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                //Hataları yakalayıp sadece mesaj kısımlarını bir listeye alıyoruz
                var errors = context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();

                context.Result = new BadRequestObjectResult(new { Errors = errors });
            }
        }
    }
}