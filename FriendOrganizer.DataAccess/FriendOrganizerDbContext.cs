using FriendOrganizer.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FriendOrganizer.DataAccess
{
    public class FriendOrganizerDbContext :DbContext
    {
        public FriendOrganizerDbContext() : base("FriendOrganizerDb")
        {
            
        }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<ProgrammingLanguage> programmingLanguages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
            
        }        

    }
}
