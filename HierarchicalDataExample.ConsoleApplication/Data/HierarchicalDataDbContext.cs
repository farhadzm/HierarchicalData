using HierarchicalDataExample.ConsoleApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HierarchicalDataExample.ConsoleApplication.Data
{
    public class HierarchicalDataDbContext : DbContext
    {
        public HierarchicalDataDbContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=HierarchicalData; Integrated Security=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Users>()
                .HasData(new Models.Users
                {
                    Id = 1,
                    FullName = "Farhad Zamani"
                },
                new Users
                {
                    Id = 2,
                    FullName = "Himan falahi"
                });
            modelBuilder.Entity<Posts>()
                .HasData(new Models.Posts
                {
                    Id = 1,
                    UserId = 1,
                    Title = "Test HierarchicalData",
                    Descriptions = "HierarchicalData"
                });
            modelBuilder.Entity<Comments>()
                .HasOne(a => a.User)
                .WithMany(a => a.ChildComments)
                .HasForeignKey(a => a.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Comments>()
                .HasOne(a => a.Parent)
                .WithMany(a => a.ChildComments)
                .HasForeignKey(a => a.ParentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Comments>()
                .HasData(new Models.Comments
                {
                    Id = 1,
                    ParentId = null,
                    PostId = 1,
                    Text = "First comments",
                    UserId = 1
                }, new Models.Comments
                {
                    Id = 2,
                    ParentId = 1,
                    PostId = 1,
                    Text = "Reply to first comments",
                    UserId = 2
                }, new Models.Comments
                {
                    Id = 3,
                    ParentId = null,
                    PostId = 1,
                    Text = "Second comment",
                    UserId = 1
                }, new Models.Comments
                {
                    Id = 4,
                    ParentId = 2,
                    PostId = 1,
                    Text = "Reply to previous comment",
                    UserId = 2
                }, new Models.Comments
                {
                    Id = 5,
                    ParentId = 3,
                    PostId = 1,
                    Text = "Reply to second comment",
                    UserId = 2
                });
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Posts> Posts { get; set; }
        public DbSet<Comments> Comments { get; set; }
    }
}
