namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseStructurePersonalArea",
                c => new
                    {
                        CourseStructureId = c.Int(nullable: false),
                        PersonalAreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseStructureId, t.PersonalAreaId })
                .ForeignKey("dbo.CourseStructures", t => t.CourseStructureId, cascadeDelete: true)
                .ForeignKey("dbo.PersonalAreas", t => t.PersonalAreaId, cascadeDelete: true)
                .Index(t => t.CourseStructureId)
                .Index(t => t.PersonalAreaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseStructurePersonalArea", "PersonalAreaId", "dbo.PersonalAreas");
            DropForeignKey("dbo.CourseStructurePersonalArea", "CourseStructureId", "dbo.CourseStructures");
            DropIndex("dbo.CourseStructurePersonalArea", new[] { "PersonalAreaId" });
            DropIndex("dbo.CourseStructurePersonalArea", new[] { "CourseStructureId" });
            DropTable("dbo.CourseStructurePersonalArea");
        }
    }
}
