# ParkingSystem.Core

### Parking Management System — Backend

**ParkingSystem.Core** is the backend component of a Parking Management System built with **ASP.NET Core Web API**, **Entity Framework Core**, and **Microsoft SQL Server**.

The system provides RESTful APIs for managing parking spots, checking availability, creating reservations, validating bookings, and managing users.

## Architecture Overview

The solution follows a layered architecture based on Clean Architecture principles.

| Layer | Technology | Responsibility |
|---|---|---|
| Backend | ASP.NET Core Web API | REST API endpoints and business logic |
| Data Access | Entity Framework Core | Database operations and persistence |
| Database | Microsoft SQL Server | Users, parking spots, reservations, and availability |
| Frontend | Angular SPA | User interface provided by ParkingSystem.UI |

### Architecture and Design Principles

- Clean Architecture to separate application responsibilities.
- Repository Pattern for data access abstraction.
- Unit of Work Pattern for coordinating database operations.
- Dependency Injection for loosely coupled components.
- Entity Framework Core for database interaction.
- RESTful API design for communication with the frontend.

## Technology Stack

- ASP.NET Core Web API
- .NET 8
- C#
- Entity Framework Core
- Microsoft SQL Server
- Repository Pattern
- Unit of Work
- Dependency Injection
- Angular (Frontend integration)

## Core Functionalities

### Parking Management
- Retrieve available parking spots.
- Manage parking spot information.
- Check parking spot availability.

### Booking Management
- Create parking reservations.
- Check whether a parking spot is already booked.
- Validate booking duration.
- Retrieve invalid bookings.
- Apply fines for invalid bookings.

### User Management
- User registration.
- User authentication.
- Token-based authentication.
- User logout.

## API Documentation

The following REST endpoints are available according to the project API specification.

| HTTP Method | Endpoint | Description |
|---|---|---|
| POST | `/api/Booking/create-booking` | Create a new parking booking |
| GET | `/api/Booking/check-spot-already-booked` | Check whether a parking spot is already booked for a given user |
| GET | `/api/Booking/check-duration` | Validate booking duration |
| GET | `/api/Booking/not-valid-bookings` | Retrieve bookings that are not valid |
| GET | `/api/ParkingSpot/parking-spots` | Retrieve parking spots with availability |
| POST | `/api/UserManagement/registration` | Register a new user |
| POST | `/api/UserManagement/login` | Authenticate a user and obtain a token |
| POST | `/api/UserManagement/logout` | Log out a user |
| PUT | `/api/UserManagement/fine-parked-not-valid-booking` | Apply a fine for an invalid booking |

> Note: Request parameters, request bodies, response schemas, and authentication requirements should be verified against the actual API implementation or Swagger documentation.

## Database

The application uses Microsoft SQL Server to store parking management data.

The main data areas include:

- Users
- Parking Spots
- Parking Spot Slots
- Reservations and booking information
- Parking availability

### Database Setup

Before running the application:

1. Configure the SQL Server connection string in the backend configuration.
2. Create the database and required schema.
3. Apply Entity Framework Core migrations if available.
4. Execute the sample data script.

### Sample Data Generation

The repository includes:

`ScriptGenerateData.sql`

The script generates:

- 50 parking spots.
- 12 hourly slots for each parking spot.
- 600 parking slot records in total.
- Initial availability status set to `IsOccupied = 0`.

The generated slots cover the period from 01:00 to 13:00 on the current database server date.

**Important:** The current script assumes parking spot IDs correspond to values from 1 to 50. It should be executed against an appropriately prepared database and should not be run repeatedly without checking for existing records.

## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or a compatible .NET development environment
- Microsoft SQL Server
- Git

### 1. Clone the Repository

```bash
git clone https://github.com/mosaabudosh/ParkingSystem.Core.git
```

### 2. Navigate to the Project

```bash
cd ParkingSystem.Core
```

### 3. Restore Dependencies

Run the following command from the directory containing the solution or project file:

```bash
dotnet restore
```

### 4. Configure the Database

Update the SQL Server connection string in the backend configuration.

Ensure that the database and required tables are available before running the API.

### 5. Prepare Sample Data

Execute `ScriptGenerateData.sql` after creating the database schema.

### 6. Build the Backend

```bash
dotnet build
```

### 7. Run the API

Navigate to the directory containing the ASP.NET Core API `.csproj` file and run:

```bash
dotnet run
```

The API will start using the configured application environment and launch settings.

## Frontend Integration

The backend is designed to communicate with the Angular frontend application:

**ParkingSystem.UI**

Frontend Repository: [ParkingSystem.UI](https://github.com/mosaabudosh/ParkingSystem.UI)

The frontend consumes the backend REST APIs to support parking spot management, booking operations, and user management.

## Development Considerations

- Keep database connection strings outside source control.
- Validate booking requests on the server side.
- Ensure parking availability is checked when creating reservations.
- Maintain consistency between parking spots and their associated time slots.
- Keep API contracts synchronized with the frontend.
- Handle concurrent booking requests to avoid conflicting reservations.

## Future Improvements

Potential areas for further development include:

- Automated unit and integration testing.
- API validation and error response standardization.
- Improved booking concurrency handling.
- Automated database seeding.
- API versioning and expanded API documentation.
- CI/CD build and deployment workflows.

## Related Repository

**Frontend:** [ParkingSystem.UI](https://github.com/mosaabudosh/ParkingSystem.UI)

---

**ParkingSystem — Parking Management System**
