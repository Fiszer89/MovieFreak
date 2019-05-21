namespace MovieFreak.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.RuleY1", "Y");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RuleY1", "Y", c => c.Int(nullable: false));
        }
    }
}
