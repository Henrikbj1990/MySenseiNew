namespace MySensei.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class coursetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        NumberOfLessons = c.Int(nullable: false),
                        AppUserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CourseID)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUserID)
                .Index(t => t.AppUserID);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "AppUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "AppUserID" });
            DropTable("dbo.Courses");
        }
    }
}
