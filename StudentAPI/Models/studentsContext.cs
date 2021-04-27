using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace StudentAPI.Models
{
    public partial class studentsContext : DbContext
    {

        public studentsContext()
        {
        }

        public studentsContext(DbContextOptions<studentsContext> options)
            : base(options)
        {
        }



        public virtual DbSet<Student> StudentsModel { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }

        public virtual DbSet<Login> LoginModel { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Area)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Age);
                entity.Property(e => e.Contact);

            });

            modelBuilder.Entity<Courses>(entity =>
            {
                entity.ToTable("courses");

                entity.Property(e => e.Id);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Instructor).HasMaxLength(20);

                entity.Property(e => e.Duration).HasMaxLength(20);

            });
            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("login");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Username).HasMaxLength(50).HasColumnName("username");

                entity.Property(e => e.Password).HasMaxLength(20).HasColumnName("password");


            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
