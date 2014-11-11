namespace CodeFist.Data
{
    using System.Data.Entity;
    using System.Linq;
    using Core.Entities;
    using Migrations;

    public class CodeFistDataContext : DbContext
    {
        public IDbSet<Game> Games { get; set; }
        public IDbSet<Bot> Bots { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Match> Matches { get; set; }
        public IDbSet<UserLogin> Logins { get; set; }

        public CodeFistDataContext() : base("CodeFistConnectionString")
        {
        }
 
        public CodeFistDataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations
                .Add(EntityConfigurations.Game())
                .Add(EntityConfigurations.Bot())
                .Add(EntityConfigurations.User())
                .Add(EntityConfigurations.UserLogin())
                .Add(EntityConfigurations.Match())
                .Add(EntityConfigurations.MatchPlayer());
        }      

        public IQueryable<Bot> QueryBots()
        {
            return this.Bots;
        }

        public void InsertBot(Bot entity)
        {
            this.Bots.Add(entity);
            this.SaveChanges();
        }

        public void UpdateBot(Bot entity)
        {
            this.Bots.Attach(entity);
            this.Entry(entity).State = EntityState.Modified;
            this.SaveChanges();
        }

        public void DeleteBot(string gameId, string botId)
        {
            var entity = new Bot { GameId = gameId, BotId = botId };
            this.Bots.Attach(entity);
            this.Bots.Remove(entity);
            this.SaveChanges();
        }


        public IQueryable<Game> QueryGames()
        {
            return this.Games;
        }

        public void InsertGame(Game entity)
        {
            this.Games.Add(entity);
            this.SaveChanges();
        }

        public void UpdateGame(Game entity)
        {
            this.Games.Attach(entity);
            this.Entry(entity).State = EntityState.Modified;
            this.SaveChanges();
        }

        public void DeleteGame(string id)
        {
            var entity = new Game { Id = id };
            this.Games.Attach(entity);
            this.Games.Remove(entity);
            this.SaveChanges();
        }


        public IQueryable<User> QueryUsers()
        {
            return this.Users;
        }

        public void InsertUser(User entity)
        {
            this.Users.Add(entity);
            this.SaveChanges();
        }

        public void UpdateUser(User entity)
        {
            this.Users.Attach(entity);
            this.Entry(entity).State = EntityState.Modified;
            this.SaveChanges();
        }

        public void DeleteUser(string id)
        {
            var entity = new User { Id = id };
            this.Users.Attach(entity);
            this.Users.Remove(entity);
            this.SaveChanges();
        }


        public IQueryable<UserLogin> QueryLogins()
        {
            return this.Logins;
        }

        public IQueryable<Match> QueryMatches()
        {
            return this.Matches;
        }

        public void InsertMatch(Match entity)
        {
            this.Matches.Add(entity);
            this.SaveChanges();
        }

        public void UpdateMatch(Match entity)
        {
            this.Matches.Attach(entity);
            this.Entry(entity).State = EntityState.Modified;
            this.SaveChanges();
        }

        public void DeleteMatch(int id)
        {
            var entity = new Match { Id = id };
            this.Matches.Attach(entity);
            this.Matches.Remove(entity);
            this.SaveChanges();
        }

        public static void SetInitializer()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CodeFistDataContext, Configuration>());
        }
    }
}