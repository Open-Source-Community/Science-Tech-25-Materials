namespace Courses.Model
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime HiringDate { get; set; }

        public virtual List<Instructor> Instructors { get; set;}
    }
}
