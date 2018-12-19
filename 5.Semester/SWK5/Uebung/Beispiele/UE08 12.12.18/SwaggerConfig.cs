using System.Web.Routing;
using NSwag.AspNet.Owin;

namespace CurrencyConverter.WebApi
{
    public class SwaggerConfig
    {
        public static void RegisterSwagger(RouteCollection routes)
        {
            routes.MapOwinPath("swagger", app =>
            {
                app.UseSwaggerUi3(typeof(SwaggerConfig).Assembly, settings =>
                {
                    settings.GeneratorSettings.Title = "CurrencyConverter API";
                    settings.MiddlewareBasePath = "/swagger";
                });
            });
        }
    }
}