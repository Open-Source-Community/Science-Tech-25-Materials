---

# 📘 ASP.NET MVC Course Content (AR / EN)

---

## 1. Difference Between Web App and Desktop App

- Web apps run in the browser, are accessible over the internet, and are hosted on a server.
- Desktop apps run directly on the operating system, installed locally, and often faster.

---

## 2. Web App Hosting

- Web apps need to be **hosted** on a server to be accessed online.
- Hosting types: Shared, VPS, Cloud (Azure, AWS).

---

## 3. History of Web Apps

- Early 90s: static HTML pages.
- Late 90s: dynamic content with PHP, ASP.
- 2000s: MVC frameworks, AJAX.
- Now: APIs + Frontend frameworks.

---

## 4. Development Types of Web Apps

- Static Web Apps
- Dynamic Web Apps
- SPA (Single Page App)
- PWA (Progressive Web App)

---

## 5. MVC Design Pattern

- **Model:** represents the data.
- **View:** displays the UI.
- **Controller:** handles user input and interaction.

---

## 6. URL Mapping (Routing)

- Maps URL to a specific controller and action.
- Example: `/Employee/Index` calls `EmployeeController.Index()`

---

## 7. Create New Project

- Open Visual Studio → Create New ASP.NET Core Web App (Model-View-Controller).
- Choose .NET version, enable HTTPS.

---

## 8. Virtual Hosting vs Self Hosting

- **Virtual Hosting:** Hosted on IIS, Nginx, or Apache.
- **Self Hosting:** Use Kestrel directly to run app.

---

## 9. Project Overview and GetAllData (Index Action)

#### 🔸 Project Structure

```
/Models         => contains the Employee class
/Controllers    => contains EmployeeController.cs
/Views
   /Employee    => contains Index.cshtml, Create.cshtml
```

#### 🔸 Model

```csharp
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Salary { get; set; }
}
```

#### 🔸 Controller (Index)

```csharp
public IActionResult Index()
{
    var employees = _context.Employees.ToList();
    return View(employees);
}
```

#### 🔸 View (Index.cshtml)

```html
@model List<Employee>

<table>
@foreach (var emp in Model)
{
    <tr>
        <td>@emp.Id</td>
        <td>@emp.Name</td>
        <td>@emp.Salary</td>
    </tr>
}
</table>