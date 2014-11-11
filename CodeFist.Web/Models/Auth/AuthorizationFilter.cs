namespace CodeFist.Web.Models.Auth
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    public class AuthorizationFilter : ActionFilterAttribute
    {
        private readonly IAuthorizationManager authorizationManager;

        public AuthorizationFilter(IAuthorizationManager authorizationManager)
        {
            this.authorizationManager = authorizationManager;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var actionAttributes = actionContext.ActionDescriptor.GetCustomAttributes<RequireAccessAttribute>();

            var controllerAttributes =
                actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<RequireAccessAttribute>();

            var requireAccessAttributes = actionAttributes.Concat(controllerAttributes).ToArray();

            var authorized = requireAccessAttributes.All(a => CheckAccess(a, actionContext));

            if (!authorized)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }

            base.OnActionExecuting(actionContext);
        }

        private bool CheckAccess(RequireAccessAttribute accessAttribute, HttpActionContext actionContext)
        {
            try
            {
                var parameters = accessAttribute.Parameters
                   .ToDictionary(s => s.PropertyName, s => actionContext.ActionArguments[s.ArgumentName]);

                return this.authorizationManager.CheckAccess(accessAttribute.AccessType, parameters);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error processing access check: " + accessAttribute.AccessType, ex);
            }
        }
    }
}