namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB15 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuestionStructures", "TestId", "dbo.Tests");
            DropIndex("dbo.QuestionStructures", new[] { "TestId" });
            DropColumn("dbo.QuestionStructures", "TestId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionStructures", "TestId", c => c.Int(nullable: false));
            CreateIndex("dbo.QuestionStructures", "TestId");
            AddForeignKey("dbo.QuestionStructures", "TestId", "dbo.Tests", "Id", cascadeDelete: true);
        }
    }
}
