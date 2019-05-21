namespace MovieFreak.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserRating", "Rating", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserRating", "Rating", c => c.Single(nullable: false));
        }
    }
}
