# DnD Character Sheet

This project leverages a Clean Architecture template by Jason Taylor, implemented using a C# Web API backend and an Angular frontend. The primary goal is to create a robust and user-friendly website where Dungeons and Dragons (DnD) players can effortlessly create and modify their character sheets.

## Features
- **Web API (C#):** The backend is structured using a Clean Architecture approach to ensure scalability, maintainability, and decoupling. The API handles all data processing and business logic.
- **Frontend (Angular):** The frontend is built with Angular, providing a responsive and interactive user interface. It allows users to create, view, edit, and manage their DnD character sheets through a modern web browser.
- **PWA Compatibility:** The application is designed as a Progressive Web App, making it installable on any device, functioning offline, and loading instantly, regardless of the network state.
- **Dynamic Character Creation:** Users can create characters by selecting races, classes, and attributes with ease. The system provides dynamic adjustments of character capabilities based on selected traits.
- **Mobile Responsive:** Designed to be fully responsive, the website offers a seamless experience on both desktop and mobile devices.

## Technical Specifications
- **Clean Architecture:** By adhering to Clean Architecture principles, the project ensures that the application's domain logic and application logic are independent of the UI and the database. This makes the system easier to maintain and evolve over time.
- **Secure Authentication:** Incorporates secure authentication mechanisms to protect user data and prevent unauthorized access.
- **API Documentation:** Utilizes Swagger for API documentation, making it easier for new developers to understand and use the API effectively.
- **PWA Features:** Implementation of service workers for offline capability, manifest files for device installation, and responsive design for varied screen sizes.

# Gitflow

This section provides an overview of the Gitflow branching model and workflow of this repository.

## Branch Types and Naming Conventions

### 1. Master/Main
- **Purpose**: Main branch where the code is always production-ready.
- **Branch Name**: `main`

### 2. Stage/Development
- **Purpose**: Integration branch for development. Contains the latest delivered development changes.
- **Branch Name**: `stage`

### 3. Feature
- **Purpose**: Develop new features for upcoming releases. Merges back to `stage`.
- **Branch Name**: `feature/<feature-name>`
- **Example**: `feature/user-authentication`

### 4. Release
- **Purpose**: Support preparation of a new production release. Allows for minor bug fixes and preparing meta-data for a release.
- **Branch Name**: `release/<version>`
- **Example**: `release/1.0.0`

### 5. Hotfix
- **Purpose**: Act swiftly upon an undesired state of a live production version.
- **Branch Name**: `hotfix/<hotfix-name>`
- **Example**: `hotfix/urgent-fix-login`

### 6. Bugfix
- **Purpose**: Similar to hotfix but on a non-emergency basis from the `stage` branch.
- **Branch Name**: `bugfix/<bugfix-name>`
- **Example**: `bugfix/resolve-memory-leak`

### 7. Documentation
- **Purpose**: Manage and update documentation. Merges back to `stage`.
- **Branch Name**: `doc/<documentation-name>`
- **Example**: `doc/document-frontend`

## Workflow Overview
- **Feature branches** are created from `stage` and merged back when they are complete.
- **Release branches** are created from `stage` for preparing a release and merged into `main` and back into `stage` once the release is complete.
- **Hotfix branches** are created from `main` and are merged into both `main` and `stage`.
- **Bugfix branches** are created from `stage` and merged back when they are complete.
- **Documentation branches** are created from `stage` and merged back when they are complete.

This workflow ensures a structured approach to development, facilitating continuous integration and delivery while maintaining the stability and integrity of the production environment.

# Clean Architecture Solution Brief Explanation

The goal of this template is to provide a straightforward and efficient approach to enterprise application development, leveraging the power of Clean Architecture and ASP.NET Core. Using this template, you can effortlessly create a Single Page App (SPA) with ASP.NET Core and Angular or React, while adhering to the principles of Clean Architecture. Getting started is easy - simply install the **.NET template** (see below for full details).


## Getting Started

The following prerequisites are required to build and run the solution:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (latest version)
- [Node.js](https://nodejs.org/) (latest LTS)

Launch the app:
```bash
cd src/Web
dotnet run
```

To learn more, run the following command:
```bash
dotnet new ca-usecase --help
```

## Database

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

Running database migrations is easy. Ensure you add the following flags to your command (values assume you are executing from repository root)

* `--project src/Infrastructure` (optional if in this folder)
* `--startup-project src/Web`
* `--output-dir Data/Migrations`

For example, to add a new migration and update database from the root folder:

(If dotnet ef is not installed)

```bash
 dotnet tool install --global dotnet-ef
```

```bash
dotnet ef migrations add "SampleMigration" --project src\Infrastructure --startup-project src\Web --output-dir Data\Migrations
```

```bash
 dotnet ef database update --project src\Infrastructure --startup-project src\Web
```

## Deploy

The template includes a full CI/CD pipeline. The pipeline is responsible for building, testing, publishing and deploying the solution to Azure. If you would like to learn more, read the [deployment instructions](https://github.com/jasontaylordev/CleanArchitecture/wiki/Deployment).

## Technologies

* [ASP.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core 8](https://docs.microsoft.com/en-us/ef/core/)
* [Angular 17](https://angular.io/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq) & [Respawn](https://github.com/jbogard/Respawn)

## Support

If you are having problems, please let me know by [raising a new issue](https://github.com/zetTtai/DnDCharacterSheet/issues/new/choose).

## License

This project is licensed with the [MIT license](LICENSE).
