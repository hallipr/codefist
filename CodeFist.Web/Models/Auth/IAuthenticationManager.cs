namespace CodeFist.Web.Models.Auth
{
    using System;
    using System.Threading.Tasks;
    using Core.Entities;

    public interface IAuthenticationManager
    {
        string CurrentUserId { get; }

        User GetCurrentUser();

        Task<LoginStatus> ExternalLoginAsync();
    }
}