
###  **HTML Helper in ASP.NET MVC**  

##  **Before: Writing HTML with Embedded C#**  
Before HTML Helpers, we used to write **raw HTML** and insert **C# code** inside it.  

Example:  
```html
<input type="text" name="Username" value="@Model.Username" />
```

---

### ***1️- HTML Helper (Pure C#)***  
#### **What are HTML Helpers?**  
**HTML Helpers** are **utility methods** in ASP.NET MVC that generate **HTML elements dynamically** inside a View instead of writing static HTML manually.  

🔹 **It is a loosely typed helper** (does not have strong type checking).  

### **Example: Using HTML Helper**  
```csharp
@Html.TextBox("Username", Model.Username, new { @class = "form-control" })
               //name       //Value
```
-  **Generates**:  
```html
<input type="text" name="Username" value="JohnDoe" class="form-control" />
```

---
### **Advantages of HTML Helpers**
-  Less HTML code, more **C#-centric** approach.  
-  Generates **standard HTML elements** dynamically.  
-  Supports adding **CSS classes and attributes** easily.  

---
### **Common HTML Helpers**
| HTML Helper                     | Generates                              |
| ------------------------------- | -------------------------------------- |
| `@Html.TextBox("name")`         | `<input type="text" name="name">`      |
| `@Html.Password("pass")`        | `<input type="password" name="pass">`  |
| `@Html.DropDownList("country")` | `<select name="country">...</select>`  |
| `@Html.CheckBox("agree")`       | `<input type="checkbox" name="agree">` |

---

## Strongly Typed HTML Helper for Name

####  Example: Strongly Typed HTML Helper for `Name`  

### 1️⃣ Create a Model (`UserModel.cs`)
```csharp
public class UserModel
{
    [Display(Name="Full_Name")]
    public string Name { get; set; }
}
```

### 2️⃣ Create a Controller (`HomeController.cs`)
```csharp
public class HomeController : Controller
{
    public ActionResult Index()
    {
        var user = new UserModel(); // Creating an empty model
        return View(user);
    }

    [HttpPost]
    public ActionResult Index(UserModel model)
    {
        if (ModelState.IsValid)
        {
            // Process form data
            ViewBag.Message = "Submitted Name: " + model.Name;
        }
        return View(model);
    }
}
```

### 3️⃣ Create a View (`Index.cshtml`)
```csharp
@model UserModel

@using (Html.BeginForm())
{
    <div class="form-group">
        @Html.LaberFor(b=>b.name); 
    // will make the label name for the attribute in the class but 
    // if it found [Display (name="Name")] will put it to the label 
    
    @Html.TextBoxFor(m => m.Name, new { @class = "form-control", placeholder ="Enter your name" })
    </div>
     
    <button type="submit" class="btn btn-primary">Submit</button>
}

@if (ViewBag.Message != null)
{
    <p>@ViewBag.Message</p>
}
```

---
##  Explanation
- **`Html.TextBoxFor(m => m.Name)`** binds directly to the `Name` property in `UserModel`.  
- The model ensures **type safety** and allows data validation.  
- When the form is submitted, the controller receives the model with user input.  
- If valid, the name is displayed in `ViewBag.Message`.  

### `Html.EditorFor()` Examples

###  Example 1: **Without Data Annotation (Simple Text Input)**
### 1️⃣ Model (`UserModel.cs`)
```csharp
public class UserModel
{
    public string Name { get; set; }
}
```

### 2️⃣ View (`Index.cshtml`)
```csharp
@model UserModel

@using (Html.BeginForm())
{
    <div class="form-group">
        <label>Name:</label>
        @Html.EditorFor(m => m.Name, new { htmlAttributes = new { @class = "form-control", placeholder = "Enter name" } })
    </div>

    <button type="submit" class="btn btn-primary">Submit</button>
}
```

---

