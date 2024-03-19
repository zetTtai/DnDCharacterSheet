# Presentation Layer

The Presentation Layer is where users interact with the system, through user interfaces, API swagger, etc.

## Folder Structure:

### ClientApp
- Contains the Angular project, providing the front-end user interface.

### Endpoints &rarr; REST API / HTTP Endpoints Definition
- Defines the RESTful API or HTTP endpoints that external clients can interact with.

### Infrastructure
- **CustomExceptionHandler.cs** &rarr; Centralizes exception handling in the Presentation Layer.
- **EndpointGroupBase.cs** &rarr; An abstract base class for grouping related endpoints.
- **IEndpointRouteBuilderExtensions.cs** &rarr; Extends `IEndpointRouteBuilder` interface from `Microsoft.AspNetCore.Routing` with additional methods.
- **MethodInfoExtensions.cs** &rarr; Adds more methods to the `MethodInfo` abstract class from `System.Reflection`.
- **WebApplicationExtensions.cs** &rarr; Provides additional methods for the `WebApplication` class from `Microsoft.AspNetCore.Builder`.

### Pages &rarr; CSHTML / Razor Pages
- Hosts HTML pages or Razor Pages, which are shared/partial pages based on the user's request.

### Properties &rarr; Includes LaunchSettings.json
- **LaunchSettings.json** &rarr; Used to declare environment variables for the application.

### Services &rarr; Includes CurrentUser.cs
- **CurrentUser.cs** &rarr; Provides an easy way for the UI to access data about the current user, independent of direct dependencies on complex infrastructure code.

### Templates &rarr; Auto-generated Code or Pages (.liquid Files)
- Stores templates for auto-generated code or pages, typically using the Liquid template language.

### wwwroot &rarr; Directory for Assets like CSS, JavaScript, Images, etc.
- Serves as the root directory for static assets such as CSS files, JavaScript files, images, and more.

### Program.cs &rarr; Entry Point for ASP.NET Core Apps
- Contains the main entry point for ASP.NET Core applications, configuring and running the web application.
