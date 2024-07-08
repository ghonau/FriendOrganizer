namespace FriendOrganizer.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveValidationOnEmail : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Friend", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Friend", "Email", c => c.String(maxLength: 50));
        }
    }
}
