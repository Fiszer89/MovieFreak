namespace MovieFreak.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MyMovie", "RuleX_RuleXID", "dbo.RuleX");
            DropForeignKey("dbo.MyMovie", "RuleY_RuleYID", "dbo.RuleY");
            DropIndex("dbo.MyMovie", new[] { "RuleX_RuleXID" });
            DropIndex("dbo.MyMovie", new[] { "RuleY_RuleYID" });
            CreateTable(
                "dbo.RuleXMyMovie",
                c => new
                    {
                        RuleX_RuleXID = c.Int(nullable: false),
                        MyMovie_MyMovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RuleX_RuleXID, t.MyMovie_MyMovieID })
                .ForeignKey("dbo.RuleX", t => t.RuleX_RuleXID, cascadeDelete: true)
                .ForeignKey("dbo.MyMovie", t => t.MyMovie_MyMovieID, cascadeDelete: true)
                .Index(t => t.RuleX_RuleXID)
                .Index(t => t.MyMovie_MyMovieID);
            
            CreateTable(
                "dbo.RuleYMyMovie",
                c => new
                    {
                        RuleY_RuleYID = c.Int(nullable: false),
                        MyMovie_MyMovieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RuleY_RuleYID, t.MyMovie_MyMovieID })
                .ForeignKey("dbo.RuleY", t => t.RuleY_RuleYID, cascadeDelete: true)
                .ForeignKey("dbo.MyMovie", t => t.MyMovie_MyMovieID, cascadeDelete: true)
                .Index(t => t.RuleY_RuleYID)
                .Index(t => t.MyMovie_MyMovieID);
            
            DropColumn("dbo.MyMovie", "RuleX_RuleXID");
            DropColumn("dbo.MyMovie", "RuleY_RuleYID");
            DropColumn("dbo.RuleX", "MyMovieID");
            DropColumn("dbo.RuleX", "MyRuleID");
            DropColumn("dbo.RuleY", "MyMovieID");
            DropColumn("dbo.RuleY", "MyRuleID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RuleY", "MyRuleID", c => c.Int(nullable: false));
            AddColumn("dbo.RuleY", "MyMovieID", c => c.Int(nullable: false));
            AddColumn("dbo.RuleX", "MyRuleID", c => c.Int(nullable: false));
            AddColumn("dbo.RuleX", "MyMovieID", c => c.Int(nullable: false));
            AddColumn("dbo.MyMovie", "RuleY_RuleYID", c => c.Int());
            AddColumn("dbo.MyMovie", "RuleX_RuleXID", c => c.Int());
            DropForeignKey("dbo.RuleYMyMovie", "MyMovie_MyMovieID", "dbo.MyMovie");
            DropForeignKey("dbo.RuleYMyMovie", "RuleY_RuleYID", "dbo.RuleY");
            DropForeignKey("dbo.RuleXMyMovie", "MyMovie_MyMovieID", "dbo.MyMovie");
            DropForeignKey("dbo.RuleXMyMovie", "RuleX_RuleXID", "dbo.RuleX");
            DropIndex("dbo.RuleYMyMovie", new[] { "MyMovie_MyMovieID" });
            DropIndex("dbo.RuleYMyMovie", new[] { "RuleY_RuleYID" });
            DropIndex("dbo.RuleXMyMovie", new[] { "MyMovie_MyMovieID" });
            DropIndex("dbo.RuleXMyMovie", new[] { "RuleX_RuleXID" });
            DropTable("dbo.RuleYMyMovie");
            DropTable("dbo.RuleXMyMovie");
            CreateIndex("dbo.MyMovie", "RuleY_RuleYID");
            CreateIndex("dbo.MyMovie", "RuleX_RuleXID");
            AddForeignKey("dbo.MyMovie", "RuleY_RuleYID", "dbo.RuleY", "RuleYID");
            AddForeignKey("dbo.MyMovie", "RuleX_RuleXID", "dbo.RuleX", "RuleXID");
        }
    }
}