###  Example 2: **With Data Annotation (Password Input)**
### 1️⃣ Model (`UserModel.cs`)
```csharp
using System.ComponentModel.DataAnnotations;

public class UserModel
{
    
    public string Name { get; set; }

    [DataType(DataType.Password)]
    [Display(Password="User Password")]
    public string Password { get; set; }
}
```

### 2️⃣ View (`Index.cshtml`)
```csharp
@model UserModel

@using (Html.BeginForm())
{
    <div class="form-group">
        <label>Password:</label>
        @Html.EditorFor(m => m.Password, new { htmlAttributes = new { @class = "form-control", placeholder = "Enter password" } })
    </div>

    <button type="submit" class="btn btn-primary">Submit</button>
}
```
 I choose the data type because EditFor detect numeric a string

--------------

### ✅ Key Differences:
| Example        | Uses Data Annotation?                   | Input Type                           |
| -------------- | --------------------------------------- | ------------------------------------ |
| Name Input     | ❌ No                                    | Text (`<input type="text">`)         |
| Password Input | ✅ Yes (`[DataType(DataType.Password)]`) | Password (`<input type="password">`) |

 **`EditorFor()` automatically detects the input type based on the model's data annotation!** 🚀


##                                     ***V2 - Tag Helper***
####  Full Tag Helper Example in ASP.NET Core

### ✅ Updated Razor View (`Index.cshtml`)  
```html
@model UserModel

<form method="post" asp-action="" asp-controller="" asp-route-id="">

    <div class="form-group">
        <label asp-for="Name"></label>
        <input asp-for="Name" class="form-control" placeholder="Enter your name" />
        <span asp-validation-for="Name" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Password"></label>
        <input asp-for="Password" class="form-control" placeholder="Enter your password" />
        <span asp-validation-for="Password" class="text-danger"></span>
    </div>

    <button type="submit" class="btn btn-primary">Submit</button>
</form>

@if (!string.IsNullOrEmpty(ViewBag.Message))
{
    <p>@ViewBag.Message</p>
}

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

Another Example with Selected List

``` cs
@model EmpWithDEptListViewModel
@{
    ViewData["Title"] = "Edit";
    SelectList selectLists = new SelectList(Model.DeptList,"Field_Value","Field_Name"); /* to make casting      to IEnumerable */
}

<h1>Edit</h1>
<form method="post" asp-action="SAveEdit>
    <div>
      <input asp-for="StId" type="hidden" />
        <label asp-for="DepartmentID"></label>
        @* <input type="number" value="@Model.DepartmentID" id="DepartmentID" name="DepartmentID" class="form form-control" /> *@
        <select asp-for="DepartmentID" asp-items="selectLists" ></select>
    </div>
    <input type="submit" value="Save" class="btn btn-success" />
</form>

