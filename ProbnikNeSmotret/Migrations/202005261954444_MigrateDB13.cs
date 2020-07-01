namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParagraphStructures", "NumInCourse", c => c.Int(nullable: false));
            AddColumn("dbo.TestStructures", "NumInParagraph", c => c.Int(nullable: false));
            AddColumn("dbo.QuestionStructures", "NumInTest", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuestionStructures", "NumInTest");
            DropColumn("dbo.TestStructures", "NumInParagraph");
            DropColumn("dbo.ParagraphStructures", "NumInCourse");
        }
    }
}
