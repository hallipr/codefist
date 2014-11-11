namespace CodeFist.Core.Entities
{
    using System.Collections.Generic;

    public class Bot
    {
        public string BotId { get; set; }
        public string GameId { get; set; }
        public string UserId { get; set; }
        public string DisplayName { get; set; }

        public string Source { get; set; }

        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<MatchPlayer> MatchPlayers { get; set; }
    }
}