```
###### By default, when using `<form asp-action="SomeAction">`, the method is **POST**, unless specified otherwise.
---

###  Changes & Enhancements:
| Feature                                            | Usage                                                |
| -------------------------------------------------- | ---------------------------------------------------- |
| **`<form>` (No `asp-controller` or `asp-action`)** | Since it's in the same view, method="post" is enough |
| **`asp-for` on `<input>` & `<label>`**             | Automatically binds fields to the model              |
| **`asp-validation-for`**                           | Displays validation messages dynamically             |
| **`@section Scripts`**                             | Ensures client-side validation works                 |

 **Tag Helpers make Razor views more HTML-friendly, reducing the need for `HtmlHelpers`.**  
<U>When using <b>Tag Helpers</b> for a form's submit button, you  explicitly set the `type="submit"` attribute to ensure it functions correctly.</U>

## Note :

| `asp-for="X"` does... | Result in HTML                            |
| --------------------- | ----------------------------------------- |
| Sets `name`           | `name="X"`                                |
| Sets `id`             | `id="X"`                                  |
| Sets `value`          | `value="Model.X"`                         |
| Works with validation | `<span asp-validation-for="X">...</span>` |

-----------
# [ValidateAntiForgeryToken] in ASP.NET MVC & Core

##  What is `[ValidateAntiForgeryToken]`?
`[ValidateAntiForgeryToken]` is an attribute used in ASP.NET MVC & ASP.NET Core to **prevent Cross-Site Request Forgery (CSRF) attacks** by ensuring that form submissions are legitimate and come from the same site.

---

### Why is it Important?
Without CSRF protection, an attacker could trick a logged-in user into unknowingly submitting a form that performs malicious actions, such as:
- Transferring money
- Changing an account password
- Deleting a user account

This attribute ensures that **every POST request contains a valid anti-forgery token**, making it harder for attackers to forge requests ,used For <span style="color:green">non-Get Request</span> 


---

## ✅ How to Use `[ValidateAntiForgeryToken]`
### **1️⃣ Secure Controller Action**
In your **controller**, apply the `[ValidateAntiForgeryToken]` attribute to the POST action to enforce CSRF protection.

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult SubmitForm(UserModel model)
{
    if (ModelState.IsValid)
    {
        // Process form submission
        return RedirectToAction("Success");
    }
    return View(model);
}
```
### **2️⃣ Secure Razor View (Form)**

To ensure the token is included in the form, you must use either **Tag Helpers (ASP.NET Core)** or **HtmlHelpers (ASP.NET MVC & Core).**

## **🔹 Using Tag Helpers (ASP.NET Core)**

✔ **ASP.NET Core automatically adds the CSRF token** For <u>Tag Helper</u>, so you don’t need to manually include `@Html.AntiForgeryToken()`.

``` HTML
<form asp-action="SubmitForm" method="post"> 
<input asp-for="Email" class="form-control" /> `
<button type="submit">Submit</button> </form>`
```

✔ The hidden input **is automatically generated**:

`<input name="__RequestVerificationToken" type="hidden" value="random-token-value">`

---

## **🔹 Using HtmlHelpers (ASP.NET MVC & Core)**

✔ If you're using `Html.BeginForm()`, you **must manually add** 
``` csharp
@Html.AntiForgeryToken().
@using (Html.BeginForm("SubmitForm", "Home", FormMethod.Post))
{
    @Html.AntiForgeryToken();   
    <label>  Email:</label >
    @Html.TextBoxFor(model => model.Email, new { @class = "form-control" }) < button type = "submit" > Submit </ button >
}

```

✔ Without `@Html.AntiForgeryToken()`, the request will fail with **HTTP 403 Forbidden**.

---

## - How Does It Work?

1. **When the form is generated**, `@Html.AntiForgeryToken()` inserts a hidden input field with a CSRF token :    
2. **When the form is submitted**, the browser **sends this token** along with the request.
3. **ASP.NET checks the token** in the `[ValidateAntiForgeryToken]` attribute:
    - ✅ If the token is valid → the request is processed.
    - ❌ If the token is missing or incorrect → the request is **rejected with HTTP 403 Forbidden**.

---

##  When Do You Need It?

|Scenario|Should You Use `[ValidateAntiForgeryToken]`?|
|---|---|
|**POST request (modifies data)**|✅ Yes|
|**GET request (just displays data)**|❌ No|
|**Login, Register, Delete, Update actions**|✅ Yes|
|**API request (without authentication)**|❌ No (Use JWT or OAuth)|

---

- ## Summary

| Feature        | `[ValidateAntiForgeryToken]`                                   |
| -------------- | -------------------------------------------------------------- |
| **Prevents**   | CSRF (Cross-Site Request Forgery) attacks                      |
| **Used in**    | ASP.NET MVC & ASP.NET Core                                     |
| **Applies to** | **Only POST requests**                                         |
| **Requires**   | `@Html.AntiForgeryToken()` in forms (unless using Tag Helpers) |
| **Without It** | Hackers can submit unauthorized requests                       |

