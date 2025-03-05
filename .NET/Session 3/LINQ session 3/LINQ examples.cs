using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_session_3
{
    // ----------------------------------------------
    //  LINQ Examples - Each Topic
    // ----------------------------------------------
    public static class LINQExamples
    {
        // Data
        private static List<Student> _students = DataSeeder.GetStudents();
        private static List<Department> _departments = DataSeeder.GetDepartments();
        private static List<Course> _courses = DataSeeder.GetCourses();
        private static List<Teacher> _teachers = DataSeeder.GetTeachers();

        // Helper method to reset data (if we need fresh state each time)
        public static void ResetData()
        {
            _students = DataSeeder.GetStudents();
            _departments = DataSeeder.GetDepartments();
            _courses = DataSeeder.GetCourses();
            _teachers = DataSeeder.GetTeachers();
        }

        // Helper method to print a list of students
        private static void PrintStudents(IEnumerable<Student> students, string message = "")
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine(message);
            }
            foreach (var s in students)
            {
                Console.WriteLine($"StudentId: {s.StudentId}, Name: {s.Name}, Age: {s.Age}, Grade: {s.Grade}, DeptId: {s.DepartmentId}");
            }
            Console.WriteLine();
        }

        // ----------------------------------------------
        // (1) Introduction to LINQ
        // ----------------------------------------------
        public static void IntroductionToLINQ()
        {
            Console.WriteLine("=== (1) Introduction to LINQ ===");

            // METHOD SYNTAX: find all students age >= 18
            //Ienumerable types 
            var adultStudentsMethod = _students
                .Where(s => s.Age >= 18)//return s if s.age >=18
                .ToList();//what not how 4th generation  

            PrintStudents(adultStudentsMethod, "Method Syntax: Students age >= 18:");

            // QUERY SYNTAX: find all students age >= 18
            var adultStudentsQuery = (from s in _students
                                      where s.Age >= 18
                                      select s).ToList();

            PrintStudents(adultStudentsQuery, "Query Syntax: Students age >= 18:");
        }

        // ----------------------------------------------
        // (3) Pure vs. Impure Functions
        // ----------------------------------------------
        // A pure function: no side effects, always returns the same result for the same input
        private static int Square(int x)
        {
            return x * x;
        }

        // An impure function: modifies external state (a global/static variable)
        private static int _globalCounter = 0;
        private static int IncrementAndGetGlobalCounter()
        {
            _globalCounter++;
            return _globalCounter;
        }

        public static void PureVsImpureFunctions()
        {
            Console.WriteLine("=== (3) Pure vs. Impure Functions ===");

            // Demonstrate pure function
            Console.WriteLine($"Square(4) = {Square(4)}");
            Console.WriteLine($"Square(4) = {Square(4)} (again, same result)\n");

            // Demonstrate impure function
            Console.WriteLine($"Impure function call #1 = {IncrementAndGetGlobalCounter()}");
            Console.WriteLine($"Impure function call #2 = {IncrementAndGetGlobalCounter()}");
            Console.WriteLine($"Impure function call #3 = {IncrementAndGetGlobalCounter()}");
            Console.WriteLine("Notice how the result changes with each call because it modifies external state.\n");
        }


        // ----------------------------------------------
        // (5) Projection Operations (Select, SelectMany)
        // ----------------------------------------------
        public static void ProjectionOperations()
        {
            Console.WriteLine("=== (5) Projection Operations ===");

            // METHOD SYNTAX: Basic projection using Select
            var studentNamesAndGradesMethod = _students
                .Select(s => new { s.Name, s.Grade })
                .ToList();

            Console.WriteLine("Method Syntax: Projecting Student Name and Grade:");
            foreach (var item in studentNamesAndGradesMethod)
            {
                Console.WriteLine($"Name: {item.Name}, Grade: {item.Grade}");
            }
            Console.WriteLine();

            // QUERY SYNTAX: Basic projection using Select
            var studentNamesAndGradesQuery = (from s in _students
                                              select new { s.Name, s.Grade }).ToList();

            Console.WriteLine("Query Syntax: Projecting Student Name and Grade:");
            foreach (var item in studentNamesAndGradesQuery)
            {
                Console.WriteLine($"Name: {item.Name}, Grade: {item.Grade}");
            }
            Console.WriteLine();

            // Example of SelectMany (method syntax)
            var studentsWithNicknames = _students
                .Select(s => new
                {
                    s.Name,
                    Nicknames = new List<string> { s.Name + "y", s.Name + "ster" }
                })
                .ToList();

            var allNicknamesMethod = studentsWithNicknames
                .SelectMany(x => x.Nicknames)
                .ToList();

            Console.WriteLine("Method Syntax: All nicknames using SelectMany:");
            foreach (var nickname in allNicknamesMethod)
            {
                Console.WriteLine(nickname);
            }
            Console.WriteLine();

            // Query Syntax doesn’t have a direct “SelectMany” keyword, 
            // but we can do something similar by from-from-select:
            var allNicknamesQuery = (from s in _students
                                     from nickname in new List<string> { s.Name + "y", s.Name + "ster" }
                                     select nickname).ToList();

            Console.WriteLine("Query Syntax: All nicknames (from-from-select):");
            foreach (var nickname in allNicknamesQuery)
            {
                Console.WriteLine(nickname);
            }
            Console.WriteLine();
        }

        // ----------------------------------------------
        // (6) Sorting Data (OrderBy, ThenBy, etc.)
        // ----------------------------------------------
        public static void SortingData()
        {
            Console.WriteLine("=== (6) Sorting Data ===");

            // METHOD SYNTAX: sort by grade ascending
            var sortedByGradeAscMethod = _students
                .OrderBy(s => s.Grade)
                .ToList();
            PrintStudents(sortedByGradeAscMethod, "Method Syntax: Students sorted by Grade (asc):");

            // QUERY SYNTAX: sort by grade ascending
            var sortedByGradeAscQuery = (from s in _students
                                         orderby s.Grade ascending
                                         select s).ToList();
            PrintStudents(sortedByGradeAscQuery, "Query Syntax: Students sorted by Grade (asc):");

            // METHOD SYNTAX: sort by grade desc, then name asc
            var sortedByGradeDescThenNameMethod = _students
                .OrderByDescending(s => s.Grade)
                .ThenBy(s => s.Name)
                .ToList();
            PrintStudents(sortedByGradeDescThenNameMethod, "Method Syntax: Grade desc, then Name asc:");

            // QUERY SYNTAX: sort by grade desc, then name asc
            var sortedByGradeDescThenNameQuery = (from s in _students
                                                  orderby s.Grade descending, s.Name ascending
                                                  select s).ToList();
            PrintStudents(sortedByGradeDescThenNameQuery, "Query Syntax: Grade desc, then Name asc:");
        }

        // ----------------------------------------------
        // (7) Data Partitioning (Skip, Take, etc.)
        // ----------------------------------------------
        public static void DataPartitioning()
        {
            Console.WriteLine("=== (7) Data Partitioning ===");

            // Let's sort by Age first for demonstration
            var sortedByAge = _students.OrderBy(s => s.Age).ToList();

            // METHOD SYNTAX: Take(3)
            var firstThreeMethod = sortedByAge.Take(3).ToList();
            PrintStudents(firstThreeMethod, "Method Syntax: First 3 students by ascending Age:");

            // QUERY SYNTAX: There's no direct "take" in query syntax, so we do:
            var firstThreeQuery = (from s in sortedByAge
                                   select s).Take(3).ToList();
            PrintStudents(firstThreeQuery, "Query Syntax: First 3 students by ascending Age:");

            // METHOD SYNTAX: Skip(3)
            var skipThreeMethod = sortedByAge.Skip(3).ToList();
            PrintStudents(skipThreeMethod, "Method Syntax: Skip first 3 students by ascending Age:");

            // QUERY SYNTAX: (no direct skip in query syntax)
            var skipThreeQuery = (from s in sortedByAge
                                  select s).Skip(3).ToList();
            PrintStudents(skipThreeQuery, "Query Syntax: Skip first 3 students by ascending Age:");

            // METHOD SYNTAX: TakeWhile
            var takeWhileUnder20Method = sortedByAge.TakeWhile(s => s.Age < 20).ToList();
            PrintStudents(takeWhileUnder20Method, "Method Syntax: Students taken while Age < 20:");

            // METHOD SYNTAX: SkipWhile
            var skipWhileUnder20Method = sortedByAge.SkipWhile(s => s.Age < 20).ToList();
            PrintStudents(skipWhileUnder20Method, "Method Syntax: Students skipped while Age < 20:");

           
           
        }

        // ----------------------------------------------
        // (8) Quantifiers (Any, All, Contains)
        // ----------------------------------------------
        public static void Quantifiers()
        {
            Console.WriteLine("=== (8) Quantifiers ===");

            // METHOD SYNTAX: Are there any students with Grade > 90?
            bool anyHighAchieversMethod = _students.Any(s => s.Grade > 90);
            Console.WriteLine($"Method Syntax: Any student with Grade > 90? {anyHighAchieversMethod}");

            // QUERY SYNTAX: We can combine query with .Any() method
            bool anyHighAchieversQuery = (from s in _students
                                          where s.Grade > 90
                                          select s).Any();
            Console.WriteLine($"Query Syntax: Any student with Grade > 90? {anyHighAchieversQuery}\n");

            // METHOD SYNTAX: Do all students have Age >= 18?
            bool allAdultsMethod = _students.All(s => s.Age >= 18);
            Console.WriteLine($"Method Syntax: All students 18 or older? {allAdultsMethod}");

            // QUERY SYNTAX: combine query with .All() method
            bool allAdultsQuery = (from s in _students
                                   select s).All(s => s.Age >= 18);
            Console.WriteLine($"Query Syntax: All students 18 or older? {allAdultsQuery}\n");

            // METHOD SYNTAX: Contains
            var firstStudent = _students[0];
            bool containsFirstMethod = _students.Contains(firstStudent);
            Console.WriteLine($"Method Syntax: List contains first student object? {containsFirstMethod}");

            // There's no direct "Contains" in query syntax for an object reference check, 
            // but we can do:
            bool containsFirstQuery = (from s in _students
                                       select s).Contains(firstStudent);
            Console.WriteLine($"Query Syntax: List contains first student object? {containsFirstQuery}\n");
        }

        // ----------------------------------------------
        // (9) Join Operations
        // ----------------------------------------------
        public static void JoinOperations()
        {
            Console.WriteLine("=== (9) Join Operations ===");

            // METHOD SYNTAX: Join students with their department (inner join)
            var studentDeptJoinMethod = _students
                .Join(_departments,
                      student => student.DepartmentId,
                      dept => dept.DepartmentId,
                      (student, dept) => new
                      {
                          StudentName = student.Name,
                          DepartmentName = dept.DeptName
                      })
                .ToList();

            Console.WriteLine("Method Syntax: Student - Department (Join):");
            foreach (var item in studentDeptJoinMethod)
            {
                Console.WriteLine($"Student: {item.StudentName}, Dept: {item.DepartmentName}");
            }
            Console.WriteLine();

            // QUERY SYNTAX: Teachers with their departments
            var teacherDeptJoinQuery = from teacher in _teachers
                                       join dept in _departments
                                       on teacher.DepartmentId equals dept.DepartmentId
                                       select new
                                       {
                                           TeacherName = teacher.Name,
                                           DepartmentName = dept.DeptName
                                       };

            Console.WriteLine("Query Syntax: Teacher - Department (Join):");
            foreach (var item in teacherDeptJoinQuery)
            {
                Console.WriteLine($"Teacher: {item.TeacherName}, Dept: {item.DepartmentName}");
            }
            Console.WriteLine();
        }

        // ----------------------------------------------
        // (10) Generation Operations (Range, Repeat, Empty)
        // ----------------------------------------------
        public static void GenerationOperations()
        {
            Console.WriteLine("=== (10) Generation Operations ===");

            // METHOD SYNTAX: Range
            var rangeNumbersMethod = Enumerable.Range(1, 5);
            Console.WriteLine("Method Syntax: Enumerable.Range(1, 5):");
            foreach (var num in rangeNumbersMethod)
            {
                Console.WriteLine(num);
            }
            Console.WriteLine();

            // QUERY SYNTAX: range doesn't have direct query syntax, but we can do:
            var rangeNumbersQuery = from n in Enumerable.Range(1, 5)
                                    select n;
            Console.WriteLine("Query Syntax: (from n in Enumerable.Range(1,5) select n):");
            foreach (var num in rangeNumbersQuery)
            {
                Console.WriteLine(num);
            }
            Console.WriteLine();

            // METHOD SYNTAX: Repeat
            var repeatedStringsMethod = Enumerable.Repeat("Hello", 3);
            Console.WriteLine("Method Syntax: Enumerable.Repeat(\"Hello\", 3):");
            foreach (var str in repeatedStringsMethod)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine();

            // QUERY SYNTAX: similarly, we can do:
            var repeatedStringsQuery = from s in Enumerable.Repeat("Hello", 3)
                                       select s;
            Console.WriteLine("Query Syntax: (from s in Enumerable.Repeat(\"Hello\", 3) select s):");
            foreach (var str in repeatedStringsQuery)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine();

            // METHOD SYNTAX: Empty
            var emptySequenceMethod = Enumerable.Empty<int>();
            Console.WriteLine("Method Syntax: Enumerable.Empty<int>() has no elements:");
            Console.WriteLine($"Count = {emptySequenceMethod.Count()}");
            Console.WriteLine();

            // QUERY SYNTAX: We can combine with a from statement:
            var emptySequenceQuery = from x in Enumerable.Empty<int>()
                                     select x;
            Console.WriteLine("Query Syntax: (from x in Enumerable.Empty<int>() select x):");
            Console.WriteLine($"Count = {emptySequenceQuery.Count()}");
            Console.WriteLine();
        }

        // ----------------------------------------------
        // (11) Element Operations (First, Single, etc.)
        // ----------------------------------------------
        public static void ElementOperations()
        {
            Console.WriteLine("=== (11) Element Operations ===");

            // METHOD SYNTAX: First
            var firstStudentMethod = _students.First();
            Console.WriteLine($"Method Syntax: First student in the list: {firstStudentMethod.Name}");

            // QUERY SYNTAX: we can do partial query, then call First()
            var firstStudentQuery = (from s in _students
                                     select s).First();
            Console.WriteLine($"Query Syntax: First student in the list: {firstStudentQuery.Name}\n");

            // METHOD SYNTAX: FirstOrDefault (Age < 18)
            var firstOrDefaultUnder18Method = _students.FirstOrDefault(s => s.Age < 18);
            Console.WriteLine($"Method Syntax: First student with Age < 18: {firstOrDefaultUnder18Method?.Name ?? "None"}");

            // QUERY SYNTAX:
            var firstOrDefaultUnder18Query = (from s in _students
                                              where s.Age < 18
                                              select s).FirstOrDefault();
            Console.WriteLine($"Query Syntax: First student with Age < 18: {firstOrDefaultUnder18Query?.Name ?? "None"}\n");

            // METHOD SYNTAX: Single
            var singleStudentMethod = _students.Single(s => s.StudentId == 1);
            Console.WriteLine($"Method Syntax: Single student with Id == 1: {singleStudentMethod.Name}");

            // QUERY SYNTAX: 
            var singleStudentQuery = (from s in _students
                                      where s.StudentId == 1
                                      select s).Single();
            Console.WriteLine($"Query Syntax: Single student with Id == 1: {singleStudentQuery.Name}\n");

            // METHOD SYNTAX: SingleOrDefault
            var singleOrDefaultStudentMethod = _students.SingleOrDefault(s => s.Name == "Unknown");
            Console.WriteLine($"Method Syntax: SingleOrDefault for non-existing name: {(singleOrDefaultStudentMethod == null ? "null" : singleOrDefaultStudentMethod.Name)}\n");

            // METHOD SYNTAX: ElementAt
            var secondStudentMethod = _students.ElementAt(1);
            Console.WriteLine($"Method Syntax: ElementAt(1) => second student in list: {secondStudentMethod.Name}");

            // QUERY SYNTAX: no direct ElementAt, we do partial:
            var secondStudentQuery = (from s in _students
                                      select s).ElementAt(1);
            Console.WriteLine($"Query Syntax: ElementAt(1) => second student in list: {secondStudentQuery.Name}\n");
        }

        // ----------------------------------------------
        // (12) Equality Operations (SequenceEqual)
        // ----------------------------------------------
        public static void EqualityOperations()
        {
            Console.WriteLine("=== (12) Equality Operations ===");

            var sequence1 = new List<int> { 1, 2, 3, 4, 5 };
            var sequence2 = new List<int> { 1, 2, 3, 4, 5 };
            var sequence3 = new List<int> { 1, 2, 3, 5, 4 }; // Different order

            // METHOD SYNTAX: SequenceEqual
            bool seq1EqualsSeq2 = sequence1.SequenceEqual(sequence2);
            bool seq1EqualsSeq3 = sequence1.SequenceEqual(sequence3);

            Console.WriteLine($"Method Syntax: sequence1 SequenceEqual sequence2? {seq1EqualsSeq2}");
            Console.WriteLine($"Method Syntax: sequence1 SequenceEqual sequence3? {seq1EqualsSeq3}");

            // QUERY SYNTAX: There's no direct query syntax for SequenceEqual,
            // but we can do partial:
            bool seq1EqualsSeq2Query = (from n in sequence1 select n)
                .SequenceEqual(from n in sequence2 select n);
            Console.WriteLine($"Query Syntax (partial): seq1 vs seq2? {seq1EqualsSeq2Query}\n");
        }

        // ----------------------------------------------
        // (13) Concatenation Operations (Concat)
        // ----------------------------------------------
        public static void ConcatenationOperations()
        {
            Console.WriteLine("=== (13) Concatenation Operations ===");

            var listA = new List<string> { "A", "B", "C" };
            var listB = new List<string> { "D", "E", "F" };

            // METHOD SYNTAX: Concat
            var concatenatedMethod = listA.Concat(listB);
            Console.WriteLine("Method Syntax: Concat listA + listB:");
            foreach (var item in concatenatedMethod)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            // QUERY SYNTAX: partial approach
            var concatenatedQuery = (from x in listA select x)
                .Concat(from y in listB select y);

            Console.WriteLine("Query Syntax (partial): Concat listA + listB:");
            foreach (var item in concatenatedQuery)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        // ----------------------------------------------
        // (14) Aggregate Operations (Sum, Min, Max, etc.)
        // ----------------------------------------------
        public static void AggregateOperations()
        {
            Console.WriteLine("=== (14) Aggregate Operations ===");

            // METHOD SYNTAX
            var totalGradesMethod = _students.Sum(s => s.Grade);
            Console.WriteLine($"Method Syntax: Sum of all student grades: {totalGradesMethod}");

            var minGradeMethod = _students.Min(s => s.Grade);
            Console.WriteLine($"Method Syntax: Minimum grade: {minGradeMethod}");

            var maxGradeMethod = _students.Max(s => s.Grade);
            Console.WriteLine($"Method Syntax: Maximum grade: {maxGradeMethod}");

            var averageGradeMethod = _students.Average(s => s.Grade);
            Console.WriteLine($"Method Syntax: Average grade: {averageGradeMethod}");

            // Query syntax for sums, etc.
            var totalGradesQuery = (from s in _students
                                    select s.Grade).Sum();
            Console.WriteLine($"Query Syntax: Sum of all student grades: {totalGradesQuery}");

            // Example: create a comma-separated list of student names using Aggregate (method syntax)
            var allNames = _students
                .Select(s => s.Name)//ahmed ,  mohammed , ali ans aml    current ="";
                .Aggregate((current, next) => current + ", " + next);

            Console.WriteLine($"Method Syntax: All student names (Aggregate): {allNames}\n");
        }

        // ----------------------------------------------
        // (15) Set Operations (Distinct, Union, Intersect, Except)
        // ----------------------------------------------
        public static void SetOperations()
        {
            Console.WriteLine("=== (15) Set Operations ===");

            // Distinct
            var duplicates = new List<int> { 1, 2, 2, 3, 3, 3, 4 };

            // METHOD SYNTAX: Distinct
            var distinctValuesMethod = duplicates.Distinct();
            Console.WriteLine("Method Syntax: Distinct values from [1,2,2,3,3,3,4]:");
            foreach (var val in distinctValuesMethod)
            {
                Console.WriteLine(val);
            }
            Console.WriteLine();

            // QUERY SYNTAX: partial approach
            var distinctValuesQuery = (from d in duplicates
                                       select d).Distinct();
            Console.WriteLine("Query Syntax (partial): Distinct values from [1,2,2,3,3,3,4]:");
            foreach (var val in distinctValuesQuery)
            {
                Console.WriteLine(val);
            }
            Console.WriteLine();

            // Union
            var setA = new List<int> { 1, 2, 3 };
            var setB = new List<int> { 3, 4, 5 };

       
            // Intersect
            var intersectSetMethod = setA.Intersect(setB);
            Console.WriteLine("Method Syntax: Intersect of [1,2,3] and [3,4,5]:");
            foreach (var val in intersectSetMethod)
            {
                Console.WriteLine(val);
            }
            Console.WriteLine();

            // QUERY SYNTAX: partial approach
            var intersectSetQuery = (from x in setA select x)
                .Intersect(from y in setB select y);
            Console.WriteLine("Query Syntax (partial): Intersect of [1,2,3] and [3,4,5]:");
            foreach (var val in intersectSetQuery)
            {
                Console.WriteLine(val);
            }
            Console.WriteLine();

            // Except
            var exceptSetMethod = setA.Except(setB);
            Console.WriteLine("Method Syntax: Except of [1,2,3] from [3,4,5] (setA.Except(setB)):");
            foreach (var val in exceptSetMethod)
            {
                Console.WriteLine(val);
            }
            Console.WriteLine();

            // QUERY SYNTAX: partial approach
            var exceptSetQuery = (from x in setA select x)
                .Except(from y in setB select y);
            Console.WriteLine("Query Syntax (partial): Except of [1,2,3] from [3,4,5]:");
            foreach (var val in exceptSetQuery)
            {
                Console.WriteLine(val);
            }
            Console.WriteLine();
        }
    }
}
