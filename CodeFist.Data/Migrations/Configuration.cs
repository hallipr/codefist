namespace CodeFist.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using Core.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<CodeFist.Data.CodeFistDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
