namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseStructures", "CourseId", c => c.Int(nullable: false));
            AddColumn("dbo.ParagraphStructures", "ParagraphId", c => c.Int(nullable: false));
            AddColumn("dbo.TestStructures", "TestId", c => c.Int(nullable: false));
            CreateIndex("dbo.CourseStructures", "CourseId");
            CreateIndex("dbo.ParagraphStructures", "ParagraphId");
            CreateIndex("dbo.TestStructures", "TestId");
            AddForeignKey("dbo.CourseStructures", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ParagraphStructures", "ParagraphId", "dbo.Paragraphs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TestStructures", "TestId", "dbo.Tests", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestStructures", "TestId", "dbo.Tests");
            DropForeignKey("dbo.ParagraphStructures", "ParagraphId", "dbo.Paragraphs");
            DropForeignKey("dbo.CourseStructures", "CourseId", "dbo.Courses");
            DropIndex("dbo.TestStructures", new[] { "TestId" });
            DropIndex("dbo.ParagraphStructures", new[] { "ParagraphId" });
            DropIndex("dbo.CourseStructures", new[] { "CourseId" });
            DropColumn("dbo.TestStructures", "TestId");
            DropColumn("dbo.ParagraphStructures", "ParagraphId");
            DropColumn("dbo.CourseStructures", "CourseId");
        }
    }
}
