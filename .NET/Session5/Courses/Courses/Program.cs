using Courses.Model;
using Microsoft.EntityFrameworkCore;

namespace Courses
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var contx= new CoursesDbContext();

            var students = contx.Students;


            #region update and delete

            //contx.Students.FirstOrDefault().FName = "aaaaaaa";

            //contx.Students.Remove(contx.Students.FirstOrDefault());
            //contx.SaveChanges();
            //foreach (var student in students)
            //{
            //    Console.WriteLine(student.FName);
            //}
            #endregion

            #region ExecuteUpdate & ExecuteDelete
            //contx.Students.Where(a => a.Id >= 5)
            //            .ExecuteUpdate(a => a.SetProperty(a => a.FName, "xxxxx"));


            //contx.Students.Where(a => a.Id == 5).ExecuteDelete(); 
            #endregion


            #region Eager Loading
            //var depts = contx.Departments.Include(a=>a.Instructors).ToList();

            //foreach (var dep in depts)
            //{
            //    Console.WriteLine($"dep name : {dep.Name}");

            //    foreach(var ins in dep.Instructors)
            //    {
            //        Console.WriteLine($"ins name: {ins.Name}");
            //    }
            //    Console.WriteLine();
            //} 
            #endregion

            #region Explicit Loading
            //var inst = contx.Instructors.ToList();

            //contx.Entry(inst.FirstOrDefault()).Reference(a => a._department).Load();

            //var dep = contx.Departments.ToList();

            //contx.Entry(dep.FirstOrDefault()).Collection(a => a.Instructors).Load(); 
            #endregion


            #region Lazy Loading
            /*
             to enable lazy loading in EF Core:

                1. Download package Microsoft.EntityFrameworkCore.Proxies
                2. enable it in dbcontext by calling method UseLazyLoadingProxies() before using sql server 
                3. Make all Navigation properity " 
             */


            //var isnt = contx.Instructors.FirstOrDefault();

            //Console.WriteLine($"instructor: {isnt.Name} is in department {isnt._department.Name}");

            #endregion


        }
    }
}
