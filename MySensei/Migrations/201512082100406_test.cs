namespace MySensei.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
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
            
            CreateStoredProcedure(
                "dbo.CourseTag_Insert",
                p => new
                    {
                        CourseID = p.Int(),
                        TagID = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[CoursesTags]([CourseID], [TagID])
                      VALUES (@CourseID, @TagID)"
            );
            
            CreateStoredProcedure(
                "dbo.CourseTag_Delete",
                p => new
                    {
                        CourseID = p.Int(),
                        TagID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[CoursesTags]
                      WHERE (([CourseID] = @CourseID) AND ([TagID] = @TagID))"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.CourseTag_Delete");
            DropStoredProcedure("dbo.CourseTag_Insert");
            DropStoredProcedure("dbo.CourseAppUser_Delete");
            DropStoredProcedure("dbo.CourseAppUser_Insert");
        }
    }
}
