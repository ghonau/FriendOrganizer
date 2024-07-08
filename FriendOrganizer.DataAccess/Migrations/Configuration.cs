namespace FriendOrganizer.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FriendOrganizer.DataAccess.FriendOrganizerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FriendOrganizer.DataAccess.FriendOrganizerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.Friends.AddOrUpdate(f => f.FirstName,
                new Model.Friend { FirstName = "Thomas", LastName = "Huber" },
                new Model.Friend { FirstName = "Urs", LastName = "Meir" },
                new Model.Friend { FirstName = "Erkan", LastName = "Egin" },
                new Model.Friend { FirstName = "Sara", LastName = "Huber" }
                );
            context.programmingLanguages.AddOrUpdate(f => f.Name,
                new Model.ProgrammingLanguage { Name = "C#" },
                new Model.ProgrammingLanguage { Name = "TypeScript" },
                new Model.ProgrammingLanguage { Name = "F#" },
                new Model.ProgrammingLanguage { Name = "Java" },
                new Model.ProgrammingLanguage { Name = "Swift" });
        }
    }
}
