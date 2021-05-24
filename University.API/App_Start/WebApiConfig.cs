using System.Web.Http;
using System.Web.Http.Cors;
using University.API.Controllers;

namespace University.API
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web            
            //var enableCorsAttribute = new EnableCorsAttribute("*", 
            //    "Origin, Content-Type, Accept",
            //    "GET, PUT, POST, DELETE, OPTIONS");
            var enableCorsAttribute = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(enableCorsAttribute);

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.MessageHandlers.Add(new TokenValidationHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
