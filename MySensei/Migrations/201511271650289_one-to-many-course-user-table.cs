namespace MySensei.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class onetomanycourseusertable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "AppUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "AppUserID" });
            AlterColumn("dbo.Courses", "AppUserID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Courses", "AppUserID");
            AddForeignKey("dbo.Courses", "AppUserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "AppUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "AppUserID" });
            AlterColumn("dbo.Courses", "AppUserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Courses", "AppUserID");
            AddForeignKey("dbo.Courses", "AppUserID", "dbo.AspNetUsers", "Id");
        }
    }
}
