namespace MySensei.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseRoles : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "AppUserID", "dbo.AspNetUsers");
            RenameColumn(table: "dbo.Courses", name: "AppUserID", newName: "CourseTeacherId");
            RenameIndex(table: "dbo.Courses", name: "IX_AppUserID", newName: "IX_CourseTeacherId");
            CreateTable(
                "dbo.StudentCourses",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        AppUserID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.CourseID, t.AppUserID })
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUserID, cascadeDelete: true)
                .Index(t => t.CourseID)
                .Index(t => t.AppUserID);
            
            AddForeignKey("dbo.Courses", "CourseTeacherId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "CourseTeacherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentCourses", "AppUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentCourses", "CourseID", "dbo.Courses");
            DropIndex("dbo.StudentCourses", new[] { "AppUserID" });
            DropIndex("dbo.StudentCourses", new[] { "CourseID" });
            DropTable("dbo.StudentCourses");
            RenameIndex(table: "dbo.Courses", name: "IX_CourseTeacherId", newName: "IX_AppUserID");
            RenameColumn(table: "dbo.Courses", name: "CourseTeacherId", newName: "AppUserID");
            AddForeignKey("dbo.Courses", "AppUserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
