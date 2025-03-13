using System.ComponentModel.DataAnnotations;

namespace Courses.Model
{
    public class Instructor
    {
        // Id <class>Id
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public decimal HourRate { get; set; }
        public decimal Bonus { get; set; }

        public virtual  ICollection<Course> Course { get; set; }

        public  int DeptId { get; set; }
        public virtual Department _department { get; set; }

    }
}
