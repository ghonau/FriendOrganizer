namespace FriendOrganizer.DataAccess.Migrations
{
    using FriendOrganizer.Model;

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
                new Model.Friend { FirstName = "Urs", LastName = "Meier" },
                new Model.Friend { FirstName = "Erkan", LastName = "Egin" },
                new Model.Friend { FirstName = "Sara", LastName = "Huber" }
                );
            context.programmingLanguages.AddOrUpdate(f => f.Name,
                new Model.ProgrammingLanguage { Name = "C#" },
                new Model.ProgrammingLanguage { Name = "TypeScript" },
                new Model.ProgrammingLanguage { Name = "F#" },
                new Model.ProgrammingLanguage { Name = "Java" },
                new Model.ProgrammingLanguage { Name = "Swift" });

            context.SaveChanges();
          
            context.FriendPhoneNumbers.AddOrUpdate(pn => pn.Number,
                new Model.FriendPhoneNumber { Number = "+49 12345678", FriendId = context.Friends.First().Id });

            context.Meetings.AddOrUpdate(m => m.Title, new
            Meeting
            {
                Title = "Watching Soccer",
                DateFrom = new DateTime(2018, 5, 12),
                DateTo = new DateTime(2018, 5, 12),
                Friends = new List<Friend>
                {
                    context.Friends.Single(f=>f.FirstName == "Thomas" && f.LastName == "Huber"),
                    context.Friends.Single(f=>f.FirstName == "Urs" && f.LastName == "Meier"),
                }
            });
        }
    }
}
