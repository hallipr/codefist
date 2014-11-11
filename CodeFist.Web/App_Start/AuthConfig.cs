namespace CodeFist.Web
{
    using System;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Owin;
    using Owin.Security.Providers.GitHub;

    using CodeFist.Core;

    public static class AuthConfig
    {
        public const string ExternalCookieAuthType = "ExternalCookie";

        public static void ConfigureAuth(IAppBuilder app)
        {
            var config = AppConfig.Create();

            app.SetDefaultSignInAsAuthenticationType(ExternalCookieAuthType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                ExpireTimeSpan = config.SessionTimeout
            });

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = ExternalCookieAuthType,
                AuthenticationMode = AuthenticationMode.Passive,
                ExpireTimeSpan = TimeSpan.FromMinutes(5.0)
            });

            var gitHubOptions = new GitHubAuthenticationOptions
            {
                ClientId = config.GitHubClientId,
                ClientSecret = config.GitHubClientSecret,
            };

            gitHubOptions.Scope.Clear();

            app.UseGitHubAuthentication(gitHubOptions);
        }
    }
}