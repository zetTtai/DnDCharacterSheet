- Houses acceptance tests that assess whether the system fulfills business requirements from an end-user **perspective**.
- Often written in Behavior-Driven Development (BDD) style, it includes tests in a language understandable by non-technical stakeholders.
- Utilizes scenarios defined in "Features" and implemented in "StepDefinitions" to simulate user interactions and validate end-to-end workflows.

# Requisites

You need to install Playwright
In the root folder of this project execute the following commands

```bash
dotnet tool install --global Microsoft.Playwright.CLI
```

```bash 
playwright install
```

In order to run these tests you need to execute the following commands in a terminal (Not in Visual Studio 2022 or where you run tests)
Launch the app:
```bash
cd src/Web
dotnet run
```

These tests only works if local is deployed before running them.