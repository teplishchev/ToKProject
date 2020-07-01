namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB15 : DbMigration
    {
        public override void Up()
        {
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.QuestionStructures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdQuestion = c.Int(nullable: false),
                        TestStructureId = c.Int(nullable: false),
                        NumInTest = c.Int(nullable: false),
                        Passed = c.Boolean(nullable: false),
                        TestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.QuestionStructures", "TestId");
            CreateIndex("dbo.QuestionStructures", "TestStructureId");
            AddForeignKey("dbo.QuestionStructures", "TestStructureId", "dbo.TestStructures", "Id", cascadeDelete: true);
            AddForeignKey("dbo.QuestionStructures", "TestId", "dbo.Tests", "Id", cascadeDelete: true);
        }
    }
}
