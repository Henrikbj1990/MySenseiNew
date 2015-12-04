namespace MySensei.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        TagName = c.String(),
                    })
                .PrimaryKey(t => t.TagID);
            
            CreateTable(
                "dbo.CoursesTags",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        TagID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseID, t.TagID })
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.TagID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CoursesTags", "TagID", "dbo.Tags");
            DropForeignKey("dbo.CoursesTags", "CourseID", "dbo.Courses");
            DropIndex("dbo.CoursesTags", new[] { "TagID" });
            DropIndex("dbo.CoursesTags", new[] { "CourseID" });
            DropTable("dbo.CoursesTags");
            DropTable("dbo.Tags");
        }
    }
}
