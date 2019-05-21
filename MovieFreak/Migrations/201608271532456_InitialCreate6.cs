namespace MovieFreak.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate6 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RuleXMyMovie", newName: "RuleXMyMovie1");
            RenameTable(name: "dbo.RuleYMyMovie", newName: "RuleYMyMovie1");
            CreateTable(
                "dbo.RuleXMyMovie",
                c => new
                    {
                        RuleX_RuleXID = c.Int(nullable: false),
                        MyMovie_MyMovieID = c.Int(nullable: false),
                        MyMovie_MyMovieID1 = c.Int(),
                        RuleX_RuleXID1 = c.Int(),
                    })
                .PrimaryKey(t => new { t.RuleX_RuleXID, t.MyMovie_MyMovieID })
                .ForeignKey("dbo.MyMovie", t => t.MyMovie_MyMovieID1)
                .ForeignKey("dbo.RuleX", t => t.RuleX_RuleXID1)
                .Index(t => t.MyMovie_MyMovieID1)
                .Index(t => t.RuleX_RuleXID1);
            
            CreateTable(
                "dbo.RuleYMyMovie",
                c => new
                    {
                        RuleY_RuleYID = c.Int(nullable: false),
                        MyMovie_MyMovieID = c.Int(nullable: false),
                        MyMovie_MyMovieID1 = c.Int(),
                        RuleY_RuleXID = c.Int(),
                    })
                .PrimaryKey(t => new { t.RuleY_RuleYID, t.MyMovie_MyMovieID })
                .ForeignKey("dbo.MyMovie", t => t.MyMovie_MyMovieID1)
                .ForeignKey("dbo.RuleX", t => t.RuleY_RuleXID)
                .Index(t => t.MyMovie_MyMovieID1)
                .Index(t => t.RuleY_RuleXID);
            
            AlterColumn("dbo.MyRule", "Support", c => c.Double(nullable: false));
            AlterColumn("dbo.MyRule", "Confidene", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RuleYMyMovie", "RuleY_RuleXID", "dbo.RuleX");
            DropForeignKey("dbo.RuleYMyMovie", "MyMovie_MyMovieID1", "dbo.MyMovie");
            DropForeignKey("dbo.RuleXMyMovie", "RuleX_RuleXID1", "dbo.RuleX");
            DropForeignKey("dbo.RuleXMyMovie", "MyMovie_MyMovieID1", "dbo.MyMovie");
            DropIndex("dbo.RuleYMyMovie", new[] { "RuleY_RuleXID" });
            DropIndex("dbo.RuleYMyMovie", new[] { "MyMovie_MyMovieID1" });
            DropIndex("dbo.RuleXMyMovie", new[] { "RuleX_RuleXID1" });
            DropIndex("dbo.RuleXMyMovie", new[] { "MyMovie_MyMovieID1" });
            AlterColumn("dbo.MyRule", "Confidene", c => c.Int(nullable: false));
            AlterColumn("dbo.MyRule", "Support", c => c.Int(nullable: false));
            DropTable("dbo.RuleYMyMovie");
            DropTable("dbo.RuleXMyMovie");
            RenameTable(name: "dbo.RuleYMyMovie1", newName: "RuleYMyMovie");
            RenameTable(name: "dbo.RuleXMyMovie1", newName: "RuleXMyMovie");
        }
    }
}