---

##  Example Code

### **🔹 Controller**

``` csharp
 public class AccountController : Controller
 {
     [HttpPost]
     [ValidateAntiForgeryToken]
     public IActionResult Login(LoginModel model)
     {
         if (ModelState.IsValid)
         {      // Authentication logic here
             return RedirectToAction("Dashboard");
         }
         return View(model);
     }
 }
```
### **View (Using Tag Helpers)**

``` csharp
<form asp-action="Login" method="post">  
<input asp-for="Username" class="form-control" /> 
<input asp-for="Password" type="password" class="form-control" /> 
<button type="submit">Login</button> </form>
```
### **View (Using HtmlHelpers)**

```csharp
@using (Html.BeginForm("Login", "Account", FormMethod.Post))
{
    @Html.AntiForgeryToken() ;
    @Html.LabelFor(m => m.Username) ;
    @Html.TextBoxFor(m => m.Username, new { @class = "form-control" }) ;
    @Html.LabelFor(m => m.Password)  ;
    @Html.PasswordFor(m => m.Password, new { @class = "form-control" });
    <button type="submit">Login</button>
}
```
---

##  Final Notes

-  **Tag Helpers in ASP.NET Core** automatically include the anti-forgery token.
-  **HtmlHelpers in ASP.NET MVC/Core require `@Html.AntiForgeryToken()` manually.**
    If you forget to add the anti-forgery token, **the request will be rejected (403 Forbidden).**

| Condition                              | `[ValidateAntiForgeryToken]` | `ModelState.IsValid` | Result                                  |
| -------------------------------------- | ---------------------------- | -------------------- | --------------------------------------- |
| **No token / invalid token**           | ❌ Fails (403 Forbidden)      | ❌ Not checked        | Request is blocked                      |
| **Valid token, but invalid form data** | ✅ Passes                     | ❌ Fails              | Returns the form with validation errors |
| **Valid token and valid form data**    | ✅ Passes                     | ✅ Passes             | Redirects to "Success"                  |

#### Note :
- We Will Know more about Validation-Token at JWT
## **Layout & RenderBody in ASP.NET Core MVC**  

# [ValidateAntiForgeryToken] in ASP.NET MVC & Core

##  What is `[ValidateAntiForgeryToken]`?
`[ValidateAntiForgeryToken]` is an attribute used in ASP.NET MVC & ASP.NET Core to **prevent Cross-Site Request Forgery (CSRF) attacks** by ensuring that form submissions are legitimate and come from the same site.

---

### Why is it Important?
Without CSRF protection, an attacker could trick a logged-in user into unknowingly submitting a form that performs malicious actions, such as:
- Transferring money
- Changing an account password
- Deleting a user account

This attribute ensures that **every POST request contains a valid anti-forgery token**, making it harder for attackers to forge requests ,used For <span style="color:green">non-Get Request</span> 


---

## ✅ How to Use `[ValidateAntiForgeryToken]`
### **1️⃣ Secure Controller Action**
In your **controller**, apply the `[ValidateAntiForgeryToken]` attribute to the POST action to enforce CSRF protection.

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult SubmitForm(UserModel model)
{
    if (ModelState.IsValid)
    {
        // Process form submission
        return RedirectToAction("Success");
    }
    return View(model);
}
```
### **2️⃣ Secure Razor View (Form)**

To ensure the token is included in the form, you must use either **Tag Helpers (ASP.NET Core)** or **HtmlHelpers (ASP.NET MVC & Core).**

## **🔹 Using Tag Helpers (ASP.NET Core)**

✔ **ASP.NET Core automatically adds the CSRF token** For <u>Tag Helper</u>, so you don’t need to manually include `@Html.AntiForgeryToken()`.

``` HTML
<form asp-action="SubmitForm" method="post"> 
<input asp-for="Email" class="form-control" /> `
<button type="submit">Submit</button> </form>`
```

✔ The hidden input **is automatically generated**:

`<input name="__RequestVerificationToken" type="hidden" value="random-token-value">`

---

## **🔹 Using HtmlHelpers (ASP.NET MVC & Core)**

✔ If you're using `Html.BeginForm()`, you **must manually add** 
``` csharp
@Html.AntiForgeryToken().
@using (Html.BeginForm("SubmitForm", "Home", FormMethod.Post))
{
    @Html.AntiForgeryToken();   
    <label>  Email:</label >
    @Html.TextBoxFor(model => model.Email, new { @class = "form-control" }) < button type = "submit" > Submit </ button >
}

