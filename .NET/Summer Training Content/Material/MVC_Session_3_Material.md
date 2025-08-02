**1️- Partial View:**

-  **A small, reusable Razor view.**
    
-  **Included in a larger view to render part of the page.**
    
    - Think of it like a **view _without_ its own layout** (no full page structure).
        
-  **Often used for:**
    
    - Reusable UI pieces (e.g., a product card, menu, footer).
        
    - Keeping code clean and modular.
        
    - Loading content dynamically via AJAX (e.g., updating a section without full reload).
        

---

**How to create:**

- The process is the **same as creating a normal view.**
    
- ✅ Just **check the option "Create as a partial view"** (in Visual Studio, for example).

 - <img src="img/selcet.png" alt="Screenshot" width="1000"/>

 ---
 - it is recommended to start partial view with " _ " like `_EmoCard.cshtml

``` csharp
@model Employee

<h1>@Model.Name</h1>
<h2>@Model.DepartmentID</h2>
<h2>@Model.Salary</h2>
<h2>@Model.Address</h2>

```
##  Why you **don’t include things like:**

- `ViewData`
    
- `<head>` tags
    
- `<script>` or `<link>` includes (like jQuery, CSS)
    

 **Because:**
 
- The **partial view is _injected into_ a main view.**
    
- The **main view already has the full layout:**
    
    - HTML `<head>`, `<body>`, title, scripts, styles, etc.
        
- The partial view is meant to **focus only on the small section of content** it is responsible for. 
 
 - As we see it can easy take a Model 
 
 - #### **To include a Partial View in the Main View**:
 
**1️⃣ Using HTML Helper:**
``` csharp
@Html.Partial("_NavPartial")
```

OR (recommended for async):
``` csharp
@await Html.PartialAsync("_NavPartial")
```

OR using **RenderPartial** (writes directly to the output stream):
``` csharp
@{   
Html.RenderPartial("_NavPartial");
//or 
await Html.RenderPartialAsync("_NavPartial"); 
}
```
✅ **Notes:**

- `Html.Partial()` returns an `IHtmlContent` (you output it).
    
- `Html.RenderPartial()` writes directly to the response stream (slightly more efficient but less flexible in Razor pages).
    

---

**2️⃣ Using Tag Helper ( _recommended_ for Razor Pages and MVC):**

``` csharp
 <partial name="_NavPartial" />
```

---

## **Note:**
 by default A **partial view** will **inherit the model** from the **parent (main) view** **unless you explicitly pass a different model.**

##### -  What if you want to pass a **different model**?
You can **explicitly pass a model** when rendering:

1- By Tag Helper :
``` csharp
<partial name="_EmployeeDetails" model="Model.EmployeeDetails" />
```
`model` is an attribute , then pass it the new model you need 

2- By Html Helper :
``` csharp
@await Html.PartialAsync("_EmployeeDetails", Model.EmployeeDetails);
```

---
- At Action I Can Return a partial view :
``` csharp
public IActionResult EmpCardPartial(int id)
{
return PartialView("_EmpCard",EmployeeRepository.GetById(id));//Model=Null
}

```

## We have a Question why we return a partial view ?

**We return a partial view:**

-  **When we don’t want to refresh the whole page**  
    ➔ This is where **AJAX** comes in: you send an AJAX request, and the server returns **only the partial view**, which JavaScript injects into the page dynamically.
    
-  **When we want to reuse a small, specific part of the UI in multiple places**  
    ➔ Even **without AJAX**, you can render partial views **inside other views** to avoid repeating code.


---
<img src="img/Ajax.png" alt="Ajax" width="1000"/>

 
 - ###  AJAX Request
  - it's used to update part of the page without reloading the whole thing.
  
  - ## But technically:

- **The HTTP request itself is a _full request_.**
    
    - When you send an AJAX request using `XMLHttpRequest`, it **goes through the full HTTP pipeline:**
        
        - The browser sends **full HTTP headers, cookies, and body** (if applicable).
            
        - The server (like ASP.NET) sees it as a **normal HTTP request**.
            
- What makes it **feel "partial"** is **how you _handle the response on the client side._**
    
    - Instead of reloading the **whole page** (like with a normal form submission),
        
    - You **use JavaScript to update just part of the page** (e.g., a `<div>`, a table, a form section)

- **XmlHttpRequest:**  
    ➔ This is the JavaScript object that **creates and manages AJAX requests.** and received ***XML or JSON***  
- **js "Dom":**  
    ➔ After the response is received, **JavaScript updates the DOM (HTML content)** dynamically.
- **CSS:**  
    ➔ You can also manipulate CSS (styles) as part of the dynamic update.

|**Aspect**|**Normal HTTP Request**|**AJAX (XMLHttpRequest / Partial Request)**|
|---|---|---|
|Request type|Full HTTP request|Full HTTP request|
|How it's sent|Browser reload or form submit|JavaScript sends it (in background)|
|Page reload?|Yes, whole page reloads|No, page stays as is|
|What updates on the page?|Entire page is replaced with server response|Only parts of the page are updated via JavaScript|
|Example|Submitting a contact form that reloads the page|Submitting a form and showing success without reload|
|Headers & body|Full HTTP headers and body|Full HTTP headers and body|
|Use case|Full page navigation|Dynamic page updates (e.g., live search, chat updates)|

To implement Ajax :
``` csharp
@model List<Employee>
@{
    ViewData["Title"] = "Index";
}

<h1>Index</h1>
<a asp-action="New" asp-controller="Employee">NEw</a>


<div id="div1" style="border:2px solid blue"></div>


<table class="table table-bordered table-hover">
    <tr>
        <th>ID</th>
        <th>Name</th>
        <th>SAlary</th>
        <th></th>
    </tr>
    @foreach (var item in Model)
    {
        <tr>
            <td>@item.Id</td>
            <td>@item.Name</td>
            <td>@item.Salary</td>
            <td>
                <a href="/Employee/Edit/@item.Id">Edit</a>
            </td>
            <td>
                <a href="/Employee/EmpCardPartial/@item.Id" onclick="GetEmpData(@item.Id)">Details</a>
            </td>
        </tr>
    }

</table>


<script src="~/lib/jquery/dist/jquery.js" ></script>
<script>
    function GetEmpData(EmpID) {
        event.preventDefault();
       
        //Ajax Call Endpont using jquery
        $.ajax({
            url: "/Employee/EmpCardPartial/" + EmpID,
            success: function (result) {
                console.log(result);
                $("#div1").html(result);
            }
        });

    }
</script>
```

1- We make a div with id="div1" to get the partial view on it 
2- to use client side we used JS and Jquery , so we uploaded `Jquery`
3- We used ` event.preventDefault();` ➔ to stop the anchor tag’s default behavior** (which is to make a full-page HTTP request to the `href` URL).
4- using Ajax Call by Jquery by  a popular ready code:
``` csharp
        $.ajax({
            url: "/Employee/EmpCardPartial/" + EmpID,
            success: function (result) {
                console.log(result);
                $("#div1").html(result);
            }
        });
```
- We put Wanted URL ,and Dom for Wanted Div 
- So We have made an ajax Call
 
- -----------------
Example using `AJAX`:
    1- I want to make 2 drop-down lists , one for `Department` and other for `Employees`
        at selected department 

1- I load all Departments at Model
2- add method for Employee to return all Department Id
3- at department I return all employee result as a `JSON` file
4- use Jquery and JS to make Partial Request

1- Employee 
``` csharp
public List<Employee> GetByDEptID(int deptID)
{
    return context.Employee.Where(e=>e.DepartmentID== deptID).ToList();
}
```

2- at Department Controller
```csharp
 public IActionResult GetEmpsByDEptId(int deptId)
 {
     List<Employee> EmpList= EmployeeREpo.GetByDEptID(deptId);
     return Json(EmpList);
 }
```

3- at Department View
``` csharp
@model List<Department>
@{
    ViewData["Title"] = "DeptEmps";
}

