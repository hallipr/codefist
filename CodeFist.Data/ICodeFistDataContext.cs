namespace CodeFist.Data
{
    using System.Linq;
    using Core.Entities;

    public interface ICodeFistDataContext
    {
        IQueryable<Bot> QueryBots();
        void InsertBot(Bot entity);
        void UpdateBot(Bot value);
        void DeleteBot(string gameId, string botId);

        IQueryable<Game> QueryGames();
        void InsertGame(Game entity);
        void UpdateGame(Game entity);
        void DeleteGame(string id);

        IQueryable<User> QueryUsers();
        void InsertUser(User entity);
        void UpdateUser(User entity);
        void DeleteUser(string id);
        IQueryable<UserLogin> QueryLogins();
    }
}