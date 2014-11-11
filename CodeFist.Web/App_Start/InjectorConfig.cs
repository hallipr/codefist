namespace CodeFist.Web
{
    using System;
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using Business;
    using Business.Managers;
    using Core;
    using Data;
    using Microsoft.Owin;
    using Models.Auth;
    using SimpleInjector;
    using SimpleInjector.Integration.Web.Mvc;
    using SimpleInjector.Integration.WebApi;

    public static class InjectorConfig
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void RegisterContainer(HttpConfiguration configuration)
        {
            var container = new Container();

            container.RegisterSingle<AppConfig>(AppConfig.Create());
            container.Register<HttpContextBase>(() => new HttpContextWrapper(HttpContext.Current));
            container.Register<IOwinContext>(() => container.GetInstance<HttpContextBase>().GetOwinContext());
            container.Register<Func<IOwinContext>>(() => container.GetInstance<IOwinContext>);
            container.RegisterPerWebRequest<CodeFistDataContext>(() => new CodeFistDataContext("CodeFistConnectionString"));
            container.Register<IAuthenticationManager, AuthenticationManager>();
            container.Register<IAuthorizationManager, AuthorizationManager>();
            container.Register<AuthorizationFilter>();
            container.Register<IBotManager, BotManager>();
            container.Register<IUserManager, UserManager>();
            container.Register<IGameManager, GameManager>();
            container.Register<IMatchManager, MatchManager>();
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterWebApiControllers(configuration);

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}