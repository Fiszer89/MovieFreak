namespace MovieFreak.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.X1",
                c => new
                    {
                        X1ID = c.Int(nullable: false, identity: true),
                        element = c.Int(nullable: false),
                        RuleID = c.Int(nullable: false),
                        ruleX_RuleX1ID = c.Int(),
                    })
                .PrimaryKey(t => t.X1ID)
                .ForeignKey("dbo.RuleX1", t => t.ruleX_RuleX1ID)
                .Index(t => t.ruleX_RuleX1ID);
            
            CreateTable(
                "dbo.RuleX1",
                c => new
                    {
                        RuleX1ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.RuleX1ID);
            
            CreateTable(
                "dbo.MyRule1",
                c => new
                    {
                        MyRule1ID = c.Int(nullable: false, identity: true),
                        RuleX = c.Int(nullable: false),
                        RuleY = c.Int(nullable: false),
                        Rulex_RuleX1ID = c.Int(),
                        Ruley_RuleY1ID = c.Int(),
                    })
                .PrimaryKey(t => t.MyRule1ID)
                .ForeignKey("dbo.RuleX1", t => t.Rulex_RuleX1ID)
                .ForeignKey("dbo.RuleY1", t => t.Ruley_RuleY1ID)
                .Index(t => t.Rulex_RuleX1ID)
                .Index(t => t.Ruley_RuleY1ID);
            
            CreateTable(
                "dbo.RuleY1",
                c => new
                    {
                        RuleY1ID = c.Int(nullable: false, identity: true),
                        Y = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RuleY1ID);
            
            CreateTable(
                "dbo.Y1",
                c => new
                    {
                        Y1ID = c.Int(nullable: false, identity: true),
                        element = c.Int(nullable: false),
                        RuleID = c.Int(nullable: false),
                        ruleY_RuleY1ID = c.Int(),
                    })
                .PrimaryKey(t => t.Y1ID)
                .ForeignKey("dbo.RuleY1", t => t.ruleY_RuleY1ID)
                .Index(t => t.ruleY_RuleY1ID);
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Y",
                c => new
                    {
                        YID = c.Int(nullable: false, identity: true),
                        element = c.Int(nullable: false),
                        RuleID = c.Int(nullable: false),
                        ruleY_RuleYID = c.Int(),
                    })
                .PrimaryKey(t => t.YID);
            
            CreateTable(
                "dbo.RuleY",
                c => new
                    {
                        RuleYID = c.Int(nullable: false, identity: true),
                        Y = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RuleYID);
            
            CreateTable(
                "dbo.MyRule",
                c => new
                    {
                        MyRuleID = c.Int(nullable: false, identity: true),
                        RuleX = c.Int(nullable: false),
                        RuleY = c.Int(nullable: false),
                        Rulex_RuleXID = c.Int(),
                        Ruley_RuleYID = c.Int(),
                    })
                .PrimaryKey(t => t.MyRuleID);
            
            CreateTable(
                "dbo.RuleX",
                c => new
                    {
                        RuleXID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.RuleXID);
            
            CreateTable(
                "dbo.X",
                c => new
                    {
                        XID = c.Int(nullable: false, identity: true),
                        element = c.Int(nullable: false),
                        RuleID = c.Int(nullable: false),
                        ruleX_RuleXID = c.Int(),
                    })
                .PrimaryKey(t => t.XID);
            
            DropForeignKey("dbo.MyRule1", "Ruley_RuleY1ID", "dbo.RuleY1");
            DropForeignKey("dbo.Y1", "ruleY_RuleY1ID", "dbo.RuleY1");
            DropForeignKey("dbo.MyRule1", "Rulex_RuleX1ID", "dbo.RuleX1");
            DropForeignKey("dbo.X1", "ruleX_RuleX1ID", "dbo.RuleX1");
            DropIndex("dbo.Y1", new[] { "ruleY_RuleY1ID" });
            DropIndex("dbo.MyRule1", new[] { "Ruley_RuleY1ID" });
            DropIndex("dbo.MyRule1", new[] { "Rulex_RuleX1ID" });
            DropIndex("dbo.X1", new[] { "ruleX_RuleX1ID" });
            DropTable("dbo.Y1");
            DropTable("dbo.RuleY1");
            DropTable("dbo.MyRule1");
            DropTable("dbo.RuleX1");
            DropTable("dbo.X1");
            CreateIndex("dbo.Y", "ruleY_RuleYID");
            CreateIndex("dbo.MyRule", "Ruley_RuleYID");
            CreateIndex("dbo.MyRule", "Rulex_RuleXID");
            CreateIndex("dbo.X", "ruleX_RuleXID");
            AddForeignKey("dbo.MyRule", "Ruley_RuleYID", "dbo.RuleY", "RuleYID");
            AddForeignKey("dbo.Y", "ruleY_RuleYID", "dbo.RuleY", "RuleYID");
            AddForeignKey("dbo.MyRule", "Rulex_RuleXID", "dbo.RuleX", "RuleXID");
            AddForeignKey("dbo.X", "ruleX_RuleXID", "dbo.RuleX", "RuleXID");
        }
    }
}