<h1>DeptEmps</h1>

<select id="DeptId" name="DeptID" class="form form-control" onchange="GetEmp()">
    @foreach(var deptItem in Model){
        <option value="@deptItem.Id">@deptItem.Name</option>
    }
</select>
<br />
<select id="Emps" name="Emps" class="form form-control">
</select>


<script src="~/lib/jquery/dist/jquery.min.js"></script>
<script>
    function GetEmp() {
        var deptID= document.getElementById("DeptId").value;
        var empElement = document.getElementById("Emps");
        empElement.innerHTML = "";
        console.log(deptID);
        //Ajax call json

        $.ajax({
            url: "/DEpartment/GetEmpsByDEptId?deptId=" + deptID
            , success: function (result) {
                console.log(result)
                for (let emp of result) {
                    empElement.innerHTML += "<option value='" + emp.id + "'>" + emp.name + "</option>";
                }

            }
        });
    }
</script>


```

- First We Loaded All Departments, and use `onChange()` to every time I change the selected department I load it's employees by `GetEmp()` function
``` csharp
<select id="DeptId" name="DeptID" class="form form-control" onchange="GetEmp()">
    @foreach(var deptItem in Model){
        <option value="@deptItem.Id">@deptItem.Name</option>
    }
</select>
```

 - Then We make an Empty Select To load all Employee Related To Selected Department 
``` csharp
<select id="Emps" name="Emps" class="form form-control"></select>
```

- We use Jquery
``` csharp
<script src="~/lib/jquery/dist/jquery.min.js"></script>
<script>
    function GetEmp() {
        var deptID= document.getElementById("DeptId").value;
        var empElement = document.getElementById("Emps");
        empElement.innerHTML = "";
        console.log(deptID);
        //Ajax call json

        $.ajax({
            url: "/DEpartment/GetEmpsByDEptId?deptId=" + deptID
            , success: function (result) {
                console.log(result)
                for (let emp of result) {
                    empElement.innerHTML += "<option value='" + emp.id + "'>" + emp.name + "</option>";
                }
            }
        });
    }
</script>
```
`result` s the **data returned by the server when the AJAX request completes successfully**.

Here’s what happens step-by-step:

1. **The browser sends a request** to `/DEpartment/GetEmpsByDEptId?deptId=...`.
    
2. **Your server handles that request** and returns a response—usually JSON if you’re dealing with data.
    
3. **`result` holds that response data** (e.g., an array of employee objects like `[{ id: 1, name: 'Alice' }, ...]`).
4. I Add The response values to the empty select 
---
- ## Routing 
   - What is Routing :
   **routing** refers to the system that **maps incoming HTTP requests to specific code**—typically controllers, actions, Razor pages, or endpoints.

###     What it means:

- When a user visits a URL like `/products/details/5`, routing figures out **which controller and action** (or page) should handle that request.
    
- The routing system **parses the URL** and **binds parameters** (like the `5` in the example) to your method’s parameters.


Routing is doing by 2 types :

- 1- **convention-based routing** :You can use **convention-based routing** (via route    templates like `{controller}/{action}/{id?}`) 
- 2- **attribute routing** (via `[Route()]` attributes).

---
## 1- ***Convention-based routing***
  - We need to know about URL 
    1- `/` ➔ called a delimiter
    2- /.../   or  /emp/ ➔ called Segmrnt
  - so , **Convention-based routing** means:

- You define a **general pattern** (a _convention_) that tells ASP.NET **how to map URLs to controller actions**.
    
- URLs that follow this **pattern** automatically map to your code **without needing special route definitions for each one.** 
- pattern 
``` csharp
pattern: "{controller}/{action}/{id?}"
```
- `{controller}` → the **name of the controller** (minus `Controller`).
    
- `{action}` → the **method (action)** inside that controller.
    
- `{id?}` → an **optional parameter** (like a record ID).

- the **pattern** can include:

   1️⃣ **Placeholders**  
   2️⃣ **Literals**

---

## 1️⃣ **Placeholders (Route Parameters)**

These are **variable parts** of the URL, written inside `{ }`.

➡️ Examples:

- `{controller}`
    
- `{action}`
    
- `{id}`
    

 **What they do:**

- **Capture part of the URL** and map it to a value.
    
- Are **dynamic**—they change based on what’s in the URL.
    

---

**Example pattern:**

``` csharp
`"{controller}/{action}/{id?}"`
```
**URL:** `/Products/Details/5`

|Part of the URL|Placeholder it matches|
|---|---|
|`Products`|`{controller}`|
|`Details`|`{action}`|
|`5`|`{id}`|

---

## 2️⃣ **Literals (Fixed Parts)**

These are **hardcoded parts** of the pattern. They are **static** and must **exactly match** part of the URL.

➡️ Examples:

- `shop`
    
- `products`
    
- `api`
    

 **What they do:**

- Must **be present in the URL** exactly as written.
    
- Are **not dynamic**—if the literal doesn't match, the route won’t match.
---
To enable routing, you need to call:

``` csharp
app.UseRouting();
```

This **adds the routing middleware** to the pipeline. It tells ASP.NET Core:

> “We are going to process incoming URLs and match them to routes.”

Without `UseRouting()`, **no routing will work.**

- #### then We can customize our Routing 
- Example :
At Program .cs
``` csharp
app.MapControllerRoute("Route2", "R2",
              new { controller = "Route", action = "Method2" }
   );
```

At Route Controller
``` csharp
   public IActionResult Method2()
   {
       return Content("M2");
   }
```


The **first parameter** of `MapControllerRoute()` is a **name for the route**. In this case, `"Route2"` is simply a **name** for the route being defined , <span style="color:gold"> it must be unique.</span>
### Here's how it breaks down:

1. **`"Route2"`** – This is the **name** of the route. It’s an identifier that you can use to reference or manage this route later, but it **doesn’t affect the URL matching** directly. It's mainly for **internal reference**.
    
2. **`"R2"`** – This is the **URL pattern** that will be used to match incoming requests. So, any request to `/R2` will be mapped to this route.
    
3. **`new { controller = "Route", action = "Method2" }`** – This part defines the **controller** and **action** that will be invoked when the route is matched:
    
    - **Controller:** `RouteController`
        
    - **Action:** `Method2`
        
4. We can Directly Write for the URL `/R2/` then Will go to Wanted Method 
### **How Does It Work?**

- When someone visits `/R2` in the browser, this route is triggered.

- The request will then be directed to the **`RouteController`** and will invoke the **`Method2`** action inside that controller.


- if the method receives an attribute like :
``` csharp
   public IActionResult Method2(string name)
   {
       return Content("M2");
   }
```
we use :
``` lua
/R2?name=ali
```
- attribute must be same name as called in url 

- We Can Add constrains like :

| **Constraint** | **Description**                                                         | **Example**                         |
| -------------- | ----------------------------------------------------------------------- | ----------------------------------- |
| alpha          | Matches uppercase or lowercase Latin alphabet characters (a-z, A-Z)     | `{x:alpha}`                         |
| bool           | Matches a Boolean value.                                                | `{x:bool}`                          |
| datetime       | Matches a **DateTime** value.                                           | `{x:datetime}`                      |
| decimal        | Matches a decimal value.                                                | `{x:decimal}`                       |
| double         | Matches a 64-bit floating-point value.                                  | `{x:double}`                        |
| float          | Matches a 32-bit floating-point value.                                  | `{x:float}`                         |
| guid           | Matches a GUID value.                                                   | `{x:guid}`                          |
| int            | Matches a 32-bit integer value.                                         | `{x:int}`                           |
| length         | Matches a string with the specified length or within a specified range. | `{x:length(6)}`, `{x:length(1,20)}` |
| long           | Matches a 64-bit integer value.                                         | `{x:long}`                          |
| max            | Matches an integer with a maximum value.                                | `{x:max(10)}`                       |
| maxlength      | Matches a string with a maximum length.                                 | `{x:maxlength(10)}`                 |
| min            | Matches an integer with a minimum value.                                | `{x:min(10)}`                       |
| minlength      | Matches a string with a minimum length.                                 | `{x:minlength(10)}`                 |
| range          | Matches an integer within a range of values.                            | `{x:range(10,50)}`                  |
| regex          | Matches a regular expression.                                           | `{x:regex(^\d{3}-\d{3}-\d{4}$)}`    |
- like :
``` csharp
app.MapControllerRoute("Route2", "R2/{name}/{age:int}",
              new { controller = "Route", action = "Method2" }
   );
