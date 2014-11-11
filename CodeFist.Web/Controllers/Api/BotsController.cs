namespace CodeFist.Web.Controllers.Api
{
    using System.Collections;
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Http;
    using Business.Managers;
    using Core.Entities;
    using Models.Auth;

    [RoutePrefix("api/games/{gameId}/bots")]
    public class BotsController : ApiController
    {
        private readonly IBotManager botManager;
        private readonly IAuthenticationManager authenticationManager;

        public BotsController(IBotManager botManager, IAuthenticationManager authenticationManager)
        {
            this.botManager = botManager;
            this.authenticationManager = authenticationManager;
        }

        // GET api/games/SuperGame/bots
        [Route("")]
        public IEnumerable GetGameBots(string gameId)
        {
            return this.botManager.QueryBots()
                .Include(b => b.MatchPlayers)
                .Where(b => b.Game.Id == gameId)
                .Select(x => new
                {
                    x.BotId, 
                    x.DisplayName, 
                    x.UserId,
                    UserName = x.User.DisplayName,
                    Wins = x.MatchPlayers.Count(p => p.Winner), 
                    Losses = x.MatchPlayers.Count(p => !p.Winner)
                });
        }

        // POST api/games/SuperGame/bots
        [Route("")]
        [RequireAccess("CreateBot", "gameId")]
        public IHttpActionResult Post(string gameId, [FromBody]string name)
        {
            if (string.IsNullOrEmpty(this.authenticationManager.CurrentUserId))
                return this.Unauthorized();

            var bot = this.botManager.CreateBot(gameId, this.authenticationManager.CurrentUserId, name);

            return this.CreatedAtRoute("GetBot", new { bot.BotId }, new { bot.GameId, bot.BotId, bot.UserId, bot.DisplayName, bot.Source });
        }

        // GET api/games/SuperGame/bots/AmazeBot
        [Route("{botId}", Name = "GetBot")]
        public IHttpActionResult GetBot(string gameId, string botId)
        {
            var bot = this.botManager.GetBot(gameId, botId);
            return this.Ok(new { bot.GameId, bot.BotId, bot.UserId, bot.DisplayName, bot.Source });
        }

        // PUT api/games/SuperGame/bots/AmazeBot
        [Route("{botId}")]
        [RequireAccess("UpdateBot", "gameId", "botId")]
        public IHttpActionResult Put(string gameId, string botId, Bot value)
        {
            this.botManager.UpdateBot(gameId, botId, value);
            return this.Ok();
        }

        // DELETE api/games/SuperGame/bots/AmazeBot
        [Route("{botId}")]
        [RequireAccess("DeleteBot", "botId")]
        public IHttpActionResult Delete(string gameId, string botId)
        {
            this.botManager.DeleteBot(gameId, botId);
            return this.Ok();
        }

        // GET api/users/Wally/bots
        [Route("~/api/users/{userId}/bots")]
        public IEnumerable GetUserBots(string userId)
        {
            return this.botManager.QueryBots().Where(b => b.User.Id == userId)
                .Select(x => new { x.GameId, x.BotId, x.DisplayName });
        }
    }
}
