namespace CodeFist.Business.MatchProcessing
{
    using System;
    using System.Collections.Generic;

    public class MatchResult
    {
        public IEnumerable<PlayerResult> Players { get; set; }
        public string Log { get; set; }
    }
}