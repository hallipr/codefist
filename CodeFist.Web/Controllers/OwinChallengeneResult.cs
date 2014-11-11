using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;

namespace CodeFist.Web.Controllers
{
    internal class OwinChallengeneResult : ActionResult
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OwinChallengeneResult"/> class.
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        /// <param name="returnUrl">
        /// The return url.
        /// </param>
        public OwinChallengeneResult(string provider, string returnUrl)
        {
            this.Provider = provider;
            this.ReturnUrl = returnUrl;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the provider.
        /// </summary>
        public string Provider { get; private set; }

        /// <summary>
        /// Gets the return url.
        /// </summary>
        public string ReturnUrl { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The execute result.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public override void ExecuteResult(ControllerContext context)
        {
            var owinContext = context.HttpContext.GetOwinContext();
            owinContext.Authentication.Challenge(new AuthenticationProperties { RedirectUri = this.ReturnUrl }, this.Provider);
        }

        #endregion
    }
}