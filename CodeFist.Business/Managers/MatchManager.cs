namespace CodeFist.Business.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Entities;
    using Data;
    using MatchProcessing;

    public class MatchManager : IMatchManager
    {
        private readonly CodeFistDataContext dataContext;

        public MatchManager(CodeFistDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<Match> QueryMatches()
        {
            return this.dataContext.QueryMatches();
        }

        public Match GetMatch(int id)
        {
            return this.dataContext.QueryMatches().SingleOrDefault(g => g.Id == id);
        }

        public int RunMatch(string gameId, IEnumerable<string> botIds)
        {
            var game = this.dataContext.QueryGames().First(g => g.Id == gameId);

            var bots = game.Bots.Where(b => botIds.Contains(b.BotId)).ToArray();

            var missingBots = botIds.Where(id => bots.All(b => b.BotId != id)).ToArray();

            if (missingBots.Any())
            {
                throw new Exception(string.Format("Invalid bot ids for game {0}: [{1}]", gameId, string.Join(", ", missingBots)));
            }

            var match = MatchRunner.RunMatch(game, bots);

            this.dataContext.InsertMatch(match);

            return match.Id;
        }
    }
}