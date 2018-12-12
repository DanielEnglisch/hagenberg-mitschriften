using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CurrencyConverter.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web-API-Konfiguration und -Dienste
            config.EnableCors();

            // Web-API-Routen
            // mit Attributen im Controller kann die Route festgelegt werden
            config.MapHttpAttributeRoutes();

            /*
             * default route
             config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );*/
        }
    }
}