```

✔ Without `@Html.AntiForgeryToken()`, the request will fail with **HTTP 403 Forbidden**.

---

## - How Does It Work?

1. **When the form is generated**, `@Html.AntiForgeryToken()` inserts a hidden input field with a CSRF token :    
2. **When the form is submitted**, the browser **sends this token** along with the request.
3. **ASP.NET checks the token** in the `[ValidateAntiForgeryToken]` attribute:
    - ✅ If the token is valid → the request is processed.
    - ❌ If the token is missing or incorrect → the request is **rejected with HTTP 403 Forbidden**.

---

##  When Do You Need It?

|Scenario|Should You Use `[ValidateAntiForgeryToken]`?|
|---|---|
|**POST request (modifies data)**|✅ Yes|
|**GET request (just displays data)**|❌ No|
|**Login, Register, Delete, Update actions**|✅ Yes|
|**API request (without authentication)**|❌ No (Use JWT or OAuth)|

---

- ## Summary

| Feature        | `[ValidateAntiForgeryToken]`                                   |
| -------------- | -------------------------------------------------------------- |
| **Prevents**   | CSRF (Cross-Site Request Forgery) attacks                      |
| **Used in**    | ASP.NET MVC & ASP.NET Core                                     |
| **Applies to** | **Only POST requests**                                         |
| **Requires**   | `@Html.AntiForgeryToken()` in forms (unless using Tag Helpers) |
| **Without It** | Hackers can submit unauthorized requests                       |

---

##  Example Code

### **🔹 Controller**

``` csharp
 public class AccountController : Controller
 {
     [HttpPost]
     [ValidateAntiForgeryToken]
     public IActionResult Login(LoginModel model)
     {
         if (ModelState.IsValid)
         {      // Authentication logic here
             return RedirectToAction("Dashboard");
         }
         return View(model);
     }
 }
