namespace Courses.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string StAddress { get; set; }
        public int StAge { get; set; }

        public virtual ICollection<Course> courses { get; set; }
    }
}
