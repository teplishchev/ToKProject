namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB15 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.QuestionStructures", newName: "QuestionStructs");
            DropForeignKey("dbo.QuestionStructures", "TestId", "dbo.Tests");
            DropIndex("dbo.QuestionStructs", new[] { "TestId" });
            DropColumn("dbo.QuestionStructs", "TestId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionStructs", "TestId", c => c.Int(nullable: false));
            CreateIndex("dbo.QuestionStructs", "TestId");
            AddForeignKey("dbo.QuestionStructures", "TestId", "dbo.Tests", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.QuestionStructs", newName: "QuestionStructures");
        }
    }
}
