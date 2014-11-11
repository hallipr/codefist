namespace CodeFist.Business.Managers
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using Core;
    using Core.Entities;
    using Data;

    public class GameManager : IGameManager
    {
        private readonly CodeFistDataContext dataContext;

        public GameManager(CodeFistDataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<Game> QueryGames()
        {
            return this.dataContext.QueryGames();
        }

        public Game GetGame(string id)
        {
            return this.dataContext.QueryGames().SingleOrDefault(g => g.Id == id.ToLower());
        }

        public Game CreateGame(string name, User user)
        {
            var id = Regex.Replace(name, "[^A-Za-z0-9~!*()_.-]", string.Empty).ToLower();
            
            var game = new Game
            {
                Id = id,
                DisplayName = name,
                GameSource = Resources.SampleGameSource,
                VisualizerSource = Resources.SampleVisualizerSource,
                BotSource = Resources.SampleBotSource,
                Developers = new[] { user }
            };

            this.dataContext.InsertGame(game);

            return game;
        }

        public void UpdateGame(Game game)
        {
            this.dataContext.UpdateGame(game);
        }

        public void DeleteGame(string id)
        {
            this.dataContext.DeleteGame(id.ToLower());
        }
    }
}