```
### **View (Using Tag Helpers)**

``` csharp
<form asp-action="Login" method="post">  
<input asp-for="Username" class="form-control" /> 
<input asp-for="Password" type="password" class="form-control" /> 
<button type="submit">Login</button> </form>
```
### **View (Using HtmlHelpers)**

```csharp
@using (Html.BeginForm("Login", "Account", FormMethod.Post))
{
    @Html.AntiForgeryToken() ;
    @Html.LabelFor(m => m.Username) ;
    @Html.TextBoxFor(m => m.Username, new { @class = "form-control" }) ;
    @Html.LabelFor(m => m.Password)  ;
    @Html.PasswordFor(m => m.Password, new { @class = "form-control" });
    <button type="submit">Login</button>
}
```
---

##  Final Notes

-  **Tag Helpers in ASP.NET Core** automatically include the anti-forgery token.
-  **HtmlHelpers in ASP.NET MVC/Core require `@Html.AntiForgeryToken()` manually.**
    If you forget to add the anti-forgery token, **the request will be rejected (403 Forbidden).**

| Condition                              | `[ValidateAntiForgeryToken]` | `ModelState.IsValid` | Result                                  |
| -------------------------------------- | ---------------------------- | -------------------- | --------------------------------------- |
| **No token / invalid token**           | ❌ Fails (403 Forbidden)      | ❌ Not checked        | Request is blocked                      |
| **Valid token, but invalid form data** | ✅ Passes                     | ❌ Fails              | Returns the form with validation errors |
| **Valid token and valid form data**    | ✅ Passes                     | ✅ Passes             | Redirects to "Success"                  |

#### Note :
- We Will Know more about Validation-Token at JWT

-----------
###  **What is a Layout?**  
Microsoft provides a **default shared layout** that acts as a **template** for multiple views.  
It contains a special object called `@RenderBody`, which is **replaced by the actual View** when rendered.  

---

###  **Naming Rule for Layouts**
- The default layout follows **a naming convention**:  
  ```
  _Layout.cshtml
  ```
- It is usually stored in:  
  ```
  Views/Shared/_Layout.cshtml
  ```
- **You can have multiple layout styles** and apply them selectively.

---

###  **How to Apply a Custom Layout in a View?**  
You can specify a different layout in a View using the `Layout` property:  

```csharp
@model EmpWithDeptListViewModel
@{
    ViewData["Title"] = "Edit";
    Layout = "NewLayout"; // You can set it to null to remove layout
}
```
-  **This applies `"NewLayout.cshtml"`** instead of the default `_Layout.cshtml`.

---

###  **RenderBody vs RenderSection**  

### **1️⃣`@RenderBody` (Main Content Placeholder)**
- The **View replaces** `@RenderBody` inside the Layout.  
- **Required** in a Layout.  

**Example in `_Layout.cshtml`**:  
```html
<!DOCTYPE html>
<html>
<head>
    <title>@ViewData["Title"]</title>
</head>
<body>
    <header>My Header</header>
    <div>
        @RenderBody()  <!-- The view will be injected here -->
    </div>
    <footer>My Footer</footer>
</body>
</html>
```

---
### **2️⃣`@RenderSection` (Optional Sections)**  
- A **Layout can have multiple** `@RenderSection` placeholders.  
- These allow **specific sections** in Views to be inserted into the Layout.  
- It takes **a section name** and a **boolean** (optional, default = `true` means required).  

**Example in `_Layout.cshtml`**:  
```html
<!DOCTYPE html>
<html>
<head>
    <title>@ViewData["Title"]</title>
</head>
<body>
    <header>My Header</header>
    
    <div>
        @RenderBody() <!-- The main content of the View will be here -->
    </div>

    <div>
        @RenderSection("Sidebar", required: false) <!-- Optional Section -->
    </div>

    <footer>My Footer</footer>
</body>
</html>
```

---

###  **How to Implement Sections in a View?**  
#### **Inside `View.cshtml`**
```csharp
@section Sidebar {
    <p>This is the Sidebar content</p>
}
```
✔ This will be **inserted** into `@RenderSection("Sidebar")` inside the layout.

--- 
###  **Summary**
| Feature                | Purpose                                                    |
| ---------------------- | ---------------------------------------------------------- |
| `@RenderBody`          | Inserts the **main view content** inside the layout.       |
| `@RenderSection`       | Allows **custom sections** to be injected into the layout. |
| `Layout = "NewLayout"` | Changes the default layout for a specific view.            |

--------------------------


in ASP.NET MVC/Core, you **cannot override** an action method unless they have **different HTTP verbs** (e.g., `GET` and `POST`) or different method signatures (e.g., different parameters).

✅ **Correct Approach:**  
Use **attribute routing** (`[HttpGet]`, `[HttpPost]`, etc.) to differentiate actions.

-----
Validation is Done at 3 layers :
-   1-Mode
- 2-Controller
- 3-View

<img src="img/validation.png" alt="Screenshot" width="1000"/>

### ***Another Example***

- At Model

``` csharp
using System.ComponentModel.DataAnnotations;

public class UserViewModel
{
    [Required(ErrorMessage = "Username is required")]
    [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }

