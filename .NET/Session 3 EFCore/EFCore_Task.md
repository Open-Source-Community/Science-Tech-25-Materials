# Entity Framework Core Setup Task

## 🧾 Task Overview

Your task is to create a new .NET project using **Entity Framework Core**. You will:

1. Set up a new .NET console
2. Install the required EF Core NuGet packages  
3. Create a `DbContext` class  
4. Create `Movie`, `Cast`, and `MovieCast` classes
5. Create a `Director` class and configure a **one-to-many** relationship with `Movie`  
6. Configure a many-to-many relationship  manually between `Movie`, `Cast` using **Fluent API**  
7. Rename the `Cast` table to **`Actors`** in the database  
8. Load movie with their cast using **eager loading**
