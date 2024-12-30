﻿// <auto-generated />
using System;
using Cerebri.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cerebri.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Cerebri.Domain.Entities.JournalEntryModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("JournalEntries");
                });

            modelBuilder.Entity("Cerebri.Domain.Entities.JournalEntryMoodModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("JournalEntryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MoodId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("JournalEntryId");

                    b.HasIndex("MoodId");

                    b.ToTable("JournalEntryMoods");
                });

            modelBuilder.Entity("Cerebri.Domain.Entities.MoodModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Moods");
                });

            modelBuilder.Entity("Cerebri.Domain.Entities.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Cerebri.Domain.Entities.JournalEntryModel", b =>
                {
                    b.HasOne("Cerebri.Domain.Entities.UserModel", "User")
                        .WithMany("JournalEntries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cerebri.Domain.Entities.JournalEntryMoodModel", b =>
                {
                    b.HasOne("Cerebri.Domain.Entities.JournalEntryModel", "JournalEntry")
                        .WithMany("MoodTags")
                        .HasForeignKey("JournalEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cerebri.Domain.Entities.MoodModel", "Mood")
                        .WithMany()
                        .HasForeignKey("MoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JournalEntry");

                    b.Navigation("Mood");
                });

            modelBuilder.Entity("Cerebri.Domain.Entities.JournalEntryModel", b =>
                {
                    b.Navigation("MoodTags");
                });

            modelBuilder.Entity("Cerebri.Domain.Entities.UserModel", b =>
                {
                    b.Navigation("JournalEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
