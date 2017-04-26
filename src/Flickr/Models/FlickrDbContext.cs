using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Flickr.Models
{
    public class FlickrDbContext : IdentityDbContext<FlickrUser>
    {
        public FlickrDbContext()
        {
        }

        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Flickr;integrated security=True");
        }
        public FlickrDbContext(DbContextOptions options) : base(options)
        {        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


        
    }
}
