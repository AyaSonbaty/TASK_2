using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace DataAccessLayer.Data
{
    public class ITIDbContext: IdentityDbContext<AppUser>
    {

        public ITIDbContext(DbContextOptions<ITIDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Tranee> Tranees { get; set; }
        public DbSet<TraneeCourse> TraneeCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Instructor>()
                .HasOne(i => i.Department)
                .WithMany(d => d.Instructors)
                .HasForeignKey(i => i.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.Manager)
                .WithMany()
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder.Entity<TraneeCourse>()
                .HasKey(tc => new { tc.TraneeId, tc.CourseId });

            modelBuilder.Entity<TraneeCourse>()
                .HasOne(tc => tc.Tranee)
                .WithMany(t => t.TraneeCourses)
                .HasForeignKey(tc => tc.TraneeId);

            modelBuilder.Entity<TraneeCourse>()
                .HasOne(tc => tc.Course)
                .WithMany(c => c.TraneeCourses)
                .HasForeignKey(tc => tc.CourseId);

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Computer Science", ManagerName = "Dr. Ahmed" },
                new Department { Id = 2, Name = "Information Technology", ManagerName = "Dr. Sara" },
                new Department { Id = 3, Name = "Business Administration", ManagerName = "Dr. Omar" }
            );
            modelBuilder.Entity<Instructor>().HasData(
            new Instructor { Id = 1, Name = "Mostafa Kamel", Address = "Damietta", Salary = 12000, DepartmentId = 1 },
            new Instructor { Id = 2, Name = "Nour Hassan", Address = "Mansoura", Salary = 13500, DepartmentId = 1 },
            new Instructor { Id = 3, Name = "Youssef Adel", Address = "Cairo", Salary = 11000, DepartmentId = 2 },
            new Instructor { Id = 4, Name = "Mona Farid", Address = "Alexandria", Salary = 14500, DepartmentId = 3 }
        );
        }
    }
}


