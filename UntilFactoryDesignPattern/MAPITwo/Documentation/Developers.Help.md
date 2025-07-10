# Services Overview

## Definition

Services refer to any object that provides functionality to other parts of the application. They encapsulate any process that makes a change in the application and are typically implemented as classes with important business or process logic. Services are critical components that handle specific tasks, ensuring modularity and maintainability in the application architecture.

### Examples of Services

- **Middleware**: Browser Type Checking (e.g., a service that checks the browser type to ensure compatibility).
- Notification
- Validation
- Error Handling
- Repository
- Caching
- And more...

**Note**: Services are broader in scope than middleware. Middleware, such as Browser Type Checking, is a specific type of service that typically operates as an intermediary in request or process flows.

## Using Services

### Creating Service Instances

There are two primary approaches to creating service instances:

- **Legacy Approach**:

  - Directly instantiate a service using a constructor, e.g., `var myService = new service();`.
  - This approach tightly couples the code to specific implementations, making it harder to modify or test.

- **Modern Approach**:
  - Instead of direct instantiation, use interfaces to define contracts between different layers of the application. This promotes loose coupling and flexibility.
  - **Key Points**:
    - The lower layer implements or defines the interface.
    - The upper layer calls the interface, adhering to the defined contract.
    - A **contract** specifies conditions, features, and usage details for the service. It acts as a blueprint for how the service should be accessed and used.
    - Interfaces should be stored in a folder named `interfaces` or `contracts`, with names prefixed by `I` (e.g., `IName` or `interface`).

### Example: Browser Checker Service

To illustrate the modern approach:

1. Create a folder named `Interfaces` or `Contracts` within the `Middlewares` folder.
2. Define the interface, e.g.:
   ```csharp
   public interface IBrowserCheckerService
   {
       // Define methods and properties for browser checking
   }
   ```
3. Implement the interface in the service class:
   ```csharp
   public class BrowserCheckerService : IBrowserCheckerService
   {
       // Implementation logic for browser checking
   }
   ```
   This approach ensures that the upper layers interact with the service through the interface, allowing for easier substitution of implementations.

## SOLID Principles and Dependency Inversion

### SOLID Principles

The SOLID principles guide the design of maintainable and scalable software systems:

- **Single Responsibility Principle**: A class should have only one reason to change, meaning it should focus on a single responsibility.
- **Open/Closed Principle**: Software entities (classes, modules, etc.) should be open for extension but closed for modification.
- **Liskov Substitution Principle**: Derived classes should be substitutable for their base classes without breaking the system.
- **Interface Segregation Principle**: Clients should not be forced to depend on interfaces they don’t use, ensuring interfaces are specific to the client’s needs.
- **Dependency Inversion Principle**: High-level modules should not depend on low-level modules; both should depend on abstractions (e.g., interfaces).

### Dependency Inversion

- **Traditional Layers Pattern**:
  - The application is structured in layers: **Policy Layer** (namespaces) → **Mechanism Layer** (classes and their interactions using namespaces) → **Utilities Layer** (where instances are created for use).
  - This creates direct dependencies between classes and namespaces, which can lead to tight coupling.
- **Dependency Inversion Pattern**:
  - Instead of direct dependencies, introduce interfaces between related layers, classes, or namespaces.
  - Structure:
    ```
    Policy Layer → Interface of Policy Service → Mechanism Layer → Interface of Mechanism Service → Utility Layer
    ```
  - **Example**: In the `Middlewares` folder, create an `Interfaces` or `Contracts` subfolder and define an interface like `IBrowserCheckerService`. The `BrowserCheckerService` class implements this interface, and upper layers interact with it through the interface, reducing direct dependencies.

## Inversion of Control (IoC)

IoC is a design principle used by framework libraries where control is inverted. Instead of the programmer controlling the flow of the program, the framework takes control. This is often implemented through a **Service Factory**.

- **Service Factory**:
  - A class responsible for creating instances of all services across the project.
  - All methods in this class create new instances of other classes or services used throughout the application.
  - All classes have a dependency on this Service Factory class, centralizing instance creation.
- **Key Points**:
  - Instead of manually creating instances of services or classes (e.g., `new Service()`), the Service Factory handles instantiation.
  - **Singleton Pattern**: The Service Factory creates a single instance of each service the first time it is needed and reuses it for subsequent requests. This ensures efficient resource usage and consistency.

### Example of IoC