```

- makes third segment name must be same for for method attribute 
    like :
``` csharp
 public IActionResult Method2(string name)
 {
     return Content("M2");
 }
```

``` csharp
app.MapControllerRoute("Route2", "R2/{name}",
              new { controller = "Route", action = "Method2" }
   );
```
- Will pass attribute value by `/name`

- We can also make segment is optional like 
``` csharp
app.MapControllerRoute("Route2", "R2/{age:int}/{name?}",
              new { controller = "Route", action = "Method2" }
   );
```

- we should make optional segment the last segment 
- we can make default value for the segment by :
``` csharp
app.MapControllerRoute("Route2", "R2/{name=ali}",
              new { controller = "Route", action = "Method2" }
   );
```

- ## ***Note :***
- Most Customized Route must be first then we put default Route
- 
``` csharp
app.MapControllerRoute("Route2", "R2/{controller}/{action}");
```
- Here We can use Controller , Action For URL , it is the default for Microsoft so , We can put it at last 
---

**the best practice in software architecture (especially in C# and ASP.NET environments)** is to follow this flow:
###  **Model → Repository → Controller**

This layered approach promotes separation of concerns, maintainability, and testability. Here's what each layer is responsible for:

Note: 
  A **Repository** is a class (or interface + class) that:

- Encapsulates the logic required to **access data sources** (e.g., SQL Server, MongoDB, APIs).
    
- Provides **CRUD (Create , Read , Update , Delete ) operations** for a specific model.
    
- Hides all the low-level database details from the rest of the app

this approach is called <span style ="color:gold">Repository Pattern</span> we try not to write code at controller

So : 
 **Controller** interacts with the **Repository**, and the **Repository** interacts with the **Database Context** (often referred to as the **DbContext** in Entity Framework for C#).
#  SOLID Principles in C# 
##  1. Single Responsibility Principle (SRP)

###  What It Means:

> A class should have only one reason to change.  

Each class should focus on a single task/responsibility, which improves **maintainability**, **testability**, and **readability**.

---
### ❌ Violates SRP

```csharp

public class UserManager
{

    public void AddUser(string username)
    {

        // Add user logic

    }

    public void SendNotification(string message)
    {

        // Send notification logic

    }
}

```

### ✅ Applies SRP

```csharp

public class UserManager
{

    private readonly IUserRepository _userRepository;

    public UserManager(IUserRepository userRepository)
    {

        _userRepository = userRepository;

    }

  

    public void AddUser(string username)
    {

        // Add user logic

    }

}

  

public class NotificationService
{

    public void SendNotification(string message)

    {

        // Send notification logic

    }

}

```

  
> 🔧 Now, each class has **one responsibility** only:  

`UserManager` manages users, `NotificationService` sends notifications.

---
##  2. Open/Closed Principle (OCP)

- ### What It Means:

> A class should be **open for extension**, but **closed for modification**.  

We should be able to add new functionality without changing existing code.

  

---
### ❌ Violates OCP

```csharp

public class PaymentProcessor
{

    public void ProcessPayment(string type)
    {

        if (type == "CreditCard")
        {

            // CreditCard logic

        }

        else if (type == "PayPal")
        {

            // PayPal logic

        }

    }

}

```

###  Applies OCP

```csharp

public interface IPaymentMethod
{

    void ProcessPayment(decimal amount);

}

  
public class CreditCardPayment : IPaymentMethod
{

    public void ProcessPayment(decimal amount)

    {

        // CreditCard logic

    }

}


public class PayPalPayment : IPaymentMethod
{
    public void ProcessPayment(decimal amount)

    {

        // PayPal logic

    }
}
  

public class PaymentProcessor
{

    public void ProcessPayment(IPaymentMethod paymentMethod, decimal amount)
    // pass credit or paypal obj  
    {

        paymentMethod.ProcessPayment(amount);

    }

}

```

  

> 🔧 Add new payment types **without modifying** `PaymentProcessor`.

  

---
##  3. Liskov Substitution Principle (LSP)
###  What It Means:

> Subclasses must be replaceable for their base classes **without breaking functionality**.  

Ensure subclasses behave correctly when used in place of their parent class.

---
###  Violates LSP

```csharp

public class Bird
{

    public virtual void Fly()

    {

        // Flying logic

    }

}

public class Ostrich : Bird
{

    public override void Fly()

    {

        throw new NotImplementedException(); // Ostriches can't fly

    }

}

```
##  Applies LSP

```csharp

public abstract class Bird
{

    public abstract void Move();

}

public class Sparrow : Bird
{

    public override void Move()
    {

        // Flying logic

    }

}


public class Ostrich : Bird
{

    public override void Move()
    {

        // Running logic

    }
}
```

  

> 🔧 All subclasses can now be used **interchangeably** via `Move()` without causing errors.

  

---
##  4. Interface Segregation Principle (ISP)
###  What It Means:

> Clients should not be forced to depend on interfaces they **don’t use**.  

Split large interfaces into **smaller, role-specific ones**.

---
### ❌ Violates ISP

```csharp

public interface IWorker
{

    void Work();

    void Eat(); // Not needed for Robot

}

  

public class Robot : IWorker
{

    public void Work() { /* Work logic */ }

    public void Eat()
    {

        throw new NotImplementedException(); // Robots don't eat

    }

}
```
### ✅ Applies ISP

```csharp

public interface IWorkable
{

    void Work();

}

  

public interface IFeedable
{

    void Eat();

}

  

public class Robot : IWorkable
{

    public void Work() { /* Work logic */ }

}

  

public class Human : IWorkable, IFeedable
{

    public void Work() { /* Work logic */ }

    public void Eat() { /* Eat logic */ }

}

```

  

> 🔧 Each class **implements only what it needs**, reducing bloated interfaces.

---
##  5. Dependency Inversion Principle (DIP)
###  What It Means:

> High-level modules should not depend on low-level modules.  

> Both should depend on **abstractions**.
---
### ❌ Violates DIP

```csharp

public class PaymentProcessor
{
    private readonly CreditCardPayment _payment = new CreditCardPayment();

    public void ProcessPayment(decimal amount)
    {
        _payment.Process(amount);
    }

}

public class CreditCardPayment
{
    public void Process(decimal amount)
    {
        // Payment logic
    }
}

```
###  Applies DIP

```csharp

public interface IPaymentMethod
{

    void Process(decimal amount);

}

public class CreditCardPayment : IPaymentMethod
{
    public void Process(decimal amount)
    {

        // Payment logic
    }
}

public class PaymentProcessor
{
    private readonly IPaymentMethod _paymentMethod;

    public PaymentProcessor(IPaymentMethod paymentMethod)
    {
        _paymentMethod = paymentMethod;
    }
    public void ProcessPayment(decimal amount)

    {
        _paymentMethod.Process(amount);
    }
}

