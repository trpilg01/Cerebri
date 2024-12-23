﻿using Cerebri.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cerebri.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<JournalEntryModel> JournalEntries { get; set; }
        public DbSet<JournalEntryMoodModel> JournalEntryMoods { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(x => x.HashedPassword)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(x => x.FirstName)
                    .HasMaxLength(50);
                
                entity.Property(x => x.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");
                
                entity.Property(x => x.UpdatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.HasMany(x => x.JournalEntries)
                    .WithOne(x => x.User)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<JournalEntryModel>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(x => x.Content)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(x => x.UpdatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(x => x.CreatedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<JournalEntryMoodModel>(entity =>
            {
                entity.HasKey(x => new { x.JournalEntryId, x.MoodId });

                entity.HasOne(x => x.JournalEntry)
                      .WithMany(x => x.MoodTags)
                      .HasForeignKey(x => x.JournalEntryId);
            });
        }
    }
}
