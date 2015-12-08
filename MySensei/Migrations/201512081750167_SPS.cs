namespace MySensei.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SPS : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Course_Insert",
                p => new
                    {
                        Title = p.String(),
                        Description = p.String(),
                        StartDate = p.DateTime(),
                        EndDate = p.DateTime(),
                        NumberOfLessons = p.Int(),
                        CourseTeacherId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[Course]([Title], [Description], [StartDate], [EndDate], [NumberOfLessons], [CourseTeacherId])
                      VALUES (@Title, @Description, @StartDate, @EndDate, @NumberOfLessons, @CourseTeacherId)
                      
                      DECLARE @CourseID int
                      SELECT @CourseID = [CourseID]
                      FROM [dbo].[Course]
                      WHERE @@ROWCOUNT > 0 AND [CourseID] = scope_identity()
                      
                      SELECT t0.[CourseID]
                      FROM [dbo].[Course] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[CourseID] = @CourseID"
            );
            
            CreateStoredProcedure(
                "dbo.Course_Update",
                p => new
                    {
                        CourseID = p.Int(),
                        Title = p.String(),
                        Description = p.String(),
                        StartDate = p.DateTime(),
                        EndDate = p.DateTime(),
                        NumberOfLessons = p.Int(),
                        CourseTeacherId = p.String(maxLength: 128),
                    },
                body:
                    @"UPDATE [dbo].[Course]
                      SET [Title] = @Title, [Description] = @Description, [StartDate] = @StartDate, [EndDate] = @EndDate, [NumberOfLessons] = @NumberOfLessons, [CourseTeacherId] = @CourseTeacherId
                      WHERE ([CourseID] = @CourseID)"
            );
            
            CreateStoredProcedure(
                "dbo.Course_Delete",
                p => new
                    {
                        CourseID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Course]
                      WHERE ([CourseID] = @CourseID)"
            );
            
            CreateStoredProcedure(
                "dbo.AppUser_Insert",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        FirstName = p.String(),
                        LastName = p.String(),
                        Description = p.String(),
                        Email = p.String(maxLength: 256),
                        EmailConfirmed = p.Boolean(),
                        PasswordHash = p.String(),
                        SecurityStamp = p.String(),
                        PhoneNumber = p.String(),
                        PhoneNumberConfirmed = p.Boolean(),
                        TwoFactorEnabled = p.Boolean(),
                        LockoutEndDateUtc = p.DateTime(),
                        LockoutEnabled = p.Boolean(),
                        AccessFailedCount = p.Int(),
                        UserName = p.String(maxLength: 256),
                    },
                body:
                    @"INSERT [dbo].[AspNetUsers]([Id], [FirstName], [LastName], [Description], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName])
                      VALUES (@Id, @FirstName, @LastName, @Description, @Email, @EmailConfirmed, @PasswordHash, @SecurityStamp, @PhoneNumber, @PhoneNumberConfirmed, @TwoFactorEnabled, @LockoutEndDateUtc, @LockoutEnabled, @AccessFailedCount, @UserName)"
            );
            
            CreateStoredProcedure(
                "dbo.AppUser_Update",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        FirstName = p.String(),
                        LastName = p.String(),
                        Description = p.String(),
                        Email = p.String(maxLength: 256),
                        EmailConfirmed = p.Boolean(),
                        PasswordHash = p.String(),
                        SecurityStamp = p.String(),
                        PhoneNumber = p.String(),
                        PhoneNumberConfirmed = p.Boolean(),
                        TwoFactorEnabled = p.Boolean(),
                        LockoutEndDateUtc = p.DateTime(),
                        LockoutEnabled = p.Boolean(),
                        AccessFailedCount = p.Int(),
                        UserName = p.String(maxLength: 256),
                    },
                body:
                    @"UPDATE [dbo].[AspNetUsers]
                      SET [FirstName] = @FirstName, [LastName] = @LastName, [Description] = @Description, [Email] = @Email, [EmailConfirmed] = @EmailConfirmed, [PasswordHash] = @PasswordHash, [SecurityStamp] = @SecurityStamp, [PhoneNumber] = @PhoneNumber, [PhoneNumberConfirmed] = @PhoneNumberConfirmed, [TwoFactorEnabled] = @TwoFactorEnabled, [LockoutEndDateUtc] = @LockoutEndDateUtc, [LockoutEnabled] = @LockoutEnabled, [AccessFailedCount] = @AccessFailedCount, [UserName] = @UserName
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AppUser_Delete",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[AspNetUsers]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.IdentityUserClaim_Insert",
                p => new
                    {
                        UserId = p.String(maxLength: 128),
                        ClaimType = p.String(),
                        ClaimValue = p.String(),
                    },
                body:
                    @"INSERT [dbo].[AspNetUserClaims]([UserId], [ClaimType], [ClaimValue])
                      VALUES (@UserId, @ClaimType, @ClaimValue)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[AspNetUserClaims]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[AspNetUserClaims] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.IdentityUserClaim_Update",
                p => new
                    {
                        Id = p.Int(),
                        UserId = p.String(maxLength: 128),
                        ClaimType = p.String(),
                        ClaimValue = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[AspNetUserClaims]
                      SET [UserId] = @UserId, [ClaimType] = @ClaimType, [ClaimValue] = @ClaimValue
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.IdentityUserClaim_Delete",
                p => new
                    {
                        Id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[AspNetUserClaims]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.IdentityUserLogin_Insert",
                p => new
                    {
                        LoginProvider = p.String(maxLength: 128),
                        ProviderKey = p.String(maxLength: 128),
                        UserId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[AspNetUserLogins]([LoginProvider], [ProviderKey], [UserId])
                      VALUES (@LoginProvider, @ProviderKey, @UserId)"
            );
            
            CreateStoredProcedure(
                "dbo.IdentityUserLogin_Update",
                p => new
                    {
                        LoginProvider = p.String(maxLength: 128),
                        ProviderKey = p.String(maxLength: 128),
                        UserId = p.String(maxLength: 128),
                    },
                body:
                    @"RETURN"
            );
            
            CreateStoredProcedure(
                "dbo.IdentityUserLogin_Delete",
                p => new
                    {
                        LoginProvider = p.String(maxLength: 128),
                        ProviderKey = p.String(maxLength: 128),
                        UserId = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[AspNetUserLogins]
                      WHERE ((([LoginProvider] = @LoginProvider) AND ([ProviderKey] = @ProviderKey)) AND ([UserId] = @UserId))"
            );
            
            CreateStoredProcedure(
                "dbo.IdentityUserRole_Insert",
                p => new
                    {
                        UserId = p.String(maxLength: 128),
                        RoleId = p.String(maxLength: 128),
                    },
                body:
                    @"INSERT [dbo].[AspNetUserRoles]([UserId], [RoleId])
                      VALUES (@UserId, @RoleId)"
            );
            
            CreateStoredProcedure(
                "dbo.IdentityUserRole_Update",
                p => new
                    {
                        UserId = p.String(maxLength: 128),
                        RoleId = p.String(maxLength: 128),
                    },
                body:
                    @"RETURN"
            );
            
            CreateStoredProcedure(
                "dbo.IdentityUserRole_Delete",
                p => new
                    {
                        UserId = p.String(maxLength: 128),
                        RoleId = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[AspNetUserRoles]
                      WHERE (([UserId] = @UserId) AND ([RoleId] = @RoleId))"
            );
            
            CreateStoredProcedure(
                "dbo.Tag_Insert",
                p => new
                    {
                        TagName = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Tag]([TagName])
                      VALUES (@TagName)
                      
                      DECLARE @TagID int
                      SELECT @TagID = [TagID]
                      FROM [dbo].[Tag]
                      WHERE @@ROWCOUNT > 0 AND [TagID] = scope_identity()
                      
                      SELECT t0.[TagID]
                      FROM [dbo].[Tag] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[TagID] = @TagID"
            );
            
            CreateStoredProcedure(
                "dbo.Tag_Update",
                p => new
                    {
                        TagID = p.Int(),
                        TagName = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Tag]
                      SET [TagName] = @TagName
                      WHERE ([TagID] = @TagID)"
            );
            
            CreateStoredProcedure(
                "dbo.Tag_Delete",
                p => new
                    {
                        TagID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Tag]
                      WHERE ([TagID] = @TagID)"
            );
            
            CreateStoredProcedure(
                "dbo.IdentityRole_Insert",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        Name = p.String(maxLength: 256),
                    },
                body:
                    @"INSERT [dbo].[AspNetRoles]([Id], [Name], [Discriminator])
                      VALUES (@Id, @Name, N'IdentityRole')"
            );
            
            CreateStoredProcedure(
                "dbo.IdentityRole_Update",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        Name = p.String(maxLength: 256),
                    },
                body:
                    @"UPDATE [dbo].[AspNetRoles]
                      SET [Name] = @Name
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.IdentityRole_Delete",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[AspNetRoles]
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AppRole_Insert",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        Name = p.String(maxLength: 256),
                    },
                body:
                    @"INSERT [dbo].[AspNetRoles]([Id], [Name], [Discriminator])
                      VALUES (@Id, @Name, N'AppRole')"
            );
            
            CreateStoredProcedure(
                "dbo.AppRole_Update",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                        Name = p.String(maxLength: 256),
                    },
                body:
                    @"UPDATE [dbo].[AspNetRoles]
                      SET [Name] = @Name
                      WHERE ([Id] = @Id)"
            );
            
            CreateStoredProcedure(
                "dbo.AppRole_Delete",
                p => new
                    {
                        Id = p.String(maxLength: 128),
                    },
                body:
                    @"DELETE [dbo].[AspNetRoles]
                      WHERE ([Id] = @Id)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.AppRole_Delete");
            DropStoredProcedure("dbo.AppRole_Update");
            DropStoredProcedure("dbo.AppRole_Insert");
            DropStoredProcedure("dbo.IdentityRole_Delete");
            DropStoredProcedure("dbo.IdentityRole_Update");
            DropStoredProcedure("dbo.IdentityRole_Insert");
            DropStoredProcedure("dbo.Tag_Delete");
            DropStoredProcedure("dbo.Tag_Update");
            DropStoredProcedure("dbo.Tag_Insert");
            DropStoredProcedure("dbo.IdentityUserRole_Delete");
            DropStoredProcedure("dbo.IdentityUserRole_Update");
            DropStoredProcedure("dbo.IdentityUserRole_Insert");
            DropStoredProcedure("dbo.IdentityUserLogin_Delete");
            DropStoredProcedure("dbo.IdentityUserLogin_Update");
            DropStoredProcedure("dbo.IdentityUserLogin_Insert");
            DropStoredProcedure("dbo.IdentityUserClaim_Delete");
            DropStoredProcedure("dbo.IdentityUserClaim_Update");
            DropStoredProcedure("dbo.IdentityUserClaim_Insert");
            DropStoredProcedure("dbo.AppUser_Delete");
            DropStoredProcedure("dbo.AppUser_Update");
            DropStoredProcedure("dbo.AppUser_Insert");
            DropStoredProcedure("dbo.Course_Delete");
            DropStoredProcedure("dbo.Course_Update");
            DropStoredProcedure("dbo.Course_Insert");
        }
    }
}