    [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
    public int Age { get; set; }
}
```


- At Controller
``` csharp
 [HttpPost]
    public IActionResult Register(UserViewModel model)
    {
      //ModelState is Dictionary 
      //it's key is Property name and it's value is property value
        if (!ModelState.IsValid)
        {
            return View(model); // Return the view with validation errors
        }

        // Process the valid model
        return RedirectToAction("Success");
    }
```

- ModelState is Dictionary 
    it's key is Property name and it's value is property value

- At View
 ``` html
 <form asp-action="Register">
    
    <!-- Validation Summary -->
    <div asp-validation-summary="All" class="text-danger"></div>    

    <div class="form-group">
        <label>Username</label>
        <input asp-for="Username" class="form-control" />
        <div class="text-danger">
            <span asp-validation-for="Username"></span>
        </div>
    </div>

    <div class="form-group">
        <label>Email</label>
        <input asp-for="Email" class="form-control" />
        <div class="text-danger">
            <span asp-validation-for="Email"></span>
        </div>
    </div>
    <button type="submit" class="btn btn-primary">Register</button>
</form>
```
 

- **`<div asp-validation-summary="All" class="text-danger"></div>`**: Displays and gather all validation errors at the top of the form.
- The `"All"` option shows **both model-level and property-level errors** :
    1- **Model-level errors** (e.g., errors added manually in the controller using- `ModelState.AddModelError("", "message")`)
    2- **Field-specific errors** (e.g., `[Required]`, `[EmailAddress]`)
- You can change it to:
 `"ModelOnly"` → Displays only model-level validation errors.
 `"None"` → Hides the validation summary (not recommended unless you use inline messages only).
----
#                      ***Custom Validation Erorr***

`ModelState.AddModelError` is a method in ASP.NET Core and ASP.NET MVC that allows you to manually add validation errors to the `ModelState` dictionary. This is useful when you want to return custom validation errors in your controllers. 

- Note:  `ModelState.AddModelError` makes `ModelState.IsValid` is **false**;

#### 1- `ModelState.AddModelError("FieldName", "Custom error message.");`

``` csharp
 Student st = context.Students.FirstOrDefault(b => b.StId == std.StId);

 if (std.DeptId == 0)
 {
     ModelState.AddModelError("DeptId", "Select Department ");
     return View("View1", std);
 }
```
<img src="img/custom.png" alt="Screenshot" width="500"/>

- ==Note That It is Server-Side== 
### **When Should You Use It ?**

1. **Custom Business Logic** – When validation depends on dynamic conditions.
2. **Database or External Checks** – When you need to check the database (e.g., unique email, existing username).
3. **General Errors** – When you want to show an error not related to a specific field.
4. **API Error Handling** – When returning structured validation errors in an API.
----------
#### **2- Making my Own Attribute** (==Server-Side==)

If I want to create a unique attribute for a name, I need to create a new model (class) and name it with 'Attribute' at the end, like `UniqueAttribute`.
``` csharp
public class UniqueAttribute : ValidationAttribute
{
     public string msg { get; set; }//to use it by attribute 

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return null;
        }
        
        string name = value.ToString();
        
        var context = new ItiContext();
        
        Student student = context.Students.
        FirstOrDefault(b => b.StFname == name);
      
         Student sts =(Student)validationContext.ObjectInstance;
        //To Use the validationContext  
        
        if (student != null)
        {
            return new ValidationResult("Error Message");
        }

        return ValidationResult.Success;
    }
}
```

```csharp

