namespace CodeFist.Business.Managers
{
    using System.Linq;
    using Core.Entities;

    public interface IBotManager
    {
        IQueryable<Bot> QueryBots();
        Bot CreateBot(string gameId, string userId, string botName);
        Bot GetBot(string gameId, string botId);
        void UpdateBot(string gameId, string botId, Bot value);
        void DeleteBot(string gameId, string botId);
    }
}