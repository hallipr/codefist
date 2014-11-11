namespace CodeFist.Business.Managers
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using Core.Entities;
    using Data;

    public class BotManager : IBotManager
    {
        private readonly CodeFistDataContext dataContext;

        public BotManager(CodeFistDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<Bot> QueryBots()
        {
            return this.dataContext.QueryBots();
        }

        public Bot GetBot(string gameId, string botId)
        {
            return this.dataContext
                .QueryBots()
                .SingleOrDefault(g => g.Game.Id == gameId.ToLower() && g.BotId == botId.ToLower());
        }

        public Bot CreateBot(string gameId, string userId, string botName)
        {
            var shortName = Regex.Replace(botName, "[^A-Za-z0-9~!*()_.-]", string.Empty).ToLower();
            
            var botSource = this.dataContext.QueryGames()
                .Where(g => g.Id == gameId)
                .Select(g => g.BotSource)
                .Single();

            var bot = new Bot
            {
                BotId = shortName,
                GameId = gameId,
                UserId = userId,
                DisplayName = botName,
                Source = botSource
            };

            this.dataContext.InsertBot(bot);

            return bot;
        }

        public void UpdateBot(string gameId, string botId, Bot bot)
        {
            bot.GameId = gameId;
            bot.BotId = botId;

            this.dataContext.UpdateBot(bot);
        }

        public void DeleteBot(string gameId, string botId)
        {
            this.dataContext.DeleteBot(gameId, botId);
        }
    }
}