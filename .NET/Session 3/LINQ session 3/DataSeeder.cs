using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_session_3
{
    // ----------------------------------------------
    // 2) Data Seeder
    //    Provides multiple objects to demonstrate LINQ
    // ----------------------------------------------
    public static class DataSeeder
    {
        public static List<Student> GetStudents()
        {
            return new List<Student>
            {
                new Student { StudentId = 1, Name = "Alice",   Age = 17, Grade = 88.5, DepartmentId = 1 },
                new Student { StudentId = 2, Name = "Bob",     Age = 19, Grade = 91.0, DepartmentId = 2 },
                new Student { StudentId = 3, Name = "Charlie", Age = 18, Grade = 75.5, DepartmentId = 2 },
                new Student { StudentId = 4, Name = "Diana",   Age = 21, Grade = 82.0, DepartmentId = 3 },
                new Student { StudentId = 5, Name = "Eve",     Age = 20, Grade = 95.0, DepartmentId = 1 },
                new Student { StudentId = 6, Name = "Frank",   Age = 22, Grade = 67.5, DepartmentId = 3 },
                new Student { StudentId = 7, Name = "Grace",   Age = 19, Grade = 92.3, DepartmentId = 2 }
            };
        }

        public static List<Department> GetDepartments()
        {
            return new List<Department>
            {
                new Department { DepartmentId = 1, DeptName = "Computer Science", Building = "Engineering Hall" },
                new Department { DepartmentId = 2, DeptName = "Mathematics",      Building = "Science Wing" },
                new Department { DepartmentId = 3, DeptName = "Literature",       Building = "Humanities Block" }
            };
        }

        public static List<Course> GetCourses()
        {
            return new List<Course>
            {
                new Course { CourseId = 101, Title = "Intro to Programming", DepartmentId = 1 },
                new Course { CourseId = 102, Title = "Data Structures",      DepartmentId = 1 },
                new Course { CourseId = 201, Title = "Calculus I",           DepartmentId = 2 },
                new Course { CourseId = 202, Title = "Discrete Math",        DepartmentId = 2 },
                new Course { CourseId = 301, Title = "World Literature",     DepartmentId = 3 },
                new Course { CourseId = 302, Title = "Creative Writing",     DepartmentId = 3 }
            };
        }

        public static List<Teacher> GetTeachers()
        {
            return new List<Teacher>
            {
                new Teacher { TeacherId = 1, Name = "Dr. Smith",   DepartmentId = 1 },
                new Teacher { TeacherId = 2, Name = "Dr. Johnson", DepartmentId = 2 },
                new Teacher { TeacherId = 3, Name = "Prof. Brown", DepartmentId = 3 },
                new Teacher { TeacherId = 4, Name = "Dr. Miller",  DepartmentId = 2 }
            };
        }
    }
}
