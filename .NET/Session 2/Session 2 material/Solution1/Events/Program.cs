using System;

namespace EmployeeSalaryEvent
{
    // Publisher: Company (Notifies employees of salary changes)
    class Company
    {
        
        public delegate void SalaryChangedHandler(string employeeName, decimal newSalary);

      
        public event SalaryChangedHandler OnSalaryChanged;

       
        public void ChangeSalary(string employeeName, decimal newSalary)
        {
            

            // Raise the event
            OnSalaryChanged?.Invoke(employeeName, newSalary);

        }
    }

    // Subscriber: Employee (Receives notifications when salary changes)
    class Employee
    {
        public string Name { get; }

        public Employee(string name)
        {
            Name = name;
        }

        // Event handler to respond to salary change
        public void NotifySalaryChange(string employeeName, decimal newSalary)
        {
            if (Name == employeeName)
            {
                Console.WriteLine($"Notification: {Name}, your new salary is ${newSalary}");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            // Create company instance (Publisher)
            Company company = new Company();

            // Create employee instances (Subscribers)
            Employee emp1 = new Employee("Alice");
            Employee emp2 = new Employee("Bob");

            // Step 4: Subscribe employees to the salary change event
            company.OnSalaryChanged += emp1.NotifySalaryChange;
            company.OnSalaryChanged += emp2.NotifySalaryChange;

            // Change salaries (Events are triggered)
            company.ChangeSalary("Alice", 5000);
            company.ChangeSalary("Bob", 6000);

            // Step 5: Unsubscribe an employee
            company.OnSalaryChanged -= emp2.NotifySalaryChange;


            Console.WriteLine("\nAfter Unsubscribing Alice...\n");

            // Change salary again (Only Bob should receive notification)
            company.ChangeSalary("Bob", 6500);
        }
    }
}
