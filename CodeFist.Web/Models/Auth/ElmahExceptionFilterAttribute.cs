namespace CodeFist.Web.Models.Auth
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Filters;
    using Elmah;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public sealed class ElmahExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            
            var exception = actionExecutedContext.Exception;
            if (exception != null)
            {
                RaiseOrLog(exception, actionExecutedContext);
            }
            else
            {
                if (actionExecutedContext.Response.StatusCode < HttpStatusCode.InternalServerError)
                    return;

                RaiseOrLog(new HttpException((int)actionExecutedContext.Response.StatusCode, ResolveMessage(actionExecutedContext)), actionExecutedContext);
            }
        }

        private static string ResolveMessage(HttpActionExecutedContext actionExecutedContext)
        {
            var reasonPhrase = actionExecutedContext.Response.ReasonPhrase;
            
            var objectContent = actionExecutedContext.Response.Content as ObjectContent<HttpError>;
            if (objectContent == null)
                return reasonPhrase;

            var httpError = objectContent.Value as HttpError;
            if (httpError == null || !httpError.ContainsKey("Message"))
                return reasonPhrase;

            var str = httpError["Message"] as string;
            return !string.IsNullOrWhiteSpace(str) 
                ? str 
                : reasonPhrase;
        }

        private static void RaiseOrLog(Exception exception, HttpActionExecutedContext actionExecutedContext)
        {
            if (RaiseErrorSignal(exception) || IsFiltered(actionExecutedContext))
                return;
            LogException(exception);
        }

        private static bool RaiseErrorSignal(Exception e)
        {
            HttpContext current = HttpContext.Current;
            if (current == null)
                return false;
            HttpApplication applicationInstance = HttpContext.Current.ApplicationInstance;
            if (applicationInstance == null)
                return false;
            ErrorSignal errorSignal = ErrorSignal.Get(applicationInstance);
            if (errorSignal == null)
                return false;
            errorSignal.Raise(e, current);
            return true;
        }

        private static bool IsFiltered(HttpActionExecutedContext context)
        {
            var filterConfiguration = HttpContext.Current.GetSection("elmah/errorFilter") as ErrorFilterConfiguration;
            if (filterConfiguration == null)
                return false;
            
            var assertionHelperContext = new ErrorFilterModule.AssertionHelperContext(context.Exception, HttpContext.Current);
            return filterConfiguration.Assertion.Test(assertionHelperContext);
        }

        private static void LogException(Exception e)
        {
            var current = HttpContext.Current;
            ErrorLog.GetDefault(current).Log(new Elmah.Error(e, current));
        }
    }
}