```

> 🔧 The `PaymentProcessor` depends on **abstraction**, not on concrete classes. Easier to switch implementations (e.g., to PayPal or Crypto).

<a href="https://www.linkedin.com/posts/ahmed-elghrabawy_what-is-dependency-injection-in-net-activity-7278411902983000065-2CCp?utm_source=share&utm_medium=member_android&rcm=ACoAAD7r_gIBuWxSSJqAEeiHbd7WSxStnQ72ivk">Strongly Recommended To Read About Dependency Injection</a>

---
##  Final Summary

| Principle | Full Name             | Key Idea                                                |
| --------- | --------------------- | ------------------------------------------------------- |
| **S**     | Single Responsibility | One job per class                                       |
| **O**     | Open/Closed           | Extend behavior without modifying code                  |
| **L**     | Liskov Substitution   | Subclasses should fully work in place of parents        |
| **I**     | Interface Segregation | Avoid forcing classes to implement what they don’t need |
| **D**     | Dependency Inversion  | Rely on abstractions, not concrete implementations      |

- Suppose we have two models to implement: Employee and Department.  
We have three folders:

1. Repository (contains interfaces and repository classes),
    
2. Model (contains the actual models),
    
3. Controllers (contains the controllers for the models).  
    Remember, we try not to write model logic inside the controllers.


  example on Employee Method :
  1- Employee Model
``` csharp
public class Employee
{
    public int Id { get; set; }

    //        [Required]
    [MinLength(2,ErrorMessage ="Name Must be greater than 2 char")]
    [MaxLength(25)]
    [Unique]
    public string Name { get; set; }

    //[Range(6000,25000,ErrorMessage ="Salary mustbe range 6000 to 25000")]
    [Remote("CheckSalary","Employee"
        ,AdditionalFields = "JobTitle"
        , ErrorMessage ="Salary greater than 6000 L.E")]
    public int Salary { get; set; }

    public string JobTitle { get; set; }

    [RegularExpression(@"\w+\.(jpg|png)",ErrorMessage ="Image must Be jpg or png")]
    public string ImageURL { get; set; }


    public string? Address { get; set; }
   
    [ForeignKey("Department")]
    [Display(Name="Department")]
    public int DepartmentID { get; set; }

    public Department? Department { get; set; }
}
```

2- Employee Repository:
- Employee Interface 
``` csharp
 public interface IEmployeeRepository
 {
     public void Add(Employee obj);

     public void Update(Employee obj);

     public void Delete(int id);

     public List<Employee> GetAll();
     public Employee GetById(int id);

     public void Save();
   
 }
```

- Employee Repository
 ``` csharp
public class EmployeeRepository:IEmployeeRepository
{
    ITIContext context;
    public EmployeeRepository()
    {
        context = new ITIContext();
    }
    //CRUD
    public void Add(Employee obj)
    {
        context.Add(obj);
    }
    public void Update(Employee obj)
    {
        context.Update(obj);

    }

    public void Delete(int id)
    {
        Employee Emp = GetById(id);
        context.Remove(Emp);
    }
    public List<Employee> GetAll()
    {
        return context.Employee.ToList();
    }
    public Employee GetById(int id)
    {
        return context.Employee.FirstOrDefault(e => e.Id == id);
    }
    public void Save()
    {
        context.SaveChanges();
    }
}
 ```

3- Employee Controller (inject By Constructor): 
``` csharp
 public class EmployeeController : Controller
 {
     //  ITIContext context = new ITIContext();
     IEmployeeRepository EmployeeRepository;
     public EmployeeController(IEmployeeRepository EmpRepo)
     {
         EmployeeRepository = EmpRepo;
     }
 }

   // Actions to implement 

```

- After this implementation we have a problem that the Controller Factory can't create an instance of EmployeeRepository to pass it as a parameter to the controller constructor because :
 
     - The `EmployeeController` needs an `IEmployeeRepository` passed **into** its constructor.
       
    - But ASP.NET MVC **by default** does not know _how to create_ an `IEmployeeRepository`.
    
    - So the **Controller Factory** (the part that creates controllers) **throws an error** because it can't build `EmployeeController` automatically
    
--- 

- To Fix the exception we implement `IoC` Container (**`Inversion of Control`**). 
   : Instead of a class creating its own dependencies, someone else (usually a framework or container) injects those dependencies into the class or creating the needed objects?

- in .net IoC Called Service Provider , how IoC containers work : 
    - We usually deal with **three key operations**:

| Term         | Meaning (Simple)                                                                                                 | Example                                                                          |
| ------------ | ---------------------------------------------------------------------------------------------------------------- | -------------------------------------------------------------------------------- |
| **Register** | Tell the IoC container _which classes_ to create when asked for an interface.                                    | `builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();`         |
| **Resolve**  | Ask the IoC container _to give you an instance_ of a registered service. (Happens automatically in Controllers.) | Controller constructor parameter: `EmployeeController(IEmployeeRepository repo)` |
| **Dispose**  | Free or clean up objects _after they finish_. This happens automatically if you use DI properly.                 | The container disposes your repository instance after the request ends.          |
                                                                                                                                                                                               

- ##  quick summary:

| Step         | Action                                                         |
| ------------ | -------------------------------------------------------------- |
| **Register** | Add service to the container.                                  |
| **Resolve**  | Get an instance when needed (e.g., in controller constructor). |
| **Dispose**  | Automatically clean up the service after use.                  |

---
<H3 style="  text-align: center">Services Type</H3> 
-  There are basically two types of services in ASP.NET Core

| Type                   | Declared         | Registered                  | Example                                                      |
| ---------------------- | ---------------- | --------------------------- | ------------------------------------------------------------ |
| **Framework Services** | Already declared | Already registered          | `ILogger<T>`, `IHttpContextAccessor`                         |
| **Built-in Services**  | Already declared | Must be registered manually | `AddDbContext<>`, `AddAuthentication()`                      |
| Custom Services        | Not declared     | Not Registered              | `AddTransient<>()` <br>`AddScoped<>()`<br>`AddSingleton<>()` |


- ## 1- Register

- it's Called by the host before the Configure method to configure the app's services

``` csharp
   public static void Main(string[] args)
   {
       var builder = WebApplication.CreateBuilder(args);
       //Framwork service :already decalre ,alraedy register
       //built in service :already delcare ,need to register
       // Add services to the container.
       builder.Services.AddControllersWithViews();
       //Custom Servce "RegisterB
       builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); 
       
       var app = builder.Build();

       app.Run();
   }
     
```

``` csharp
 builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); 
```
You're telling the ASP.NET Core **Dependency Injection (DI) container** that when a class (like a controller) **requests** an instance of `IEmployeeRepository`, the container should create (resolve) an instance of `EmployeeRepository`.

- ###  Life-Time of Custom Service :

| Lifetime                            | Behavior                                                                  |
| ----------------------------------- | ------------------------------------------------------------------------- |
| `builder.Services.AddTransient<>()` |  New object is created **every time** it’s requested/injected.            |
| `builder.Services.AddScoped<>()`    | One object **per HTTP request** (or per created scope).                   |
| `builder.Services.AddSingleton<>()` | One object for the **whole application lifetime** (same instance always). |

---
- ## 2- Resolve (at controller)
1- inject (ask) by ***constructor*** :
``` csharp
 public EmployeeController(IEmployeeRepository EmpRepo)
 {
     EmployeeRepository = EmpRepo;
 }
```
- In this constructor, ASP.NET Core uses dependency injection to automatically provide an instance of `IEmployeeRepository` when creating the `EmployeeController`, as long as the service is properly registered in the DI container.

2- inject by ***Action***

``` csharp
public IActionResult Details([FromServices] IEmployeeRepository employeeRepository)
    {
        var employee = employeeRepository.GetById(id);
        return View(employee);
    }
