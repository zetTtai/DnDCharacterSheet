# Infrastructure Layer (Depends on Application Layer)

Persistence, APIs, and Database connection 

## Folder Structure:

### Data &rarr; DBContext, Migrations, and Repository Implementation

- **Configurations** &rarr; Configure the EntityFramework (EF) models to DB tables, relationships, schemas, constraints...
- **Interceptors** &rarr; Intercept DB operations to implement common behavior (Auditing and Logging)
- **Migrations** &rarr; Ensure Database structure
- **SQLite** &rarr; Specific SQLite configuration
- **ApplicationDbContext.cs** &rarr; EntityFramework (EF). Represents session with the database
- **ApplicationDbContextInitializer.cs** &rarr; Initialize DB with a specific configuration and seed data.

### Identity &rarr; Authentication and Authorization

- **ApplicationUser** &rarr; Extends from IdentityUser, represent the user in the system
- **IdentityResultExtensions** &rarr; Extension of IdentityResult (ToApplicationResult method is here)
- **IdentityService** &rarr; Handle identity-related operations (All of them are **ASYNC**):
    - GetUserName
    - CreateUser
    - IsInRole &rarr; Check if a user is assigned to a specific role
    - Authorize
    - DeleteUser &rarr; By Id and By User
