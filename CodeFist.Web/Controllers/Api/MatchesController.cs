namespace CodeFist.Web.Controllers.Api
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Business.Managers;
    using Models.Auth;

    [RoutePrefix("api/matches")]
    public class MatchesController : ApiController
    {
        private readonly IMatchManager matchManager;

        public MatchesController(IMatchManager matchManager)
        {
            this.matchManager = matchManager;
        }

        // GET api/games/SuperGame/bots
        [Route("~/api/games/{gameId}/matches")]
        public IEnumerable GetGameMatches(string gameId)
        {
            return this.matchManager.QueryMatches()
                .Where(b => b.Game.Id == gameId)
                .Select(x => new
                {
                    x.Id,
                    Players = x.MatchPlayers.Select(b => new
                    {
                        b.BotId,
                        b.Bot.DisplayName,
                        b.Bot.UserId,
                        b.Winner,
                        UserName = b.Bot.User.DisplayName,
                    })
                });
        }

        // POST api/games/SuperGame/bots
        [Route("~/api/games/{gameId}/matches")]
        [RequireAccess("CreateMatch", "gameId", "botIds")]
        public IHttpActionResult Post(string gameId, [FromBody]IEnumerable<string> botIds)
        {
            var id = this.matchManager.RunMatch(gameId, botIds);

            return this.CreatedAtRoute("GetMatch", new { id }, id);
        }

        // GET api/matches/1324
        [Route("{id}", Name = "GetMatch")]
        public IHttpActionResult GetMatch(int id)
        {
            var match = this.matchManager.GetMatch(id);
            var game = match.Game;

            var result = new
                {
                    match.Id,
                    match.Log,
                    Game = new
                    {
                        game.Id,
                        game.DisplayName,
                        game.VisualizerSource
                    },
                    Players = match.MatchPlayers.Select(b => new
                    {
                        b.Bot.BotId,
                        b.Bot.DisplayName,
                        b.Bot.UserId,
                        b.Winner,
                        UserName = b.Bot.User.DisplayName,
                    })
                };

            return Ok(result);
        }
    }
}
