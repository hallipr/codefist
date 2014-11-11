namespace CodeFist.Web.Models.Auth
{
    using System.Collections.Generic;
    using System.Linq;
    using Business;
    using Business.Managers;
    using Core;

    public class AuthorizationManager : IAuthorizationManager
    {
        private readonly IGameManager gameManager;
        private readonly IBotManager botManager;
        private readonly AppConfig appConfig;
        private readonly IAuthenticationManager authenticationManager;
        private readonly Dictionary<string, SecurityRule[]> securityRules;

        public AuthorizationManager(AppConfig appConfig, IBotManager botManager, IGameManager gameManager, IAuthenticationManager authenticationManager)
        {
            this.gameManager = gameManager;
            this.botManager = botManager;
            this.appConfig = appConfig;
            this.authenticationManager = authenticationManager;
            this.securityRules = this.GameRules().Concat(this.BotRules())
                .GroupBy(r => r.ClaimType.ToUpper())
                .ToDictionary(g => g.Key, g => g.ToArray());
        }

        private IEnumerable<SecurityRule> BotRules()
        {
            yield return new SecurityRule("UpdateBot", this.CanModifyBot);
            yield return new SecurityRule("DeleteBot", this.CanModifyBot);
        }

        private IEnumerable<SecurityRule> GameRules()
        {
            yield return new SecurityRule("UpdateGame", this.CanModifyGame);
            yield return new SecurityRule("DeleteGame", this.CanModifyGame);
        }

        private bool CanModifyBot(AccessRequest request)
        {
            var gameId = request.Property<string>("gameId");
            var botId = request.Property<string>("botId");
            var existingBot = this.botManager.GetBot(gameId, botId);

            return existingBot != null && existingBot.UserId == this.authenticationManager.CurrentUserId;
        }

        private bool CanModifyGame(AccessRequest request)
        {
            var gameId = request.Property<string>("id");

            var developers = this.gameManager.QueryGames()
                .Where(g => g.Id == gameId)
                .SelectMany(g => g.Developers);

            return developers.Any(d => d.Id == this.authenticationManager.CurrentUserId);
        }

        public bool CheckAccess(AccessRequest request)
        {
            var ruleName = request.Type.ToUpper();
            return this.securityRules.ContainsKey(ruleName)
                ? this.securityRules[ruleName].All(r => r.AccessCheck(request)) 
                : this.appConfig.AllowAccessByDefault;
        }
    }
}