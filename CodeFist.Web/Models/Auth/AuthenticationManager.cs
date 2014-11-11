namespace CodeFist.Web.Models.Auth
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Business.Managers;
    using Core.Entities;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;

    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IUserManager userManager;
        private readonly Func<IOwinContext> owinContextFactory;

        public AuthenticationManager(IUserManager userManager, Func<IOwinContext> owinContextFactory)
        {
            this.userManager = userManager;
            this.owinContextFactory = owinContextFactory;
        }
        
        public async Task<LoginStatus> ExternalLoginAsync()
        {
            var loginResult = new LoginStatus();

            var owinContext = this.owinContextFactory();
            var authResult = await owinContext.Authentication.AuthenticateAsync(AuthConfig.ExternalCookieAuthType);
            var loginData = ExternalLoginData.FromIdentity(authResult.Identity);

            if (loginData == null || loginData.ProviderKey == null)
                return loginResult;
            
            var user = this.userManager.GetUser(loginData.LoginProvider, loginData.ProviderKey) ??
                       this.userManager.CreateUser(loginData.UserName, loginData.LoginProvider, loginData.ProviderKey);

            loginResult.UserId = user.Id;
            loginResult.UserDisplayName = user.DisplayName;
            loginResult.Enabled = user.Enabled;
            loginResult.Success = true;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id, "Guid", "CodeFist"),
                new Claim(ClaimTypes.Name, user.DisplayName, "String", "CodeFist")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);

            owinContext.Authentication.SignOut(AuthConfig.ExternalCookieAuthType);
            
            if(user.Enabled)
            {
                owinContext.Authentication.SignIn(identity);
            }

            return loginResult;
        }

        public string CurrentUserId
        {
            get
            {
                if (this.owinContextFactory == null)
                    return null;

                var owinContext = this.owinContextFactory();
                var userIdClaim = owinContext.Authentication.User.FindFirst(ClaimTypes.NameIdentifier);

                return userIdClaim != null ? userIdClaim.Value : null;
            }
        }
        
        public User GetCurrentUser()
        {
            var currentUserId = this.CurrentUserId;

            return string.IsNullOrEmpty(currentUserId) ? null : this.userManager.GetUser(currentUserId);
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                var providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirst(ClaimTypes.Name).Value
                };
            }
        }

    }
}