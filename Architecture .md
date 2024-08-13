Architecture Overview

For the test case, I employed a layered architecture so as to ensure a well-structred, scalable, and maintainable system. I use this approach to ensure clear separation of concerns and enhance testability, which are crucial for developing a robust backend system. I wrote this application using .Net 8.0.

API Layer (Presentation Layer): This layer serves as the entry point into the system. It exposes enpoints for interacting with the system. It handles HTTP requests and responses, converting them to and from the internal models.

Domain Layer: This layer focuses on domain-centric logic and entities. I use this layer to encapsulate core business rules and state, it provides a clear representation of the domain model.

Infrastructure Layer: This layer deals with data access. It implements the interface defined in the repository layer and interacts directly with the database.

Repository Layer: I usually add the repository layer to abstract the data access logic, providing a clean interface for querying and persisting data.

Shared Kernel: I house cross-cutting models and utilities used across different layers of the application here. It ensures consistency and reusability of common components.

Design Patterns
Dependency Injection:I utilized constructor injection. I used this pattern to manage dependencies between components, enhancing modularity and testability. This pattern ensures that my application conforms to the last principle of SOLID, that high-level class should not depend on low-level class. So I make both depend on abstractions.

Repository Pattern: This pattern abstracts data access logic, providing a clear interface for data operations. It decouples the business logic from data access code, making the system easier to test and maintain. By using this pattern, changes in data access implementation do not affect the business logic, for example, if I am required to change the database technology from SQLite, only a small change will be required and it won't affect other parts of the application.

Technology Choices
Database: I decided to use SQLite for its ease of use and minimal configuration requirements. It eliminates the need for a separate DBMS and simplifies the development and testing processes.

Entity Framework Core: My Object-Relational Mapper (ORM) of choice is Entity Framework Core, I used it for data access. It provides a high-level abstraction for interacting with the database. It simplifies database operations and integrates well with the layered architecture.

JWT (JSON Web Tokens): I employed JWT for authentication and authorization. It provides a stateless mechanism for securely transmitting user information between parties. I implemented a custom claims role-based authorization which ensures that access to resources is controlled based on user roles, enhancing the security of the application.

Fluent Validation: I used this library for validating and sanitizing user input. It provides a fluent interface for defining validation rules, making the validation logic more readable and maintainable. I use Fluent Validation to ensure that incoming data meets the required criteria before processing.

Error Handling
I implemented global error handling to manage and respond to exceptions consistently across the application.

Testing
For testing, xUnit was utilized due to its robust support for unit and integration testing in .NET applications.

Deployment
To facilitate smooth deployment and integration into production environments, I provided a Dockerfile. This Dockerfile ensures that the application can be containerized, allowing for consistent and reliable deployments across various environments.

Conclusion
This architecture and set of design decisions provide a solid foundation for developing a scalable, maintainable, and secure backend system. By leveraging a layered approach, design patterns, and carefully selected technologies, I designed the solution to meet the requirements of the test case while supporting future growth and changes.