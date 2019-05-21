namespace MovieFreak.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyRule",
                c => new
                    {
                        MyRuleID = c.Int(nullable: false, identity: true),
                        Support = c.Int(nullable: false),
                        Confidene = c.Int(nullable: false),
                        RuleXID = c.Int(nullable: false),
                        RuleYID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MyRuleID)
                .ForeignKey("dbo.RuleX", t => t.RuleXID, cascadeDelete: true)
                .ForeignKey("dbo.RuleY", t => t.RuleYID, cascadeDelete: true)
                .Index(t => t.RuleXID)
                .Index(t => t.RuleYID);
            
            CreateTable(
                "dbo.RuleX",
                c => new
                    {
                        RuleXID = c.Int(nullable: false, identity: true),
                        MyMovieID = c.Int(nullable: false),
                        MyRuleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RuleXID);
            
            CreateTable(
                "dbo.RuleY",
                c => new
                    {
                        RuleYID = c.Int(nullable: false, identity: true),
                        MyMovieID = c.Int(nullable: false),
                        MyRuleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RuleYID);
            
            AddColumn("dbo.MyMovie", "RuleX_RuleXID", c => c.Int());
            AddColumn("dbo.MyMovie", "RuleY_RuleYID", c => c.Int());
            CreateIndex("dbo.MyMovie", "RuleX_RuleXID");
            CreateIndex("dbo.MyMovie", "RuleY_RuleYID");
            AddForeignKey("dbo.MyMovie", "RuleX_RuleXID", "dbo.RuleX", "RuleXID");
            AddForeignKey("dbo.MyMovie", "RuleY_RuleYID", "dbo.RuleY", "RuleYID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyRule", "RuleYID", "dbo.RuleY");
            DropForeignKey("dbo.MyMovie", "RuleY_RuleYID", "dbo.RuleY");
            DropForeignKey("dbo.MyRule", "RuleXID", "dbo.RuleX");
            DropForeignKey("dbo.MyMovie", "RuleX_RuleXID", "dbo.RuleX");
            DropIndex("dbo.MyRule", new[] { "RuleYID" });
            DropIndex("dbo.MyRule", new[] { "RuleXID" });
            DropIndex("dbo.MyMovie", new[] { "RuleY_RuleYID" });
            DropIndex("dbo.MyMovie", new[] { "RuleX_RuleXID" });
            DropColumn("dbo.MyMovie", "RuleY_RuleYID");
            DropColumn("dbo.MyMovie", "RuleX_RuleXID");
            DropTable("dbo.RuleY");
            DropTable("dbo.RuleX");
            DropTable("dbo.MyRule");
        }
    }
}
