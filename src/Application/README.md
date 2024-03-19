# Application Layer (Depends on Model Layer)

Application logic, interfaces to interact with the "outside world" (UI, API).

## Folder Structure:

- **Common** &rarr; Shared resources/utilities
    - **Behaviours** &rarr; Common behavior applied to multiple classes (validation, logging, decorators, middleware,...) all of them have the suffix of "Behaviour" such as:
        - AuthorizationBehaviour &rarr; Auth Middleware
        - LoggingBehaviour &rarr; Logs
        - PerformanceBehaviour &rarr; Notify a warning if a request took longer than 500ms
        - UnhandledExceptionBehaviour &rarr; Global error handler middleware
        - ValidationBehaviour &rarr; Part of MediatR, validation middleware
    - **Exceptions** &rarr; Custom exceptions thrown by the application layer
    - **Interfaces** &rarr; Interface definitions for services in the application layer (contract-based design)
    - **Mappings** &rarr; AutoMapper configurations to convert Entities, ViewModels, and DTOs
    - **Models** &rarr; Shared DTOs or ViewModels used by more than one component in the Application layer
    - **Security** &rarr; Related to security features
        - AuthorizeAttribute extends Attribute &rarr; Usage:
        ```csharp
        [Authorize(Roles = Roles.Administrator)]
        [Authorize(Policy = Policies.CanPurge)]
        function() {}
        ```

- **A folder for each ENTITY** &rarr; All follow the same structure
    - **Commands** &rarr; Command Query Responsibility Segregation (CQRS) commands and handlers. A command is a query that modifies the state of the system.
        - Example: CreateTodoItem (folder) contains CreateTodoItemCommandValidator.cs and CreateTodoItemCommand.cs, this file includes:
            - Command &rarr; A record struct
            - CommandHandler &rarr; Adds a BaseEvent to Entity.DomainEvents (Explained in Domain Layer)
    - **EventHandlers** &rarr; Handlers that respond (notify) to entity-related events (These events may be published by Commands)
    - **Queries** &rarr; Data retrieval operations. Queries retrieve data without changing the system's state.
        - Data Transfer Objects (DTOs) go here
        - ViewModels (VMs) go here

# Commands vs Queries

A Command is an instruction to change the state of the system. This could mean altering data stored in a database, such as adding a new record (create), changing an existing one (update), or removing a record (delete). Commands are all about actions and side effects. They:

- Carry the intent and the necessary information to perform an action.
- Do not return data (or if they do, it's minimal, such as a success status or the ID of a newly created entity).
- Enforces a clear intention in the system's operations, making it explicit when data is intended to be modified.

A Query, in contrast, is a request for data. It does not modify the system's state; it only reads data and returns it. Queries are used for anything that involves fetching data from the system, such as displaying a list of items on a user interface or generating reports. They:

- Request and retrieve data based on specific criteria.
- Ensure that the operation has no side effects, meaning the state of the system before and after the query remains unchanged.
- Fast and efficient, optimizing for data retrieval without impacting the system's state.