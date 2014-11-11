namespace CodeFist.Core.Entities
{
    using System;
    using System.Collections.Generic;

    public class Match
    {
        public int Id { get; set; }
        public string GameId { get; set; }
        public TimeSpan Duration { get; set; }
        public string Log { get; set; }

        public virtual Game Game { get; set; }
        public virtual ICollection<MatchPlayer> MatchPlayers { get; set; }
    }
}
