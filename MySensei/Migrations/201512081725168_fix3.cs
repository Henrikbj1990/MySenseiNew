namespace MySensei.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix3 : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.CourseAppUser_Insert",
                p => new
                    {
                        CourseID = p.Int(),
                        AppUserID = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[StudentCourses]([CourseID], [AppUserID])
                      VALUES (@CourseID, @AppUserID)"
            );
            
            CreateStoredProcedure(
                "dbo.CourseAppUser_Delete",
                p => new
                    {
                        CourseID = p.Int(),
                        AppUserID = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[StudentCourses]
                      WHERE (([CourseID] = @CourseID) AND ([AppUserID] = @AppUserID))"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.CourseAppUser_Delete");
            DropStoredProcedure("dbo.CourseAppUser_Insert");
        }
    }
}
