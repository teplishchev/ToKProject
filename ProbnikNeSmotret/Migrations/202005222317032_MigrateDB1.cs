namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tests", "CourseId", "dbo.Courses");
            DropIndex("dbo.Tests", new[] { "CourseId" });
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        IsRight = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Points = c.Int(nullable: false),
                        TestId = c.Int(nullable: false),
                        ImgUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.Paragraphs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImgUrl = c.String(),
                        Description = c.String(),
                        Text = c.String(),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.PersonalAreas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        Person = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Courses", "PersonalArea_Id", c => c.Int());
            AddColumn("dbo.Tests", "ParagraphId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tests", "ParagraphId");
            CreateIndex("dbo.Courses", "PersonalArea_Id");
            AddForeignKey("dbo.Tests", "ParagraphId", "dbo.Paragraphs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Courses", "PersonalArea_Id", "dbo.PersonalAreas", "Id");
            DropColumn("dbo.Tests", "Question");
            DropColumn("dbo.Tests", "Answer");
            DropColumn("dbo.Tests", "CourseId");
        }
        
        //public override void Down()
        //{
        //    AddColumn("dbo.Tests", "CourseId", c => c.Int(nullable: false));
        //    AddColumn("dbo.Tests", "Answer", c => c.String());
        //    AddColumn("dbo.Tests", "Question", c => c.String());
        //    DropForeignKey("dbo.Courses", "PersonalArea_Id", "dbo.PersonalAreas");
        //    DropForeignKey("dbo.Questions", "TestId", "dbo.Tests");
        //    DropForeignKey("dbo.Tests", "ParagraphId", "dbo.Paragraphs");
        //    DropForeignKey("dbo.Paragraphs", "CourseId", "dbo.Courses");
        //    DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
        //    DropIndex("dbo.Courses", new[] { "PersonalArea_Id" });
        //    DropIndex("dbo.Paragraphs", new[] { "CourseId" });
        //    DropIndex("dbo.Tests", new[] { "ParagraphId" });
        //    DropIndex("dbo.Questions", new[] { "TestId" });
        //    DropIndex("dbo.Answers", new[] { "QuestionId" });
        //    DropColumn("dbo.Tests", "ParagraphId");
        //    DropColumn("dbo.Courses", "PersonalArea_Id");
        //    DropTable("dbo.PersonalAreas");
        //    DropTable("dbo.Paragraphs");
        //    DropTable("dbo.Questions");
        //    DropTable("dbo.Answers");
        //    CreateIndex("dbo.Tests", "CourseId");
        //    AddForeignKey("dbo.Tests", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        //}
    }
}
