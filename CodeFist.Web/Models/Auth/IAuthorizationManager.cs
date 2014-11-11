namespace CodeFist.Web.Models.Auth
{
    using Business;

    public interface IAuthorizationManager
    {
        bool CheckAccess(AccessRequest request);
    }
}