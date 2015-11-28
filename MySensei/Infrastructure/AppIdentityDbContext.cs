﻿using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MySensei.Models;
using Microsoft.AspNet.Identity;

namespace MySensei.Infrastructure
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext() : base("IdentityDb") { }

        static AppIdentityDbContext()
        {
            Database.SetInitializer<AppIdentityDbContext>(new IdentityDbInit());
        }

        public static AppIdentityDbContext Create()
        {
            return new AppIdentityDbContext();
        }

        public IEnumerable AppUsers { get; internal set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); // Identity use pluralized table names
            // one-to-many relation between Course (1) and User (N)
            modelBuilder.Entity<Course>()
            .HasRequired<AppUser>(s => s.AppUser)
            .WithMany(s => s.Courses)
            .HasForeignKey(s => s.AppUserID);

            modelBuilder.Entity<Course>()
            .HasMany(t => t.Tags)
            .WithMany(t => t.Courses)
            .Map(m =>
            {
                m.ToTable("CoursesTags");
                m.MapLeftKey("CourseID");
                m.MapRightKey("TagID");
            });

            // the all important base class call! Add this line to make your problems go away.
            base.OnModelCreating(modelBuilder);
        }

    }

    public class IdentityDbInit : NullDatabaseInitializer<AppIdentityDbContext>
    {
    }

}