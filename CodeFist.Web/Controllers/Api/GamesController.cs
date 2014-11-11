namespace CodeFist.Web.Controllers.Api
{
    using System.Collections;
    using System.Linq;
    using System.Web.Http;
    using Business.Managers;
    using Core.Entities;
    using Models.Auth;

    [RoutePrefix("api/games")]
    public class GamesController : ApiController
    {
        private readonly IGameManager gameManager;
        private readonly IAuthenticationManager authenticationManager;

        public GamesController(IGameManager gameManager, IAuthenticationManager authenticationManager)
        {
            this.gameManager = gameManager;
            this.authenticationManager = authenticationManager;
        }

        // GET api/games
        public IEnumerable Get()
        {
            var games = this.gameManager.QueryGames();
            return games.Select(g => new { g.Id, g.DisplayName });
        }

        // POST api/games
        [RequireAccess("CreateGame")]
        public IHttpActionResult Post([FromBody]string name)
        {
            var currentUser = this.authenticationManager.GetCurrentUser();

            if (currentUser == null)
                return this.Unauthorized();

            var game = this.gameManager.CreateGame(name, currentUser);

            return this.CreatedAtRoute("GetGame", new { game.Id }, new { game.Id, game.DisplayName });
        }

        // GET api/games/123
        [Route("{id}", Name = "GetGame")]
        public IHttpActionResult Get(string id)
        {
            var game = this.gameManager.GetGame(id);
            return Ok(new { game.Id, game.DisplayName, game.GameSource, game.VisualizerSource, game.BotSource });
        }

        // PUT api/games/coolgame
        [Route("{id}")]
        [RequireAccess("UpdateGame", "id")]
        public void Put(string id, Game value)
        {
            value.Id = id.ToLower();
            this.gameManager.UpdateGame(value);
        }

        // DELETE api/games/coolgame
        [Route("{id}")]
        [RequireAccess("UpdateGame", "id")]
        public void Delete(string id)
        {
            this.gameManager.DeleteGame(id);
        }
    }
}