A class, often called a Service Factory, manages the creation of all service instances:

```csharp
public class ServiceFactory
{
    private static BrowserCheckerService _browserCheckerService;

    public static IBrowserCheckerService GetBrowserCheckerService()
    {
        if (_browserCheckerService == null)
        {
            _browserCheckerService = new BrowserCheckerService();
        }
        return _browserCheckerService;
    }
}
```

Here, the Service Factory ensures a singleton instance of `BrowserCheckerService` is created and reused.

## Dependency Injection

The most modern pattern combines **IoC**, **Dependency Inversion**, and the **Factory Pattern** into **Dependency Injection (DI)**. DI automates the process of providing dependencies to classes, reducing manual instantiation and improving modularity.

### Types of Dependency Injection

- **Constructor Injection**: Dependencies are provided through a class’s constructor. Commonly used in backend development.
  ```csharp
  public class MyClass
  {
      private readonly IBrowserCheckerService _browserCheckerService;
      public MyClass(IBrowserCheckerService browserCheckerService)
      {
          _browserCheckerService = browserCheckerService;
      }
  }
  ```
- **Property Injection**: Dependencies are set via public properties. Common in Blazor applications.
  ```csharp
  public class MyComponent
  {
      [Inject]
      public IBrowserCheckerService BrowserCheckerService { get; set; }
  }
  ```
- **Method Injection**: Dependencies are passed as parameters to a method. Used in specific backend scenarios.
  ```csharp
  public class MyClass
  {
      public void PerformAction(IBrowserCheckerService browserCheckerService)
      {
          // Use the service
      }
  }
  ```

### Key Concepts

- **Dependency**: Relationships between classes, where one class requires another to function.
- **Coupling**: Dependencies between layers. Lower coupling is better, as it makes the system more modular and easier to maintain.
- **Dependency Injection**: Automatically injects dependencies into classes, reducing the need for manual instantiation and improving testability and scalability.

# Services

Refer to any object that provides functionality to other parts of the application. Any Process that make a change on application. Is a class with important business or process.

**EX:** Middleware: Browser Type Checking ==> is a Service

- Notification
- Validation
- Error Handling
- Repository
- Caching
- …

Services is bigger than Middleware's.

## Using Service

**Create Service Instance:**

- **Legacy** ==> Creating an instance from the Class `var myService = new service();`

- **Modern:** instead layers and their dependencies, we use interface with a contract between different layers. We use interfaces with Contract conditions for access and use.
  - **Point:** downer layer implement/ define the interface and upper layer call it.
  - **Point:** contract like conditions, features, type of using service and related features to Service. **Contract** === Requires => Folder name interfaces or contracts (On this folders or file should define as IName or interface)

## SOLID - Dependency Inversion

- **Single Responsibility Principle** – A class should have only one reason to change.
- **Open/Closed Principle** – Software entities should be open for extension but closed for modification.
- **Liskov Substitution Principle** – Derived classes should be substitutable for their base classes without breaking the system.
- **Interface Segregation Principle** – Clients should not be forced to depend on interfaces they don't use.
- **Dependency Inversion Principle** – High-level modules should not depend on low-level modules, but both should depend on abstractions.

### Dependency Inversion:

**Traditional Layers Pattern:**
Policy Layer (Name Spaces)==> Mechanism Layer (Classes and use different classes with namespace and .) ==> Utilities Layer (create instance to use)

Thet are Dependencies between classes/ name spaces.

**Dependencies Inversion Pattern:**

**POINT:** It says instead this dependencies use an interface between related layers/ classes/ namespaces.

Policy Layer ==> Interface of Police Service==> Mechanism Layer ==> interface of Mechanism Service ==> Utility Layer

**Modern:**
create a folder Interfaces or Contract in Middlewares Folder==> Define what you want what is your need For Example: BrowserCheckerService.

```csharp
public interface IBrowserCheckerService(){}

## Summary

- Services are critical for encapsulating business or process logic in an application.
- The modern approach uses interfaces and contracts to decouple layers, adhering to SOLID principles, particularly Dependency Inversion.
- IoC, implemented via a Service Factory, centralizes instance creation and often uses the Singleton pattern.
- Dependency Injection combines IoC, Dependency Inversion, and Factory patterns to provide a robust, maintainable way to manage dependencies.
- By minimizing coupling and leveraging interfaces, modern service design improves the flexibility and scalability of applications.
```
