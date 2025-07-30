
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
        if (ModelState.IsValid) // will explained in detail at day 5
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
## **Layout & RenderBody in ASP.NET Core MVC**  

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