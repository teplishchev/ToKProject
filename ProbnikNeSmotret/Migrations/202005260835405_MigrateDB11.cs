namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseStructures", "Course_Id", c => c.Int());
            AddColumn("dbo.ParagraphStructures", "Paragraph_Id", c => c.Int());
            AddColumn("dbo.TestStructures", "Test_Id", c => c.Int());
            AddColumn("dbo.QuestionStructures", "Question_Id", c => c.Int());
            CreateIndex("dbo.CourseStructures", "Course_Id");
            CreateIndex("dbo.ParagraphStructures", "Paragraph_Id");
            CreateIndex("dbo.TestStructures", "Test_Id");
            CreateIndex("dbo.QuestionStructures", "Question_Id");
            AddForeignKey("dbo.CourseStructures", "Course_Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.ParagraphStructures", "Paragraph_Id", "dbo.Paragraphs", "Id");
            AddForeignKey("dbo.QuestionStructures", "Question_Id", "dbo.Questions", "Id");
            AddForeignKey("dbo.TestStructures", "Test_Id", "dbo.Tests", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestStructures", "Test_Id", "dbo.Tests");
            DropForeignKey("dbo.QuestionStructures", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.ParagraphStructures", "Paragraph_Id", "dbo.Paragraphs");
            DropForeignKey("dbo.CourseStructures", "Course_Id", "dbo.Courses");
            DropIndex("dbo.QuestionStructures", new[] { "Question_Id" });
            DropIndex("dbo.TestStructures", new[] { "Test_Id" });
            DropIndex("dbo.ParagraphStructures", new[] { "Paragraph_Id" });
            DropIndex("dbo.CourseStructures", new[] { "Course_Id" });
            DropColumn("dbo.QuestionStructures", "Question_Id");
            DropColumn("dbo.TestStructures", "Test_Id");
            DropColumn("dbo.ParagraphStructures", "Paragraph_Id");
            DropColumn("dbo.CourseStructures", "Course_Id");
        }
    }
}
