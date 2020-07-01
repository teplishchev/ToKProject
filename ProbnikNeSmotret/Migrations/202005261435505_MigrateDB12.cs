namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB12 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourseStructures", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.ParagraphStructures", "Paragraph_Id", "dbo.Paragraphs");
            DropForeignKey("dbo.QuestionStructures", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.TestStructures", "Test_Id", "dbo.Tests");
            DropIndex("dbo.CourseStructures", new[] { "Course_Id" });
            DropIndex("dbo.ParagraphStructures", new[] { "Paragraph_Id" });
            DropIndex("dbo.TestStructures", new[] { "Test_Id" });
            DropIndex("dbo.QuestionStructures", new[] { "Question_Id" });
            AddColumn("dbo.CourseStructures", "IdCourse", c => c.Int(nullable: false));
            AddColumn("dbo.ParagraphStructures", "IdParagraph", c => c.Int(nullable: false));
            AddColumn("dbo.TestStructures", "IdTest", c => c.Int(nullable: false));
            AddColumn("dbo.QuestionStructures", "IdQuestion", c => c.Int(nullable: false));
            DropColumn("dbo.CourseStructures", "Course_Id");
            DropColumn("dbo.ParagraphStructures", "Paragraph_Id");
            DropColumn("dbo.TestStructures", "Test_Id");
            DropColumn("dbo.QuestionStructures", "Question_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.QuestionStructures", "Question_Id", c => c.Int());
            AddColumn("dbo.TestStructures", "Test_Id", c => c.Int());
            AddColumn("dbo.ParagraphStructures", "Paragraph_Id", c => c.Int());
            AddColumn("dbo.CourseStructures", "Course_Id", c => c.Int());
            DropColumn("dbo.QuestionStructures", "IdQuestion");
            DropColumn("dbo.TestStructures", "IdTest");
            DropColumn("dbo.ParagraphStructures", "IdParagraph");
            DropColumn("dbo.CourseStructures", "IdCourse");
            CreateIndex("dbo.QuestionStructures", "Question_Id");
            CreateIndex("dbo.TestStructures", "Test_Id");
            CreateIndex("dbo.ParagraphStructures", "Paragraph_Id");
            CreateIndex("dbo.CourseStructures", "Course_Id");
            AddForeignKey("dbo.TestStructures", "Test_Id", "dbo.Tests", "Id");
            AddForeignKey("dbo.QuestionStructures", "Question_Id", "dbo.Questions", "Id");
            AddForeignKey("dbo.ParagraphStructures", "Paragraph_Id", "dbo.Paragraphs", "Id");
            AddForeignKey("dbo.CourseStructures", "Course_Id", "dbo.Courses", "Id");
        }
    }
}
