namespace CodeFist.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bots",
                c => new
                    {
                        GameId = c.String(nullable: false, maxLength: 32),
                        BotId = c.String(nullable: false, maxLength: 32),
                        UserId = c.String(nullable: false, maxLength: 32),
                        DisplayName = c.String(),
                        Source = c.String(),
                    })
                .PrimaryKey(t => new { t.GameId, t.BotId })
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        DisplayName = c.String(),
                        GameSource = c.String(),
                        VisualizerSource = c.String(),
                        BotSource = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        DisplayName = c.String(),
                        Enabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserGames",
                c => new
                    {
                        User_Id = c.String(nullable: false, maxLength: 32),
                        Game_Id = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Game_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Game_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.UserGames", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Bots", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bots", "GameId", "dbo.Games");
            DropIndex("dbo.UserGames", new[] { "Game_Id" });
            DropIndex("dbo.UserGames", new[] { "User_Id" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.Bots", new[] { "UserId" });
            DropIndex("dbo.Bots", new[] { "GameId" });
            DropTable("dbo.UserGames");
            DropTable("dbo.UserLogins");
            DropTable("dbo.Users");
            DropTable("dbo.Games");
            DropTable("dbo.Bots");
        }
    }
}
