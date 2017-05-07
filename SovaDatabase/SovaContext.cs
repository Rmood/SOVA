using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DomainModel;

namespace SovaDatabase
{
    public class SovaContext : DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Posttype> Posttypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql("server=localhost;database=stackoverflow_sample_universal;uid=Rmood;pwd=1234");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Comment>().ToTable("comments");
            modelBuilder.Entity<Comment>().Property(x => x.Id).HasColumnName("comment_id");
            modelBuilder.Entity<Comment>().Property(x => x.postId).HasColumnName("post_id");
            modelBuilder.Entity<Comment>().Property(x => x.date).HasColumnName("creation_date");
            modelBuilder.Entity<Comment>().Property(x => x.userId).HasColumnName("user_id");
            modelBuilder.Entity<Posttype>().ToTable("posttype");
        }
    }
}
