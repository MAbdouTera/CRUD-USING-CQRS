# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

A Todo Management CRUD API built with ASP.NET Core (.NET 10.0) demonstrating CQRS architecture with MediatR, FluentValidation, and Entity Framework Core (SQLite).

## Common Commands

```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the API (from the API/ directory)
dotnet run --project API

# Add EF Core migration
dotnet ef migrations add <MigrationName> --project Infrasturcture --startup-project API

# Apply migrations
dotnet ef database update --project Infrasturcture --startup-project API
```

The API runs on HTTPS port 7066 (see [API/Properties/launchSettings.json](API/Properties/launchSettings.json)).

A REST client test file is at [Requests/request.http](Requests/request.http) for manual endpoint testing.

## Architecture

Four-layer clean architecture with CQRS:

```
Domain → Application → Infrastructure
                    ↗
              API
```

- **Domain**: Core entity (`Todo`) with no external dependencies
- **Application**: CQRS commands/queries via MediatR, FluentValidation validators, `IAppDbContext` interface
- **Infrastructure** (spelled `Infrasturcture` in the filesystem — note the typo): EF Core `AppDbContext`, SQLite, entity configurations, migrations
- **API**: Controllers, request DTOs, global exception handler

### CQRS Structure

Each operation lives in `Application/Todos/Commands/<OperationName>/` or `Application/Todos/Queries/<OperationName>/` and contains three files: the command/query record, its handler, and (for commands) a FluentValidation validator.

### Request Pipeline

`API Controller` → `MediatR.Send()` → `ValidationBehavior` (validates via FluentValidation, throws on failure) → `Handler` → `IAppDbContext`

### Exception Handling

`GlobalExceptionHandler` in the API layer maps:
- `ValidationException` → HTTP 400
- `NotFoundException` → HTTP 404
- Unhandled → HTTP 500

### ID Generation

All entities use `Guid.CreateVersion7()` for time-ordered UUIDs.

## Key Notes

- `IAssemblyMarker` in the Application project is used for MediatR/FluentValidation assembly scanning registration in `Program.cs`.
- The Infrastructure project name is intentionally misspelled as `Infrasturcture` — keep this consistent when adding files or project references.
- The SQLite database file (`API/app.db`) is local and not committed to source control.

## Add Unit tests 
- Whenever you add any changes add unit tests ,run and make sure the tests passes.
