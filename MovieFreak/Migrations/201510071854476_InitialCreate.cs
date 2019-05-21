namespace MovieFreak.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rule",
                c => new
                    {
                        RuleID = c.Int(nullable: false, identity: true),
                        RuleXid = c.Int(nullable: false),
                        RuleYid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RuleID)
                .ForeignKey("dbo.RuleX", t => t.RuleXid, cascadeDelete: true)
                .ForeignKey("dbo.RuleY", t => t.RuleYid, cascadeDelete: true)
                .Index(t => t.RuleXid)
                .Index(t => t.RuleYid);
            
            CreateTable(
                "dbo.RuleX",
                c => new
                    {
                        RuleXID = c.Int(nullable: false, identity: true),
                        X = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RuleXID);
            
            CreateTable(
                "dbo.RuleY",
                c => new
                    {
                        RuleYID = c.Int(nullable: false, identity: true),
                        Y = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RuleYID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rule", "RuleYid", "dbo.RuleY");
            DropForeignKey("dbo.Rule", "RuleXid", "dbo.RuleX");
            DropIndex("dbo.Rule", new[] { "RuleYid" });
            DropIndex("dbo.Rule", new[] { "RuleXid" });
            DropTable("dbo.RuleY");
            DropTable("dbo.RuleX");
            DropTable("dbo.Rule");
        }
    }
}
