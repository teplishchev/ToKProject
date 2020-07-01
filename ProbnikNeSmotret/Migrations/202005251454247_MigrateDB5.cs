namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PersonalAreas", "AspNetUserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PersonalAreas", "AspNetUserId", c => c.Int(nullable: false));
        }
    }
}
