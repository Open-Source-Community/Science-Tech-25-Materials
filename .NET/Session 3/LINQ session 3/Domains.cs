using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_session_3
{

    // ----------------------------------------------
    // 1) Domain Models
    // ----------------------------------------------
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double Grade { get; set; }
        public int DepartmentId { get; set; }
    }

    public class Department
    {
        public int DepartmentId { get; set; }
        public string DeptName { get; set; }
        public string Building { get; set; }
    }

    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public int DepartmentId { get; set; }
    }

    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
    }
}
