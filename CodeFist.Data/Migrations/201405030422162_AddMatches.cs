namespace CodeFist.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMatches : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.String(nullable: false, maxLength: 32),
                        Duration = c.Time(nullable: false, precision: 7),
                        Log = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.MatchPlayers",
                c => new
                    {
                        BotId = c.String(nullable: false, maxLength: 32),
                        GameId = c.String(nullable: false, maxLength: 32),
                        MatchId = c.Int(nullable: false),
                        Winner = c.Boolean(nullable: false),
                        PrivateLog = c.String(),
                    })
                .PrimaryKey(t => new { t.BotId, t.GameId, t.MatchId })
                .ForeignKey("dbo.Matches", t => t.MatchId, cascadeDelete: true)
                .ForeignKey("dbo.Bots", t => new { t.GameId, t.BotId })
                .Index(t => new { t.GameId, t.BotId })
                .Index(t => t.MatchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MatchPlayers", new[] { "GameId", "BotId" }, "dbo.Bots");
            DropForeignKey("dbo.Matches", "GameId", "dbo.Games");
            DropForeignKey("dbo.MatchPlayers", "MatchId", "dbo.Matches");
            DropIndex("dbo.MatchPlayers", new[] { "MatchId" });
            DropIndex("dbo.MatchPlayers", new[] { "GameId", "BotId" });
            DropIndex("dbo.Matches", new[] { "GameId" });
            DropTable("dbo.MatchPlayers");
            DropTable("dbo.Matches");
        }
    }
}
