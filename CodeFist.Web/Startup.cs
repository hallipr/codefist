using CodeFist.Web;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace CodeFist.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AuthConfig.ConfigureAuth(app);
        }
    }
}

