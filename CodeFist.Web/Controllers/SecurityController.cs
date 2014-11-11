namespace CodeFist.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using CodeFist.Web.Models;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using Models.Auth;

    [AllowAnonymous]
    public class SecurityController : Controller
    {
        private readonly IOwinContext owinContext;
        private readonly IAuthenticationManager authenticationManager;

        public SecurityController(IOwinContext owinContext, IAuthenticationManager authenticationManager)
        {
            this.owinContext = owinContext;
            this.authenticationManager = authenticationManager;
        }

        public ActionResult Login()
        {
            var loginResult = this.GetLoginStatus();
            
            return loginResult.Success
                ? View("Callback", loginResult) as ActionResult
                : new OwinChallengeneResult("GitHub", Url.Action("Callback"));
        }

        public ActionResult LoginStatus()
        {
            var loginResult = this.GetLoginStatus();

            return new JsonNetResult(loginResult);
        }

        public void Logout()
        {
            owinContext.Authentication.SignOut(
                CookieAuthenticationDefaults.AuthenticationType, 
                AuthConfig.ExternalCookieAuthType);
        }

        public async Task<ActionResult> Callback()
        {
            LoginStatus loginStatus;

            try
            {
                loginStatus = await this.authenticationManager.ExternalLoginAsync();
            }
            catch (Exception ex)
            {
                loginStatus = new LoginStatus { Exception = ex.Message };
            }
            
            return this.View(loginStatus);
        }

        private LoginStatus GetLoginStatus()
        {
            var result = new LoginStatus();

            var currentUser = authenticationManager.GetCurrentUser();

            if (currentUser == null)
            {
                return result;
            }

            result.Enabled = currentUser.Enabled;
            result.Success = true;
            result.UserId = currentUser.Id;
            result.UserDisplayName = currentUser.DisplayName;

            return result;
        }
    }
}