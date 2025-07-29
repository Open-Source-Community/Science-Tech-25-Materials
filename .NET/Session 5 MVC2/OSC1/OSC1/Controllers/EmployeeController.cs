using Microsoft.AspNetCore.Mvc;
using OSC1.Models;

namespace OSC1.Controllers
{
    public class EmployeeController : Controller
    {
        AppDbContext context = new AppDbContext();

        public IActionResult Index()
        {
            var emps = context.Employees.ToList();
            ViewData["msg"] = "Hello from controller";
            return View("Index", emps);
        }

        public IActionResult setTempData()
        {
            TempData["Name"] = "Namor"; //Deleted when session ends or when read
            return View();
        }

        public IActionResult getTempData()
        {
            ViewData["Name"] = TempData["Name"]; //Delete After Reading
            TempData.Keep("Name"); //To preven TempData From Deleting After Reading
            return View();
        }

        public IActionResult setCookie()
        {
            CookieOptions options = new CookieOptions();
            Response.Cookies.Append("Name", "Abdelhamid", options);
            Response.Cookies.Append("Age", "12");
            return View();
        }

        public IActionResult getCookie()
        {
            ViewData["Name"] = Request.Cookies["Name"];
            return View();
        }

        //To Set Session You need to set the configuration and middleware in the main
        public IActionResult setSession()
        {
            HttpContext.Session.SetString("Name", "Anthony");
            return View();
        }

        public IActionResult getSession()
        {
            ViewData["Name"] = HttpContext.Session.GetString("Name");
            return View();
        }

        //open form view
        public IActionResult New()
        {
            var emp = new Employee();
            return View("NewEmployee",emp);
        }
        //send data to server
        [HttpPost]
        public IActionResult NewEmp(Employee employee)
        {
            if(employee.Name != null)
            {
                context.Employees.Add(employee);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("NewEmployee",employee);
        }

        //open form view
        public IActionResult Edit(int id)
        {
            var emp = context.Employees.FirstOrDefault(x => x.Id == id);
            return View("Edit",emp);
        }

        //send data to controller to edit and save changes
        [HttpPost]
        public IActionResult EditEmp(int id, Employee employee)
        {
            
            var emp = context.Employees.FirstOrDefault(x => x.Id == id);
            if (emp != null && employee.Name != null)
            {
                emp.Name = employee.Name;
                emp.Salary = employee.Salary;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View("Edit",employee);
        }
    }
}
