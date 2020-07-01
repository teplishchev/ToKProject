namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB9 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionStructures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Passed = c.Boolean(nullable: false),
                        TestId = c.Int(nullable: false),
                        TestStructure_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .ForeignKey("dbo.TestStructures", t => t.TestStructure_Id)
                .Index(t => t.TestId)
                .Index(t => t.TestStructure_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionStructures", "TestStructure_Id", "dbo.TestStructures");
            DropForeignKey("dbo.QuestionStructures", "TestId", "dbo.Tests");
            DropIndex("dbo.QuestionStructures", new[] { "TestStructure_Id" });
            DropIndex("dbo.QuestionStructures", new[] { "TestId" });
            DropTable("dbo.QuestionStructures");
        }
    }
}
