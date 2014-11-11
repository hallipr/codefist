namespace CodeFist.Business.Managers
{
    using Core.Entities;

    public interface IUserManager
    {
        User CreateUser(string id, string loginProvider, string providerKey);
        User GetUser(string id);
        User GetUser(string loginProvider, string providerKey);
        void UpdateUser(string id, User user);
        void DeleteUser(string id);
    }
}