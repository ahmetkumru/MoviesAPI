using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MoviesAPI.Models
{
    public partial class MovieDBContext : DbContext
    {
        public MovieDBContext()
        {
        }

        public MovieDBContext(DbContextOptions<MovieDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Directors> Directors { get; set; }
        public virtual DbSet<MovieCategorie> MovieCategorie { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieCategorie>(entity =>
            {
                entity.HasKey(e => new { e.CategoryId, e.MovieId });

                entity.HasIndex(e => e.MovieId);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.MovieCategorie)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieCategorie)
                    .HasForeignKey(d => d.MovieId);
            });

            modelBuilder.Entity<Movies>(entity =>
            {
                entity.HasIndex(e => e.DirectorId);

                entity.HasOne(d => d.Director)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.DirectorId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
