namespace MovieFreak.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate7 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.RuleYMyMovie", name: "RuleY_RuleXID", newName: "RuleY_RuleYID1");
            RenameIndex(table: "dbo.RuleYMyMovie", name: "IX_RuleY_RuleXID", newName: "IX_RuleY_RuleYID1");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.RuleYMyMovie", name: "IX_RuleY_RuleYID1", newName: "IX_RuleY_RuleXID");
            RenameColumn(table: "dbo.RuleYMyMovie", name: "RuleY_RuleYID1", newName: "RuleY_RuleXID");
        }
    }
}
