using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySensei.Infrastructure;
using MySensei.Models;
namespace MySensei.Migrations
{
    internal sealed class Configuration
    : DbMigrationsConfiguration<AppIdentityDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "MySensei.Infrastructure.AppIdentityDbContext";
        }
        protected override void Seed(AppIdentityDbContext context)
        {
            AppUserManager userMgr = new AppUserManager(new UserStore<AppUser>(context));
            AppRoleManager roleMgr = new AppRoleManager(new RoleStore<AppRole>(context));
            string roleName = "Administrators";
            string userName = "Admin";
            string password = "MySecret";
            string email = " admin@example.com ";
            if (!roleMgr.RoleExists(roleName))
            {
                roleMgr.Create(new AppRole(roleName));
            }
            AppUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new AppUser { UserName = userName, Email = email }, password);
                user = userMgr.FindByName(userName);
            }
            if (!userMgr.IsInRole(user.Id, roleName))
            {
                userMgr.AddToRole(user.Id, roleName);
            }

            if (!context.Tags.Any(x => x.TagName == "Musik"))
            {
                var tag = new Tag();
                tag.TagName = "Musik";

                context.Tags.Add(tag);
            }

            if (!context.Tags.Any(x => x.TagName == "Sport"))
            {
                var tag = new Tag();
                tag.TagName = "Sport";

                context.Tags.Add(tag);
            }

            if (!context.Tags.Any(x => x.TagName == "IT"))
            {
                var tag = new Tag();
                tag.TagName = "IT";

                context.Tags.Add(tag);
            }

            if (!context.Tags.Any(x => x.TagName == "Arbejde"))
            {
                var tag = new Tag();
                tag.TagName = "Arbejde";

                context.Tags.Add(tag);
            }

            if (!context.Tags.Any(x => x.TagName == "Hobby"))
            {
                var tag = new Tag();
                tag.TagName = "Hobby";

                context.Tags.Add(tag);
            }
           
            
            context.SaveChanges();
        }
    }
}