//At the Model
[Unique(msg ="Any Message")]
public string StFname { get; set; }
```

#### **1. `object? value`**

- This is the **value** of the property that the attribute is applied to.
- It contains the data that needs to be validated.
- It can be `null`, so you must check before using it.
- You typically cast it to the expected type (`string`, `int`, etc.).

#### **2. `ValidationContext validationContext`**

- This provides **metadata** about the validation process.
- It contains details about the **object being validated**.
- It is the Object of model that `UniqueAttribute` Called from 
- must make explicit casting to use it 

### Note Again :
- ####  it is Server Side 

----
To enable **custom client-side validation**, we need to **use `jQuery along with jQuery Validation and jQuery Unobtrusive Validation`**.
### **Steps to Set Up Client-Side Validation**

#### **1. Include jQuery and Validation Scripts**

You need to include the required JavaScript libraries at the end of the view:

```csharp
<script src="~/lib/jquery/dist/jquery.min.js"></script>
<script src="~/lib/jquery-validation/dist/jquery.validate.min.js"></script> <script src="~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"></script>
```
 **Why at the end?**  
Placing scripts at the bottom ensures that the entire page loads first, improving performance.

---

#### **2. Recommended Approach: Use `_Layout.cshtml` for Script Loading**

Instead of adding scripts manually in every view, it's best to load them in the `_Layout.cshtml` file using the built-in **`@RenderSectionAsync("Scripts")`** method.

***Note*** : Can add it by new section at end 

In `_Layout.cshtml` (usually before `</body>`):

`@await RenderSectionAsync("Scripts", required: false)`

This allows each view to **inject its own scripts** inside a `Scripts` section without duplicating code.

Then, in individual views:

```c#
@section Scripts {     
<script src="~/lib/jquery-validation/dist/jquery.validate.min.js"></script> 
<script src="~/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js"></script> 
}
```

📌 **Why don’t we include jQuery again?**  
Because `_Layout.cshtml` typically already includes `jquery.min.js`, so we only add validation scripts where needed.

---
#### **3- Making Remote Validation  (Remote)** 
(==Server-Side & Client-Side==)
`[Remote]` is an **attribute** used for **client-side validation** that makes an **AJAX call to the server** to validate a field **without submitting the entire form**.

- at model
```c#
public class UserModel
{
    [Required]
    [Remote(action:"check",controller:"Instructor",ErrorMessage ="MSG")]
    public string Username { get; set; }
}
```

- at controlled
```c#
 public IActionResult check(string Username)
 //name for attribute must be same for property
 {
     if(Username.Contains("Mr"))
     {
         return Json(true);
     }
     return Json(false);
 }
```
#### **Note** : Function's Parameter must be same name for Model's Property

- I Can Put more than property to check ...by  :
At Model
``` c#
public class UserModel
{
    [Required]
     [Remote(action:"check",controller:"Instructor",ErrorMessage ="MSG"
     ,AdditionalFields ="Address,Email"
     )]
    public string Username { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}
```

At Controller
```c#
public IActionResult check(string Username ,string Email ,string Address)
 {
     //You Can Check Any of them if there is any relationship between them 
 }
```

---
#### ***Note***: Validation By Remote I Must Call Jquery At View 
---

## Difference Between Remote Validation and Custom Validation in ASP.NET Core

| Feature                        | Remote Validation (`[Remote]`)                              | Custom Validation (`ValidationAttribute`)    |
| ------------------------------ | ----------------------------------------------------------- | -------------------------------------------- |
| **Where It Runs?**             | Client-side (AJAX) + Server                                 | Server-side only                             |
| **Uses AJAX?**                 | ✅ Yes                                                       | ❌ No                                         |
| **Ideal For?**                 | Checking **existing records** (e.g., unique username/email) | Business rules, **complex field validation** |
| **Requires JavaScript?**       | ✅ Yes                                                       | ❌ No                                         |
| **Multiple Field Validation?** | ✅ Yes, with `AdditionalFields`                              | ✅ Yes, using `ValidationContext`             |

### **When to Use What?**
- ✅ **Use Remote Validation (`[Remote]`)** when you need **real-time** checks (like checking if an email is taken).  
- ✅ **Use Custom Validation (`ValidationAttribute`)** when you need **complex logic** or **server-only validation** (e.g., checking password strength).  

-------------
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
