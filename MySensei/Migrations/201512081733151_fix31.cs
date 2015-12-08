namespace MySensei.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix31 : DbMigration
    {
        public override void Up()
        {
            DropStoredProcedure("dbo.CourseAppUser_Insert");
            DropStoredProcedure("dbo.CourseAppUser_Delete");
        }
        
        public override void Down()
        {
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
