namespace CodeFist.Data
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using Core.Entities;

    static class EntityConfigurations
    {
        public static EntityTypeConfiguration<Game> Game()
        {
            // Common
            var entityType = new EntityTypeConfiguration<Game>();
            entityType.HasKey(x => x.Id);
            entityType.Property(x => x.Id).HasMaxLength(32);

            // Properties
            entityType.Property(x => x.GameSource).IsMaxLength();
            entityType.Property(x => x.VisualizerSource).IsMaxLength();
            entityType.Property(x => x.BotSource).IsMaxLength();

            // Navigation Properties
            entityType.HasMany(x => x.Bots).WithRequired(b => b.Game);
            entityType.HasMany(x => x.Matches).WithRequired(b => b.Game);

            return entityType;
        }

        public static EntityTypeConfiguration<Bot> Bot()
        {
            // Common
            var entityType = new EntityTypeConfiguration<Bot>();
            entityType.HasKey(x => new { x.GameId, x.BotId });
            entityType.Property(x => x.BotId).HasMaxLength(32);

            // Properties
            entityType.Property(x => x.Source).IsMaxLength();

            // Navigation Properties
            entityType.HasMany(x => x.MatchPlayers).WithRequired(b => b.Bot).WillCascadeOnDelete(false);
            
            return entityType;
        }

        public static EntityTypeConfiguration<Match> Match()
        {
            // Common
            var entityType = new EntityTypeConfiguration<Match>();
            entityType.HasKey(x => x.Id);
            entityType.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Navigation Properties
            entityType.HasMany(x => x.MatchPlayers).WithRequired(b => b.Match);

            return entityType;
        }
        public static EntityTypeConfiguration<MatchPlayer> MatchPlayer()
        {
            // Common
            var entityType = new EntityTypeConfiguration<MatchPlayer>();
            entityType.HasKey(x => new { x.BotId, x.GameId, x.MatchId });

            // Properties
            entityType.Property(x => x.PrivateLog).IsMaxLength();

            return entityType;
        }

        public static EntityTypeConfiguration<User> User()
        {
            // Common
            var entityType = new EntityTypeConfiguration<User>();
            entityType.HasKey(x => x.Id);
            entityType.Property(x => x.Id).HasMaxLength(32);
            
            // Navigation Properties
            entityType.HasMany(x => x.Logins).WithRequired(b => b.User);
            entityType.HasMany(x => x.Bots).WithRequired(b => b.User);
            entityType.HasMany(x => x.Games).WithMany(b => b.Developers);
            return entityType;
        }

        public static EntityTypeConfiguration<UserLogin> UserLogin()
        {
            var entityType = new EntityTypeConfiguration<UserLogin>();
            entityType.HasKey(x => new { x.LoginProvider, x.ProviderKey });

            return entityType;
        }
    }
}
