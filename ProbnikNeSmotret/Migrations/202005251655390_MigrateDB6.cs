namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseStructures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumParagraph = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParagraphStructures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumTest = c.Int(nullable: false),
                        CourseStructureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourseStructures", t => t.CourseStructureId, cascadeDelete: true)
                .Index(t => t.CourseStructureId);
            
            CreateTable(
                "dbo.TestStructures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumQuestion = c.Int(nullable: false),
                        ParagraphStructureId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ParagraphStructures", t => t.ParagraphStructureId, cascadeDelete: true)
                .Index(t => t.ParagraphStructureId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestStructures", "ParagraphStructureId", "dbo.ParagraphStructures");
            DropForeignKey("dbo.ParagraphStructures", "CourseStructureId", "dbo.CourseStructures");
            DropIndex("dbo.TestStructures", new[] { "ParagraphStructureId" });
            DropIndex("dbo.ParagraphStructures", new[] { "CourseStructureId" });
            DropTable("dbo.TestStructures");
            DropTable("dbo.ParagraphStructures");
            DropTable("dbo.CourseStructures");
        }
    }
}
