# Employee Management System 
---

## **Entities**

1. **Employee**
    
    - `Id` (Primary Key)
    - `Name` (Required, Max Length: 100)
    - `Email` (Required, Unique)
    - `DepartmentId` (Foreign Key)
2. **Department**
    
    - `Id` (Primary Key)
    - `Name` (Required, Max Length: 100)
3. **Address**
    
    - `Id` (Primary Key)
    - `Street` (Required)
    - `City` (Required)
    - `EmployeeId` (Foreign Key)

### **Relationships:**

- **One-to-Many**: One `Department` has many `Employees`.
- **One-to-One**: One `Employee` has one `Address`.

---

## Task Deliverables
1. Configure Relationships using fluent API
2. Apply Migrations and Seed Data
3. Perform CRUD Operations: every operation in separate function
	1. Add new employees with their department and address 
	2. Update an employee’s department 
	3. Delete a department and How to check for Cascade Delete(search for it :>)
4. Loading
	1. Write Function in "program.cs" that use Eager Loading
	2. Write Function in "program.cs" that use Lazy Loading
	3. Write Function in "program.cs" that use Explicit Loading 
