namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "Name", c => c.String());
        }
        
        //public override void Down()
        //{
        //    DropColumn("dbo.Tests", "Name");
        //}
    }
}
