namespace MovieFreak.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MyMovie", "support");
            DropColumn("dbo.MyMovie", "confidence");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MyMovie", "confidence", c => c.Int(nullable: false));
            AddColumn("dbo.MyMovie", "support", c => c.Int(nullable: false));
        }
    }
}
