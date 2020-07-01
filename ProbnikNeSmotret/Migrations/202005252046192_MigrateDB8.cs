namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paragraphs", "NumInCourse", c => c.Int(nullable: false));
            AddColumn("dbo.Tests", "NumInParagraph", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "NumInTest", c => c.Int(nullable: false));
            AddColumn("dbo.CourseStructures", "HowManyOpen", c => c.Int(nullable: false));
            AddColumn("dbo.ParagraphStructures", "HowManyOpen", c => c.Int(nullable: false));
            AddColumn("dbo.TestStructures", "HowManyOpen", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TestStructures", "HowManyOpen");
            DropColumn("dbo.ParagraphStructures", "HowManyOpen");
            DropColumn("dbo.CourseStructures", "HowManyOpen");
            DropColumn("dbo.Questions", "NumInTest");
            DropColumn("dbo.Tests", "NumInParagraph");
            DropColumn("dbo.Paragraphs", "NumInCourse");
        }
    }
}
