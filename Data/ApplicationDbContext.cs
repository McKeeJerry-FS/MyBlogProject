using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBlogProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlogProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<BlogUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BlogModel> Blogs { get; set; }
        public DbSet<BlogPostModel> Posts { get; set;}
        public DbSet<BlogCommentModel> Comments { get; set; }
        public DbSet<BlogTagModel> Tags { get; set; }
    }
}
