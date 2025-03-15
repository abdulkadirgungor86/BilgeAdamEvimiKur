# Hybrid N-Tier E-Commerce Project on .NET 8.0 with C#

## Overview

After thorough evaluations, it has been decided that our project will be developed using a **Hybrid N-Tier architecture**. The primary reason for adopting a hybrid approach is that our project will run on the Core platform. Thanks to the Core platform’s middleware structure and its emphasis on interfaces, instance creation is achieved via dependency injection. Therefore, we found it appropriate to incorporate the interface system embraced by the Onion architecture into our design. This approach preserves the essence of the N-Tier architecture while adapting it to work with interfaces—notice the benefits of interface segregation. The project’s platform will be **.NET 8.0**.

## Project Layers and Their Responsibilities

Below is a breakdown of the layers implemented in our project along with their roles and the libraries they utilize.

---

### 1. **ENTITIES Layer**

This layer contains:
- Classes representing the tables in our database.
- Associated interfaces (if any).
- Enumerations (enums) representing constant sets of values.

**Used Libraries:**
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- `Microsoft.Extensions.Identity.Stores`

---

### 2. **CONF Layer**

This layer holds the configurations for the structures that will be sent to the database. It was separated to maintain a compact structure and is triggered by the DAL when the database is created.

**Used Libraries:**
- *None*

---

### 3. **DAL (Data Access Layer)**

This layer manages communication with the database through its embedded repositories, which perform CRUD operations. It uses EntityFramework Core and follows a Code-First approach to create the database. Additionally, fake data is generated via the Bogus library to facilitate testing.

**Used Libraries:**
- `Bogus`
- `Microsoft.EntityFrameworkCore.Proxies`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`

---

### 4. **BLL (Business Logic Layer)**

This layer handles the business process flow. Even if a class does not need to participate in the current CRUD operations, a Manager is still created for that class using the Manager pattern—to be prepared for future business process requirements. Interfaces are used along with dependency injection.

**Used Libraries:**
- `AutoMapper`

---

### 5. **COMMON Layer**

This layer includes classes and functions that can be used independently by any other layer. It provides general-purpose functionalities such as email sending and encryption, which can be required across all layers.

**Used Libraries:**
- `Microsoft.AspNetCore.Http.Features`
- `Newtonsoft.Json`
- `SkiaSharp`

---

### 6. **DTO (Data Transfer Object) Layer**

DTOs are simple objects used solely for carrying data between layers. They do not contain any business logic, which ensures data security during transfer and improves performance.

**Used Libraries:**
- `Newtonsoft.Json`

---

### 7. **IOC (Inversion of Control) Layer**

This layer manages dependencies by applying the inversion of control principle. Instead of a class instantiating its own dependencies, it receives them externally—making the code more flexible, testable, and reusable. This is typically implemented with dependency injection.

**Used Libraries:**
- `FluentValidation.AspNetCore`

---

### 8. **MAP (Mapping) Layer**

This layer is responsible for data mapping—that is, converting data from one model to another to ensure compatibility between layers. The mapping process is automated using the AutoMapper library.

**Used Libraries:**
- `AutoMapper`

---

### 9. **VALIDATION Layer**

This layer performs data validation to ensure that input or processed data complies with defined rules and constraints. Validations help maintain data security, consistency, and accuracy during data entry and processing.

**Used Libraries:**
- `FluentValidation`

---

### 10. **VIEWMODEL Layer**

In the Model-View-ViewModel (MVVM) design pattern, the ViewModel is responsible for managing data binding and UI logic. It acts as a bridge between the model and the view, providing the necessary data for the UI and handling user interactions. By separating business logic from UI logic, the ViewModel enhances code maintainability and testability.

**Used Libraries:**
- `X.PagedList`
- `X.PagedList.Mvc.Core`

---

### 11. **MVCUI Layer**

This layer represents the user interface. It interacts with the BLL layer using the Manager pattern to process operations and manage user interactions.

**Used Libraries:**
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.VisualStudio.Web.CodeGeneration.Design`
- `X.PagedList`
- `X.PagedList.Mvc.Core`

---

## Conclusion

Our architecture leverages the strengths of both traditional N-Tier and Onion architectures by incorporating interface segregation and dependency injection on a robust .NET 8.0 platform. This design ensures that our code remains maintainable, scalable, and adaptable to future requirements while providing a clear separation of concerns.

---

**More Info:**

- Explore different repository implementations or the Unit of Work pattern for further refinement.
- Integrate comprehensive testing for inter-layer interactions.
- Consider integrating Razor Pages or SPA frameworks (such as Angular or React) in the UI layer for enhanced user experience.
