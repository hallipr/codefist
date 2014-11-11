namespace CodeFist.Business.Managers
{
    using System;
    using System.Linq;
    using Core.Entities;

    public interface IGameManager
    {
        IQueryable<Game> QueryGames();
        Game CreateGame(string name, User user);
        Game GetGame(string id);
        void UpdateGame(Game game);
        void DeleteGame(string id);
    }
}