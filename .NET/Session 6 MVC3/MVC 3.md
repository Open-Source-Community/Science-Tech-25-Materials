
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