```
- When you use **Action Injection** in a controller, the **[FromServices]** attribute tells the Dependency Injection (DI) system to **inject the service directly into the action parameter**, ignoring any model binding that might otherwise be used.
- examples :
    <img src="img/from.png" alt="Screenshot" width="500"/>

 
| Attribute               | Where the value comes from                  | Example                                            |
| ----------------------- | ------------------------------------------- | -------------------------------------------------- |
| **[FromBody]**          | Request body (usually JSON) (API)           | `POST` data (like a JSON object)                   |
| **[FromForm]**          | Form fields (like HTML forms)               | `<form method="post">`                             |
| **[FromHeader]**        | HTTP request headers                        | e.g., `Authorization` header                       |
| **[FromKeyedServices]** | From DI container **by key**                | Special keyed services in DI (advanced use)        |
| **[FromQuery]**         | URL query string parameters                 | `/api/items?id=5`                                  |
| **[FromRoute]**         | URL route parameters                        | `/api/items/5` (id from route)                     |
| **[FromServices]**      | From the **dependency injection container** | Inject a service like a logger or business service |

## 3 - Inject by _**View**_

### _**Note:**_

- Before we talk about injecting services into a view, we can make the `Repository` folder easier to access everywhere by creating a **global using**.
    
- To do this, add the following line (for example, in a `GlobalUsings.cs` file):
    

``` csharp 
global using MVC.Repository;
```

- This way, you don't need to manually add `using MVC.Repository;` in every file — it becomes automatically available across the whole project.

- ### How to Inject a Service in a View

 - Use the `@inject` directive at the top of your `.cshtml` view.
``` csharp
@inject IDepartmentRepository deptREpo
@{
    ViewData["Title"] = "Index";
}
<h1>Index</h1>
<h3>Id From View @deptREpo.Id</h3>
```

- ### Note: 
    `Id = Guid.NewGuid().ToString();`  it returns unique id every time You call it .
-----
- If You remember at employee repository :
``` csharp
public EmployeeRepository()
    {
        context = new ITIContext();
    }
```
- the `context = new ITIContext();` inside the constructor of `EmployeeRepository` violates the Dependency Injection (DI) principle. In a Dependency Injection pattern, dependencies (like the `ITIContext`) should be provided externally, rather than being created inside the class. This allows for easier testing, better flexibility, and separation of concerns.

- In this case, you should inject the `ITIContext` into the constructor of `EmployeeRepository` instead of creating it directly in the constructor by: 


- 1 -  We're using `appsettings.json` to store the connection string, which is a great approach to avoid hardcoding sensitive information like database credentials. The configuration in `appsettings.json` should look like this:

``` json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "cs": "Data Source=.;Initial Catalog=ITI;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"
  }
}
```

2- adding a constructor to our `ITIContext` class that accepts `DbContextOptions` is required to use the configuration provided through the `appsettings.json` file. The goal here is to pass the configuration (e.g., connection string) to the `DbContext` via the `DbContextOptions`:

``` csharp
public class ITIContext : DbContext
{
    public ITIContext(DbContextOptions<ITIContext> options) : base(options){}
    // DbSets, etc.
}

```
  - This constructor ensures that `ITIContext` can be configured using the `DbContextOptions` that will be set up via the DI container.


 3- In your `Program.cs` or `Startup.cs` (depending on your ASP.NET Core version), you're registering `ITIContext` in the DI container. The `UseSqlServer` method will use the connection string from `appsettings.json`:

``` csharp
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add DbContext to the DI container with the connection string
        builder.Services.AddDbContext<ITIContext>(options =>
        {
    options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
        });

        // Other services...

        var app = builder.Build();

        // Other middleware...
    }
}

```


4- our `EmployeeRepository` now has the `ITIContext` injected via its constructor:

``` csharp
public class EmployeeRepository
{
    private readonly ITIContext _context;
    public EmployeeRepository(ITIContext context)
    {
        _context = context;
    }
}
```
### Key Points:

- **Appsettings.json**: The connection string is pulled from the configuration, so no hardcoding is needed.
    
- **DbContext Constructor**: The `ITIContext` constructor is modified to accept `DbContextOptions`.
    
- **DI Registration**: In `Program.cs` or `Startup.cs`, `ITIContext` is registered with the DI container using `AddDbContext`.
    
- **Repository Constructor**: `EmployeeRepository` accepts `ITIContext` via constructor injection.

### ***Note*** :
- The method `builder.Services.AddDbContext<ITIContext>(options => { ... })` registers the `DbContext` (in this case `ITIContext`) in the **Dependency Injection (DI)** container. Once you register the `DbContext`, it can be injected into any service or class that requires it.

- `AddDbContext` registers the `DbContext` to be injected into classes that depend on it. In other words, you cannot directly use `ITIContext` unless the class is being injected via **Dependency Injection**.

-  in custom validation, use the **default constructor** (`public ITIContext() : base()`), and you don't use DI to inject the context, it will work properly but it violates DI , We apply it by :
``` csharp
public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var dbContext = (ITIContext)validationContext
                            .GetService(typeof(ITIContext)); // uses DI

        var email = value as string;
        if (dbContext.Users.Any(u => u.Email == email))
        {
            return new ValidationResult("Email already exists");
        }

        return ValidationResult.Success;
    }
}
```
---
##  1 - Filters 
<img src="img/filter.png" alt="Screenshot" width="1000"/>

  -  1️- **A filter is a block of code that executes before, after, or around the execution of an action method, result, or other parts of the request pipeline.**

 - 2️- **Filters are used to reduce repetitive code by handling cross-cutting concerns such as logging, caching, authorization, or error handling in a centralized way.**

 - 3️- **Filters can be applied at different scopes: on an action method, on a controller class, or globally at the application level through configuration.**

 - 4️- **At a low level, a custom filter implements specific filter interfaces such as `IActionFilter`, `IResultFilter`, `IExceptionFilter`, or their async counterparts. All these interfaces inherit from the `IFilterMetadata` marker interface, which provides metadata about the filter but is not typically implemented directly.**


***Note*** :  <span style="color:gold">Filters</span> is at <span style="color:gold"> MVC</span> Level,  Unlike  <span style="color:gold">MiddleWares </span>is that At <span style="color:gold">Application Level</span>

***Built-in Filters*** :
<img src="img/builtin.png" alt="Screenshot" width="1000"/>


##  1️-  **Authorization Filters**

**What they do:**  
Run _before everything else_ to determine if the current user/request is allowed to continue.

**Built-in example:**

- `[Authorize]` attribute.
    
- `[AllowAnonymous]` to skip authorization or Enter As anonymous
    
**Example :**

``` csharp
[Authorize] 
public class SecureController : Controller 
{     public IActionResult Secret()  
      {         return View();     
      }
}
```

---
## 2- **Action Filter**
The **action filter** is one of the most **common ways to build your own custom filter** in ASP.NET Core.

When you want to create a filter that **runs code before and/or after your action method**, you typically implement:

- `IActionFilter` (for synchronous code)
    
- or `IAsyncActionFilter` (for async code)
    

This allows you to **inject custom behavior** around the **action method execution.**

## Why would We use an Action Filter?

 To **reduce repetitive code** by handling things like:

- Logging
    
- Validation
    
- Modifying input/output
    
- Measuring execution time
    
- Adding headers, etc.
    

Instead of repeating this logic in every action, you **write it once in a filter.**

***Example :***
``` csharp
public class LogActionFilter : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine($"[Before] {context.ActionDescriptor.DisplayName}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine($"[After] {context.ActionDescriptor.DisplayName}");
    }
}

