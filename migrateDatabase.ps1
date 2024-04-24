param(
    [Parameter(Mandatory=$true)]
    [string]$MigrationName
)

function Invoke-DotNetEFCommand {
    param(
        [string]$Command,
        [string]$ProjectPath,
        [string]$StartupProjectPath,
        [string]$AdditionalArguments = ""
    )

    $dotnetCommand = "dotnet ef $Command --project $ProjectPath --startup-project $StartupProjectPath $AdditionalArguments"
    try {
        Write-Host "Executing: $dotnetCommand"
        Invoke-Expression $dotnetCommand
        if ($LASTEXITCODE -ne 0) {
            throw "Command failed with exit code $LASTEXITCODE."
        }
    }
    catch {
        Write-Error "An error occurred: $_"
        exit
    }
}

$InfrastructureProjectPath = Join-Path -Path "src" -ChildPath "Infrastructure"
$WebStartupProjectPath = Join-Path -Path "src" -ChildPath "Web"
$MigrationsOutputDir = "Data\Migrations"

# Add Migration
$addMigrationArgs = "migrations add `"$MigrationName`" --output-dir $MigrationsOutputDir"
Invoke-DotNetEFCommand -Command $addMigrationArgs -ProjectPath $InfrastructureProjectPath -StartupProjectPath $WebStartupProjectPath

# Update Database
Invoke-DotNetEFCommand -Command "database update" -ProjectPath $InfrastructureProjectPath -StartupProjectPath $WebStartupProjectPath

Write-Host "Migration $MigrationName applied successfully and database updated."
