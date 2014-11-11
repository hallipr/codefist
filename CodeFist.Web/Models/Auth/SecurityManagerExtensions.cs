namespace CodeFist.Web.Models.Auth
{
    using System;
    using Business;

    public static class SecurityManagerExtensions
    {
        public static void RequireAccess(this IAuthorizationManager authorizationManager, AccessRequest request)
        {
            if (authorizationManager.CheckAccess(request))
                return;

            throw new UnauthorizedAccessException(request.Type);
        }
        
        public static void RequireAccess(this IAuthorizationManager authorizationManager, string accessType, object properties = null)
        {
            var request = AccessRequest.Create(accessType, properties);
            authorizationManager.RequireAccess(request);
        }

        public static bool CheckAccess(this IAuthorizationManager authorizationManager, string accessType, object properties = null)
        {
            var request = AccessRequest.Create(accessType, properties);
            return authorizationManager.CheckAccess(request);
        }
    }
}