```


## **1️- `OnActionExecuting`**

### **What it does:**

- **Runs before the action method is executed.**
    
- This is where you can add custom logic **before the action runs** (e.g., validation, logging, modifying parameters, or even redirecting).
    
### **Parameters:**

- **`ActionExecutingContext` context**: This object provides information about the action being executed, such as:
    
    - The **action descriptor** (details about the action method).
        
    - The **action parameters** (input data passed to the action). as Key Value Pair 
        

### **Typical Use Cases:**

- **Validation**: Check if the data passed into the action is valid.
    
- **Logging**: Log the start of the action execution.
    
- **Authorization checks**: Ensure the user has the right permissions.
    
- **Modify input data**: Manipulate action arguments before the action runs.

## **2️ `OnActionExecuted`**

### **What it does:**

- **Runs after the action method has executed,** but **before the result is executed** (i.e., before the action's return value is processed by any result filters or sent back to the client).
    
- This is where you can perform **post-action processing** (e.g., logging the result, modifying the result, error handling).
    

### **Parameters:**

- **`ActionExecutedContext` context**: This object provides information about the executed action:
    
    - The **action descriptor** (details about the executed action).
        
    - The **action result** (the result returned by the action, such as `ViewResult`, `JsonResult`, etc.).
        

### **Typical Use Cases:**

- **Logging**: Log the completion of the action execution.
    
- **Error handling**: Handle or log any errors that occurred during the action execution.
    
- **Modifying the action result**: Change the result returned by the action before it is processed further.

***Note :***
- if Used one method of them, don't make the other throw an Exception 
---
## 3️- **Result Filters**

**What they do:**  
Run _before and after_ the **result** (e.g., a View or JSON response) is processed. Used to change the result or log view rendering.

**Built-in example:**  
No common standalone result filters, but `ResponseCache` acts partly at this level.

***Example:***
At Home Controller :
-  It controls **caching behavior** of the HTTP response by setting appropriate **cache-related headers.**

``` csharp
 [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
 public IActionResult Error()
 {
     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
 }
```

-----
## 4️- **Exception Filter**

**Job:**

- Runs when an **unhandled exception** is thrown in the action or result execution.
    
- Used to **log, transform, or handle exceptions gracefully.**
    

**Built-in idea:**  
ASP.NET Core recommends middleware for global error handling now, but in MVC filters:

- `[HandleError]` (classic MVC) or exception filters registered in Core.

***Example :***
``` csharp
public class MyExceptionFilterAttribute : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        Console.WriteLine("Exception caught: " + context.Exception.Message);
        context.Result = new ContentResult
        {
            Content = "An error occurred, but it was handled by a filter."
        };
        context.ExceptionHandled = true; // prevents the exception from propagating
    }
}
```

Another Example :
``` csharp
 public class ErrorClassAttribute : Attribute, IExceptionFilter
 {
     public void OnException(ExceptionContext context)
     {
         string msg =context.Exception.Message+ " excep";
         var viewResult = new ViewResult
         {
             ViewName = "Error",
             ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
         };

         viewResult.ViewData["msg"] = msg;
         context.Result = viewResult;

     }
 }
```
 
 - We must Inherit First from Attribute class then IExceptionFilter interface , to use it as attribute 

At Controller: 

``` csharp
[ErrorClass]
public IActionResult TestError()
{
    throw new Exception("Test exception");
}

```

**Note :*** 
` ViewName = "Error"` means using Error View that is at Shred File

To use Filter for all actions at different Controllers (For Application ):

At Program.cs
``` csharp
 builder.Services.AddControllersWithViews(option =>
 {
     option.Filters.Add(new MyExceptionFilter());
 });
```


---
###  Think of it like this:
| Step | Happens when...                 | Example                                    | Filter Type(s)                                                  |
| ---- | ------------------------------- | ------------------------------------------ | --------------------------------------------------------------- |
| 1️⃣  | Before action logic             | Logging, authorization, model manipulation | `OnActionExecuting` (in `IActionFilter` / `IAsyncActionFilter`) |
| 2️⃣  | After action method returns     | Modify returned data or check exceptions   | `OnActionExecuted` (in `IActionFilter` / `IAsyncActionFilter`)  |
| 3️⃣  | Before View rendering           | Change ViewData, swap ViewName, log info   | `OnResultExecuting` (in `IResultFilter` / `IAsyncResultFilter`) |
| 4️⃣  | After View is rendered and sent | Cleanup, log completion, post-processing   | `OnResultExecuted` (in `IResultFilter` / `IAsyncResultFilter`)  |

---
<img src="img/identity.png" alt="Screenshot" width="1000"/>

- Sometimes We need to add layer between Repository and model called Service to make mapping 

- **We Need To Know What Are Identity Classes?** 
     In **ASP.NET Core Identity**, Microsoft provides a set of **built-in classes** that help you **manage authentication and authorization** out of the box.


The **core classes** are:

| Class                  | Purpose                                                                                                                                                                                                               |
| ---------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `IdentityUser`         | Represents a **user account** (e.g., a person who logs in).                                                                                                                                                           |
| `IdentityRole`         | Represents a **role** (e.g., Admin, User, Manager).                                                                                                                                                                   |
| `IdentityDbContext`    | The Entity Framework **DbContext** that contains the tables to store users, roles, etc.                                                                                                                               |
| `UserManager<TUser>`   | A service to **create, update, delete, and manage users.**                                                                                                                                                            |
| `RoleManager<TRole>`   | A service to **create, update, delete, and manage roles.**                                                                                                                                                            |
| `SignInManager<TUser>` | Handles **<span style="color:gold">login, logout, and authentication logic</span> &&  <span style="color:gold">Stores the user's claims in the <span style="color:green">cookie </span> for future requests.</span>** |
| `UserStore<TUser>`     | Implements `IUserStore` and works with EF Core to save users in DB.                                                                                                                                                   |
| `RoleStore<TRole>`<br> | Implements `IRoleStore` and works with EF Core to save roles in DB.                                                                                                                                                   |
 then :
**UserManager**--(deals with)-->**UserStore**--(deals with)-->**IdentityDBContext**


IdentityUser has :
``` csharp
public class IdentityUser
{
    public string Id { get; set; }                      // Unique user ID (usually a GUID)
    public string UserName { get; set; }                // The user's login name
    public string NormalizedUserName { get; set; }      // Normalized version of UserName
    public string Email { get; set; }                   // The user's email address
    public string NormalizedEmail { get; set; }         // Normalized version of Email
    public bool EmailConfirmed { get; set; }            // True if email is confirmed

    public string PasswordHash { get; set; }            // Hashed password
    public string SecurityStamp { get; set; }           // Used to invalidate sessions
    public string ConcurrencyStamp { get; set; }        // For optimistic concurrency checks

    public string PhoneNumber { get; set; }             // User's phone number
    public bool PhoneNumberConfirmed { get; set; }      // True if phone number is confirmed

    public bool TwoFactorEnabled { get; set; }          // Is 2FA enabled
    public DateTimeOffset? LockoutEnd { get; set; }     // Lockout expiration date
    public bool LockoutEnabled { get; set; }            // Can the user be locked out
    public int AccessFailedCount { get; set; }          // Number of failed access attempts
}

```

- In `IdentityUser`, the **`Id`** property is **inherited directly** from the base class and is used as the **primary key** for each user in the database.

### Where is `Id` Defined?

It's defined like this:

``` csharp
public class IdentityUser<TKey> where TKey : IEquatable<TKey>
{
    public virtual TKey Id { get; set; }
    ...
}
```

- When you use the non-generic version `IdentityUser` (i.e., `IdentityUser` without `<TKey>`), it **defaults to `string`**:
``` csharp
public class IdentityUser : IdentityUser<string>
{
    public IdentityUser()
    {
    Id = Guid.NewGuid().ToString();
    SecurityStamp = Guid.NewGuid().ToString();
    }
}
```

### **Extending IdentityUser (Adding Your Own Attributes):**

If you need extra **custom fields** (e.g., FullName, DateOfBirth), you can **inherit** from `IdentityUser` and Make a New Class to follow `Open for Extend Close for Modification ` :

``` csharp
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
}

```

- Now We Have 2 Data bases :
       1 - IdentityDbContext
       2- Ower Default Data base (ITIContext) 
  To Solve it We make ITIContext Inherit From IdentityDbContext
- 
``` csharp
 public class ITIContext:IdentityDbContext<ApplicationUser>
 {
     public DbSet<Employee> Employee { get; set; }
     public DbSet<Department> Department { get; set; }
     //
     public ITIContext() : base()
     {
     }

     public ITIContext(DbContextOptions options) : base(options)
     {

     }
     // Required
     protected override void OnModelCreating(ModelBuilder modelBuilder)
     {
        base.OnModelCreating(modelBuilder); 
     }
  }

