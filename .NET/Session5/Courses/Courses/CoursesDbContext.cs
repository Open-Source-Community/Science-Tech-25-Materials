using Courses.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses
{
    public class CoursesDbContext : DbContext
    {

        public DbSet<Course> _courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Data Source=HADEER;Initial Catalog=Courses; Integrated Security = SSPI; TrustServerCertificate = True");

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configure relation one-to-many
            modelBuilder.Entity<Instructor>()
                        .HasOne(e => e._department)
                        .WithMany(d => d.Instructors)
                        .HasForeignKey(e => e.DeptId);

                        base.OnModelCreating(modelBuilder);

            // Work in Property Configuration

            modelBuilder.Entity<Instructor>()
                        .Property(a => a.Name)
                        .HasMaxLength(150)
                        .IsRequired();

            modelBuilder.Entity<Department>()
                        .Property(a => a.HiringDate)
                        .HasColumnType("Date");

            modelBuilder.Entity<Course>()
                        .Property(a => a.Duration)
                        .HasDefaultValue(2);




            #region Data seeding using method HasData()
            var students = new List<Student>
            {
                new Student {Id = 1,FName = "John", LName = "Doe", StAddress = "123 Main St", StAge = 20,  },
                new Student {Id = 2,FName = "Jane", LName = "Smith", StAddress = "456 Elm St", StAge = 22,  },
                new Student {Id = 3,FName = "Alice", LName = "Johnson", StAddress = "789 Oak St", StAge = 21},
                new Student {Id = 4,FName = "Bob", LName = "Brown", StAddress = "101 Pine St", StAge = 23,  },
                new Student {Id = 5,FName = "Charlie", LName = "Davis", StAddress = "202 Maple St", StAge =22 }
            };
            modelBuilder.Entity<Student>().HasData(students);

            var courses = new List<Course>
            {
                new Course {Id=1,Name = "Mathematics", Duration = 30, Description = "Introduction to Algebra and Calculus" },
                new Course {Id=2,Name = "Physics", Duration = 40, Description = "Fundamentals of Physics" },
                new Course {Id=3,Name = "Chemistry", Duration = 35, Description = "Basic Principles of Chemistry" },
                new Course {Id=4,Name = "Biology", Duration = 25, Description = "Introduction to Biological Sciences" },

            };

            modelBuilder.Entity<Course>().HasData(courses);
            var departments = new List<Department>
            {
                new Department {Id = 1,Name = "Physics", HiringDate = new DateTime(2019, 5, 20) },
                new Department {Id = 2,Name = "Mathematics", HiringDate = new DateTime(2020, 1, 15) },
                new Department {Id = 3,Name = "Chemistry", HiringDate = new DateTime(2021, 3, 10) },
                new Department {Id = 4,Name = "Biology", HiringDate = new DateTime(2018, 7, 25) }
            };
            modelBuilder.Entity<Department>().HasData(departments);


            var instructors = new List<Instructor>
            {
                new Instructor { Id =1, Name = "Dr. Smith", Address = "123 University Ave", Salary = 75000.00m, HourRate = 50.00m, Bonus = 5000.00m,DeptId=1 },
                new Instructor {Id =2, Name = "Dr. Johnson", Address = "456 College St", Salary = 80000.00m, HourRate = 55.00m, Bonus = 6000.00m,DeptId=1  },
                new Instructor {Id =3, Name = "Dr. Brown", Address = "789 Campus Rd", Salary = 85000.00m, HourRate = 60.00m, Bonus = 7000.00m ,DeptId=1 },
                new Instructor {Id =4, Name = "Dr. Davis", Address = "101 School Ln", Salary = 90000.00m, HourRate = 65.00m, Bonus = 8000.00m,DeptId=2 }

            };

            modelBuilder.Entity<Instructor>().HasData(instructors);


            #endregion
        }

    }
}
