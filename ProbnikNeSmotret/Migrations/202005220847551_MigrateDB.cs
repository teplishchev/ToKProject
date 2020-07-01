namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Categories",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 50),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Clients",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            LastName = c.String(maxLength: 50),
            //            Name = c.String(maxLength: 50),
            //            Address = c.String(),
            //            TotalOrdersCost = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            OrdersNumber = c.Int(nullable: false),
            //            ReviewsNumber = c.Int(nullable: false),
            //            CurrentDiscount = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Courses",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(maxLength: 50),
            //            Price = c.Int(nullable: false),
            //            FullDescription = c.String(),
            //            ShortDescription = c.String(maxLength: 200),
            //            CategoryId = c.Int(),
            //            ImageUrl = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Categories", t => t.CategoryId)
            //    .Index(t => t.CategoryId);
            
            //CreateTable(
            //    "dbo.Reviews",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            CourseId = c.Int(nullable: false),
            //            ClientId = c.String(maxLength: 128),
            //            Text = c.String(nullable: false, maxLength: 2000),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
            //    .ForeignKey("dbo.Clients", t => t.ClientId)
            //    .Index(t => t.CourseId)
            //    .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Answer = c.String(),
                        Points = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
        }
        
        //public override void Down()
        //{
        //    DropForeignKey("dbo.Tests", "CourseId", "dbo.Courses");
        //    DropForeignKey("dbo.Reviews", "ClientId", "dbo.Clients");
        //    DropForeignKey("dbo.Reviews", "CourseId", "dbo.Courses");
        //    DropForeignKey("dbo.Courses", "CategoryId", "dbo.Categories");
        //    DropIndex("dbo.Tests", new[] { "CourseId" });
        //    DropIndex("dbo.Reviews", new[] { "ClientId" });
        //    DropIndex("dbo.Reviews", new[] { "CourseId" });
        //    DropIndex("dbo.Courses", new[] { "CategoryId" });
        //    DropTable("dbo.Tests");
        //    DropTable("dbo.Reviews");
        //    DropTable("dbo.Courses");
        //    DropTable("dbo.Clients");
        //    DropTable("dbo.Categories");
        //}
    }
}
