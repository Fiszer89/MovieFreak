namespace MovieFreak.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.X1", "ruleX_RuleX1ID", "dbo.RuleX1");
            DropForeignKey("dbo.MyRule1", "Rulex_RuleX1ID", "dbo.RuleX1");
            DropForeignKey("dbo.Y1", "ruleY_RuleY1ID", "dbo.RuleY1");
            DropForeignKey("dbo.MyRule1", "Ruley_RuleY1ID", "dbo.RuleY1");
            DropIndex("dbo.X1", new[] { "ruleX_RuleX1ID" });
            DropIndex("dbo.MyRule1", new[] { "Rulex_RuleX1ID" });
            DropIndex("dbo.MyRule1", new[] { "Ruley_RuleY1ID" });
            DropIndex("dbo.Y1", new[] { "ruleY_RuleY1ID" });
            DropTable("dbo.X1");
            DropTable("dbo.RuleX1");
            DropTable("dbo.MyRule1");
            DropTable("dbo.RuleY1");
            DropTable("dbo.Y1");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Y1",
                c => new
                    {
                        Y1ID = c.Int(nullable: false, identity: true),
                        element = c.Int(nullable: false),
                        RuleID = c.Int(nullable: false),
                        ruleY_RuleY1ID = c.Int(),
                    })
                .PrimaryKey(t => t.Y1ID);
            
            CreateTable(
                "dbo.RuleY1",
                c => new
                    {
                        RuleY1ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.RuleY1ID);
            
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
                .PrimaryKey(t => t.MyRule1ID);
            
            CreateTable(
                "dbo.RuleX1",
                c => new
                    {
                        RuleX1ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.RuleX1ID);
            
            CreateTable(
                "dbo.X1",
                c => new
                    {
                        X1ID = c.Int(nullable: false, identity: true),
                        element = c.Int(nullable: false),
                        RuleID = c.Int(nullable: false),
                        ruleX_RuleX1ID = c.Int(),
                    })
                .PrimaryKey(t => t.X1ID);
            
            CreateIndex("dbo.Y1", "ruleY_RuleY1ID");
            CreateIndex("dbo.MyRule1", "Ruley_RuleY1ID");
            CreateIndex("dbo.MyRule1", "Rulex_RuleX1ID");
            CreateIndex("dbo.X1", "ruleX_RuleX1ID");
            AddForeignKey("dbo.MyRule1", "Ruley_RuleY1ID", "dbo.RuleY1", "RuleY1ID");
            AddForeignKey("dbo.Y1", "ruleY_RuleY1ID", "dbo.RuleY1", "RuleY1ID");
            AddForeignKey("dbo.MyRule1", "Rulex_RuleX1ID", "dbo.RuleX1", "RuleX1ID");
            AddForeignKey("dbo.X1", "ruleX_RuleX1ID", "dbo.RuleX1", "RuleX1ID");
        }
    }
}
