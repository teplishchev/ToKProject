namespace ProbnikNeSmotret.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuestionStructures", "TestStructure_Id", "dbo.TestStructures");
            DropIndex("dbo.QuestionStructures", new[] { "TestStructure_Id" });
            RenameColumn(table: "dbo.QuestionStructures", name: "TestStructure_Id", newName: "TestStructureId");
            AlterColumn("dbo.QuestionStructures", "TestStructureId", c => c.Int(nullable: false));
            CreateIndex("dbo.QuestionStructures", "TestStructureId");
            AddForeignKey("dbo.QuestionStructures", "TestStructureId", "dbo.TestStructures", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionStructures", "TestStructureId", "dbo.TestStructures");
            DropIndex("dbo.QuestionStructures", new[] { "TestStructureId" });
            AlterColumn("dbo.QuestionStructures", "TestStructureId", c => c.Int());
            RenameColumn(table: "dbo.QuestionStructures", name: "TestStructureId", newName: "TestStructure_Id");
            CreateIndex("dbo.QuestionStructures", "TestStructure_Id");
            AddForeignKey("dbo.QuestionStructures", "TestStructure_Id", "dbo.TestStructures", "Id");
        }
    }
}
