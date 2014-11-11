namespace CodeFist.Core.Entities
{
    public class MatchPlayer
    {
        public string BotId { get; set; }
        public string GameId { get; set; }
        public int MatchId { get; set; }
        public bool Winner { get; set; }
        public string PrivateLog { get; set; }
        public virtual Bot Bot { get; set; }
        public virtual Match Match { get; set; }
    }
}