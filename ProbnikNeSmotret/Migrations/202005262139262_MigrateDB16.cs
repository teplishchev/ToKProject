namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB16 : DbMigration
    {
        public override void Up()
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TestStructures", t => t.TestStructureId, cascadeDelete: true)
                .Index(t => t.TestStructureId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionStructures", "TestStructureId", "dbo.TestStructures");
            DropIndex("dbo.QuestionStructures", new[] { "TestStructureId" });
            DropTable("dbo.QuestionStructures");
        }
    }
}
