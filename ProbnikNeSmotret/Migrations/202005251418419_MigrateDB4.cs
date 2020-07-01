namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "PersonalArea_Id", "dbo.PersonalAreas");
            DropIndex("dbo.Courses", new[] { "PersonalArea_Id" });
            CreateTable(
                "dbo.CoursePersonalArea",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        PersonalAreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseId, t.PersonalAreaId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.PersonalAreas", t => t.PersonalAreaId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.PersonalAreaId);
            
            AddColumn("dbo.PersonalAreas", "AspNetUserId", c => c.Int(nullable: false));
            DropColumn("dbo.Courses", "PersonalArea_Id");
            DropColumn("dbo.PersonalAreas", "ClientId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PersonalAreas", "ClientId", c => c.Int(nullable: false));
            AddColumn("dbo.Courses", "PersonalArea_Id", c => c.Int());
            DropForeignKey("dbo.CoursePersonalArea", "PersonalAreaId", "dbo.PersonalAreas");
            DropForeignKey("dbo.CoursePersonalArea", "CourseId", "dbo.Courses");
            DropIndex("dbo.CoursePersonalArea", new[] { "PersonalAreaId" });
            DropIndex("dbo.CoursePersonalArea", new[] { "CourseId" });
            DropColumn("dbo.PersonalAreas", "AspNetUserId");
            DropTable("dbo.CoursePersonalArea");
            CreateIndex("dbo.Courses", "PersonalArea_Id");
            AddForeignKey("dbo.Courses", "PersonalArea_Id", "dbo.PersonalAreas", "Id");
        }
    }
}
