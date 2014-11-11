namespace CodeFist.Web
{
    using System.Web.Http;
    using System.Web.Mvc;
    using Models.Auth;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.Filters.Add(DependencyResolver.Current.GetService<AuthorizationFilter>());
            config.Filters.Add(new ElmahExceptionFilterAttribute());
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