```

- We Make it Generic to Apply New Attributes  (Attribute For Generic Must inherit from identityDbContext )

- After that we add migration and add update the database 

- ***Now*** We Finished Our Classes and DataBase 
---------------
###  We will make the Controller and ViewModel for mapping , because we don't need to enter all IdentityUser properties


- ## 1- ViewModel:
``` csharp
public class RegisterUserViewModel
{
    public string UserName { get; set; }
    
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Compare("Password")]
    [Display(Name ="Confirm Password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
    
    public string Address { get; set; }
}
```

- Used For Mapping from View's Data (Request)

 - ## 2-  Controller :
``` csharp
 public class AccountController : Controller
 {
     private readonly UserManager<ApplicationUser> userManager;
     private readonly SignInManager<ApplicationUser> signInManager;

     public AccountController
         (UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
     {
         this.userManager = userManager;
         this.signInManager = signInManager;
     }

     [HttpPost]
     public async Task<IActionResult> SaveRegister
        (RegisterUserViewModel UserViewModel)
     {
         if (ModelState.IsValid)
         {
             //Mapping
             ApplicationUser appUser = new ApplicationUser();
             appUser.Address = UserViewModel.Address;
             appUser.UserName = UserViewModel.UserName;

             //save database
             IdentityResult result =
                 await userManager.CreateAsync(appUser, UserViewModel.Password);
             if (result.Succeeded)
             {
                 //Cookie
                 await signInManager.SignInAsync(appUser, false);
                 return RedirectToAction("Index");
             }
             foreach (var item in result.Errors)
             {
                 ModelState.AddModelError("", item.Description);
             }
         }
         return View("Register", UserViewModel);
     }
}
```

- Both are injected via Dependency Injection (IoC container) and are ready to use in your controllers or services.
 ``` csharp
 private readonly UserManager<ApplicationUser> userManager;
 private readonly SignInManager<ApplicationUser> signInManager;
 ```
Remember : 
UserManager Used For CRUD operations , SignInManager for adding Cookies 

``` csharp
IdentityResult result = await userManager.CreateAsync(appUser, UserViewModel.Password);
```

- `CreateAsync`:
    
    - Adds the user to the **database**.
        
    - Automatically **hashes the password** and saves it securely.
    
    - returns `IdentityResult` that is a **result object** that tells you **whether the operation succeeded or failed.**
    
    - - When you pass the password to `CreateAsync()`, **it hashes it immediately** using a secure algorithm (by default, PBKDF2 with HMAC-SHA256).
    
    - The result (the **hash**) is saved in the `PasswordHash` column in the `AspNetUsers` table.

    -   It has two key members:

| Property    | Purpose                                                                                     |
| ----------- | ------------------------------------------------------------------------------------------- |
| `Succeeded` | A **bool**: `true` if the user was created successfully; `false` if there were any errors.  |
| `Errors`    | A collection of **IdentityError** objects that explain **what went wrong** (if any errors). |

- `result` tells you if it **succeeded or failed.**
    - Here We Add the Errors to ModelErrors 
    
``` csharp
 foreach (var item in result.Errors)
   {
      ModelState.AddModelError("", item.Description);
   }     
```

***Note :*** Because of CreateAsync is Async We Add ***await*** and Convert ***Action To Task** 

- `SignInAsync`

``` csharp
await signInManager.SignInAsync(appUser, false);
```

   - Sign in the user (`appUser`) and create an authentication cookie to keep them logged in.

|Part|Meaning|
|---|---|
|`appUser`|The user object (of type `ApplicationUser`) you just created or fetched.|
|`false`|This is `isPersistent`: it tells whether the **login session (cookie) should be persistent.**(The login session will end when the browser is closed.)| 

### 3- At Program.cs
``` csharp
 builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ITIContext>();
```
-  this line **adds Identity services** with your user (`ApplicationUser`) and role (`IdentityRole`).
    
- It also **registers UserManager, SignInManager, RoleManager, etc.** for **Dependency Injection** automatically.
## **What Does It Mean?**

This code **registers ASP.NET Core Identity** in the **dependency injection (DI) container** and tells it:

1️⃣ **Which classes to use for Users and Roles.**

2️⃣ **Which database context to use for storing Identity data.**

---
##  **Breaking Down Each Part:**

|Part|Meaning|
|---|---|
|`AddIdentity<ApplicationUser, IdentityRole>()`|🔧 You're telling ASP.NET:|
||- `ApplicationUser`: your **custom user class** (inherits from `IdentityUser`).|
||- `IdentityRole`: the default class to handle **roles** (like Admin, User, etc.).|
|`.AddEntityFrameworkStores<ITIContext>()`|🔗 You’re telling Identity to **use your Entity Framework DbContext (`ITIContext`) to store users/roles.**|

-------

- Instead Of Build The View From Scratch We Have Built-in Template We Can Use  
<img src="img/razor1.png" alt="Screenshot" width="1000"/>



<img src="img/razor2" alt="Screenshot" width="1000"/>


I chose the Template and the ViewModel That will apply for it 

---
<img src="img/summary.png" alt="Screenshot" width="1000"/>

- the steps that is  listed form the core sequence to implement an authentication module using **ASP.NET Core Identity with Entity Framework Core**. However, to ensure your authentication module is complete and functional, :

    - 1. Install package Identity "Microsoft.AspNetCore.Identity.EntityFrameworkCore"

    - 2. class ApplicationUser : IdentityUser

    - 3. ITIContext : `IdentityDbContext<ApplicationUser>`
 
    - 4. Add-migration

    - 5. Create AccountController (Adding user manager , sign in manager  by DI) 

    - 6. Create RegisterViewModel
 
    - 7. Register Action

    - 8. Register IdentityService in Program class
 

---
***Note :***
- Identity For Password Has Some Strict Condition like :
  **Password Requirements:**

    - Minimum 6 characters
    
    - At least one non-alphanumeric character (e.g., !, @, #, etc.)
    
    - At least one digit (0-9)
    
    - At least one uppercase letter (A-Z)

- We Can edit these requirements by (At Program.cs):
``` csharp
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    option.Password.RequiredLength = 4;
    option.Password.RequireDigit = false;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<ITIContext>();
```

- When using  `[Authorize]` We need To use `app.UseAuthentication();` middleware at programe.cs before ` app.UseAuthorization();`

##  `app.UseAuthorization();`:

- It checks:
    
    - "Does the incoming request have any **authentication data** like:
        
        - a Cookie,
            
        - a JWT Token,
            
        - or any other authentication method?"
            
- If it **finds data** :
    
    - It **verifies** it (e.g., checks if the token or cookie is valid).
        
    - If valid ➔ it **fills `HttpContext.User`** with the user info (like username, roles, claims, etc.).
        
- If **nothing is found** or it's invalid :
    
    - The request continues with the user as **Anonymous**.
        

 So, it's responsible for **checking and loading who the user is**.

---

##  `[Authorize]`:

**What does it do?**

- After `UseAuthentication()` finishes and has loaded the user info, `[Authorize]` comes in and says:
    

> "Alright, the current user:
> 
> - Are they **authenticated**?
>     
> - Do they have the required **roles or permissions** (if specified)?"
>     

- If everything is good  it allows access.
    
- If the user is **not logged in** , returns 401 Unauthorized.
    
- If logged in but **missing required role/permission** , returns 403 Forbidden.
    

 - So, it's responsible for **authorizing access based on user info.**
  - We _usually_ need both together if you're using `[Authorize]`.

---
##  To summarize what you said (which is correct):

-  `UseAuthentication`: **checks if there’s a cookie (or token) and verifies it.**
    
-  `[Authorize]`: **checks the loaded user info to decide if access is allowed.**
----
- To Make Sign out (Delete Cookies ):

``` csharp
 public async Task<IActionResult> SignOut()
 {
     await signInManager.SignOutAsync();
     return View("Login");
 }
```

- Will Delete User Cookie For Identity
-------
-  How We Login Again :
``` csharp
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> SaveLogin(LoginUserViewModel userViewModel)
  {
      if (ModelState.IsValid==true)
      {
          //check found 
          ApplicationUser appUser=
              await userManager.FindByNameAsync(userViewModel.Name);
          if (appUser != null)
          {
             bool found=
                  await userManager.CheckPasswordAsync(appUser, userViewModel.Password);
              if(found==true)
              {
                  await signInManager.SignInAsync(appUser,userViewModel.RememberMe);
                  return RedirectToAction("Index", "Department");
              }
              
          }
          ModelState.AddModelError("", "Username OR PAssword wrong");
          //create cookie
      }
      return View("Login", userViewModel);
  }

```

``` csharp
   ApplicationUser appUser=
              await userManager.FindByNameAsync(userViewModel.Name);
```
- it returns the User of `ApplicationUser` type if Found it , if not will return null

``` csharp
bool found=  await userManager.CheckPasswordAsync(appUser, userViewModel.Password);
```
- Make Re-hash For Password and compare it 
---
- Controllers in ASP.NET Core **inherit** some properties from the `ControllerBase` or `Controller` class (depending on the type of controller). These properties are automatically available to the controller without any additional setup. Specifically:
     1- ViewData
     
     2- ViewBag
    
     3- **`User`** ,  **Type**: `ClaimsPrincipal`
      
    -   The `User` property represents the **current authenticated user**. It contains   information about the user's identity and claims (like roles, email, etc.), If No Authentication Found it Becomes ***`Null`*** 
      
    - **Inheritance**: `User` is inherited from `ControllerBase` (which is the base class for controllers).
    
      - ###  **`ClaimsPrincipal`**:
       In ASP.NET Core, **`ClaimsPrincipal`** is the class used to represent the **current authenticated user** and contains all the claims associated with that user. The claims provide key information about the user, such as their roles, email address, and any other custom attributes related to their identity.

       - **`ClaimsPrincipal`** provides:
       1- **Identity**: Basic information about the user (like username).
       2- **Claims**: A collection of ***<span style="color:gold">key-value pairs</span>*** containing the user's information, such as roles, permissions, email, etc.
    
    ***Example :***
``` csharp
 public IActionResult TestAuth()
 {
     if (User.Identity.IsAuthenticated == true)
     {
         
         Claim IDClaim= User.Claims
             .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

         Claim AddressClaim = User.Claims.FirstOrDefault(c => c.Type == "UserAddress");

         string id = IDClaim.Value;
         string address = AddressClaim?.Value ?? "Address not available";
         string name = User.Identity.Name;
         return Content($"welcome {name} \n ID= {id}");
     }
     return Content("Welcome Guest");
 }
```

- `User.Identity.IsAuthenticated == true` Used To Get The Authentication like `[Authorize]

-  ` string name = User.Identity.Name;` used to get the Name
- To Get Other Properties by :
     - We Deal With User as context , Claims as Class in it , then use the needed property 
     - Claims returns Type of `IEnumerable`
     - `NameIdentifier` is part of standard identity claims but `"UserAddress"` is A Custom Claim
   
``` csharp
    Claim IDClaim= User.Claims
             .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

         Claim AddressClaim = User.Claims.FirstOrDefault(c => c.Type == "UserAddress");
```
   
***Example***:
1- Identity :
```lua
User.Identity.IsAuthenticated;
User.Identity.Name;
User.Identity.AuthenticationType;
```


2- Claims :

|Claim Type|ClaimTypes Constant|Example Value|
|---|---|---|
|Name|`ClaimTypes.Name`|`jdoe`|
|Name Identifier (User ID)|`ClaimTypes.NameIdentifier`|`12345`|
|Email|`ClaimTypes.Email`|`jdoe@example.com`|
|Role|`ClaimTypes.Role`|`Admin`|
|Given Name|`ClaimTypes.GivenName`|`John`|
|Surname|`ClaimTypes.Surname`|`Doe`|
|Date of Birth|`ClaimTypes.DateOfBirth`|`1990-01-01`|
|Gender|`ClaimTypes.Gender`|`Male`|
|Mobile Phone|`ClaimTypes.MobilePhone`|`+1234567890`|
|Country|`ClaimTypes.Country`|`US`|
|Locality (City)|`ClaimTypes.Locality`|`New York`|
|Street Address|`ClaimTypes.StreetAddress`|`123 Main St`|
|Postal Code|`ClaimTypes.PostalCode`|`10001`|
|Website|`ClaimTypes.Webpage`|`https://example.com`|
|Authentication Method|`ClaimTypes.AuthenticationMethod`|`Password`|
|Security Stamp (if using ASP.NET Identity)|_Custom, often named "AspNet.Identity.SecurityStamp"_|(GUID)|

- ## How Can We Add Custom Claims 
1- Make Collection of Claims 
2- Add needed Claim to created collection 
3- use <span style="color:gold">SignInWithClaimsAsync</span> and add Appuser , boolen for long term cookie and collection of claims

``` csharp
 List<Claim> Claims = new List<Claim>();
 Claims.Add(new Claim("UserAddress",appUser.Address));

 await signInManager.SignInWithClaimsAsync(appUser, userViewModel.RememberMe, Claims);
```

- #### To get it :
``` csharp
var userAddress = User.FindFirst("UserAddress")?.Value;
//or
Claim AddressClaim = User.Claims.FirstOrDefault(c => c.Type == "UserAddress");

```

---
- ## How Can Add Role
- To Add Role to DataBase We Use <span style="color:gold">IdentityRole</span> so we Make an inject for it at Controller 
- then :
``` csharp
 IdentityRole role = new IdentityRole();
 role.Name = "Admin";
 IdentityResult result= await roleManager.CreateAsync(role);
 if (result.Succeeded == true)
 {
     ViewBag.sucess = true;
     return View("AddRole");
 }

  foreach (var item in result.Errors)
  {
      ModelState.AddModelError("", item.Description);
  }
  //code
```

- ## Add Role To User
- 
``` csharp 
 IdentityResult result= 
 await userManager.CreateAsync(appUser,UserViewModel.Password);
 if (result.Succeeded)
 {
     //assign to role
     await userManager.AddToRoleAsync(appUser, "Admin");
     // or
     await userManager.AddToRolesAsync(appUser, new[] { "Admin", "Manager" });
     //Cookie
     await signInManager.SignInAsync(appUser, false);
     return RedirectToAction("Index", "Department");
 }
```

- If you want to **restrict access** so that **only users in the `Admin` role** can access a controller or an action, you use:
``` csharp
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    public IActionResult Dashboard()
    {
        return View();
    }
}
```

- To check if User Has a specific Role
``` csharp
if (User.IsInRole("Admin")){}
```

**When you assign a role to a user (e.g., with `AddToRoleAsync`):**

-  A new record is created in the `AspNetUserRoles` table.
    
-  This table acts as a **join table** between:
    
    - `AspNetUsers` (your users)
        
    - `AspNetRoles` (your roles)
        

 So , it **creates a foreign key relationship** between the user and the role.

---

 **At login time:**

- The system **queries that table** to find out which roles the user belongs to (following those foreign key links).
    
- It **creates claims for each role** and **loads them into the user’s identity (ClaimsPrincipal).**
    
- Those claims are **stored in the authentication cookie** (or token).
    
- - **Roles → Claims → ClaimsPrincipal → User.**
  
- On every request:
    - The `ClaimsPrincipal` is rebuilt from the cookie/token and made available via `User` in controllers.
---
 
 **After login:**
 
- When you call:
  ``` csharp
    User.IsInRole("Admin")
    ```

- it **does NOT hit the database.**
- Instead, it **checks the user's already-loaded claims** (from the cookie/session) to see if `"Admin"` is there.

---
**Important reminder:**  
If you assign a role to a user **after they’ve already logged in,** the user won’t see the new role until they **log out and log in again,** unless you force the claims to refresh.
 
 
---