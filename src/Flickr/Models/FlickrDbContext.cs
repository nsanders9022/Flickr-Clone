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
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PictureTag> PicturesTags { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Flickr;integrated security=True");
        }
        public FlickrDbContext(DbContextOptions options) : base(options)
        {        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PictureTag>()
                .HasKey(t => new { t.PictureId, t.TagId });

            builder.Entity<PictureTag>()
                .HasOne(pt => pt.Picture)
                .WithMany(p => p.PicturesTags)
                .HasForeignKey(pt => pt.PictureId);

            builder.Entity<PictureTag>()
               .HasOne(pt => pt.Tag)
               .WithMany(p => p.PicturesTags)
               .HasForeignKey(pt => pt.TagId);
            base.OnModelCreating(builder);

        }



    }
}
