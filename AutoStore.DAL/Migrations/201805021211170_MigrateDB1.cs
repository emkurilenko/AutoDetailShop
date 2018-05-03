namespace AutoStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ClientProfiles", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientProfiles", "UserName", c => c.String());
        }
    }
}
