namespace CodeFist.Core.Entities
{
    using System.Collections.Generic;

    public class Game
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }

        public string GameSource { get; set; }
        public string VisualizerSource { get; set; }
        public string BotSource { get; set; }

        public virtual ICollection<Bot> Bots { get; set; }
        public virtual ICollection<User> Developers { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}
