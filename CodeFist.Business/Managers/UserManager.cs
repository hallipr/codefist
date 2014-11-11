namespace CodeFist.Business.Managers
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using Core.Entities;
    using Data;

    public class UserManager : IUserManager
    {
        private readonly CodeFistDataContext dataContext;

        public UserManager(CodeFistDataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        
        public User GetUser(string id)
        {
            return this.dataContext.QueryUsers().SingleOrDefault(g => g.Id == id.ToLower());
        }

        public User GetUser(string loginProvider, string providerKey)
        {
            return this.dataContext.QueryLogins()
                .Where(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey)
                .Select(l => l.User)
                .SingleOrDefault();
        }

        public User CreateUser(string name, string loginProvider, string providerKey)
        {
            var id = Regex.Replace(name, "[^A-Za-z0-9~!*()_.-]", string.Empty).ToLower();
            
            var user = new User
            {
                Id = id,
                DisplayName = name,
                Enabled = true,
                Logins = new[] {  new UserLogin { LoginProvider = loginProvider, ProviderKey = providerKey } }
            };

            this.dataContext.InsertUser(user);

            return user;
        }

        public void UpdateUser(string id, User user)
        {
            user.Id = id;
            this.dataContext.UpdateUser(user);
        }

        public void DeleteUser(string id)
        {
            this.dataContext.DeleteUser(id.ToLower());
        }
    }
}