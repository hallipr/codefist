namespace CodeFist.Business.Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Entities;

    public interface IMatchManager
    {
        IQueryable<Match> QueryMatches();
        Match GetMatch(int id);
        int RunMatch(string gameId, IEnumerable<string> botIds);
    }
}