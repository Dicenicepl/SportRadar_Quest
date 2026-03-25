# Sports Calendar

A simple ASP.NET Core MVC web application for managing and displaying sports events. Built as part of the Sportradar Coding Academy backend exercise.

## Tech stack

- **Backend**: ASP.NET Core MVC (.NET 8), C#
- **Database**: PostgreSQL via Entity Framework Core (Npgsql)
- **Frontend**: Razor Views, plain HTML/CSS

## Project structure

```
Quest/
├── Controllers/
│   ├── MatchController.cs       # MVC views (GET /matches)
│   └── MatchApiController.cs    # REST API (GET/POST /api/matches)
├── Models/
│   ├── Matches/   MatchEntity, MatchAddDto, StatusEnum
│   ├── Results/   ResultEntity
│   ├── Teams/     TeamEntity
│   ├── Sports/    SportEntity
│   ├── Stadiums/  StadiumEntity
│   ├── Goals/     GoalEntity
│   ├── Cards/     CardEntity, CardTypeEnum
│   └── PeriodScores/ PeriodScoreEntity
├── Views/
│   ├── Matches/   Index.cshtml, Details.cshtml, Add.cshtml
│   └── Shared/    _Layout.cshtml
├── AppDbContext.cs
├── DbSeed.cs
└── Program.cs
```

## Setup

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/) (running locally or via Docker)

### 1. Clone the repository

```bash
git clone https://github.com/Dicenicepl/SportRadar_Quest.git
cd Quest
```

### 2. Configure the database connection

Edit `appsettings.Development.json` and set your PostgreSQL connection string:

```json
{
  "ConnectionStrings": {
     "sportradarDatabase": "User ID=;Password=;Host=;Port=;Database="
  }
}
```

### 3. Create and apply the migration

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

> The application also runs `MigrateAsync()` automatically on startup, so `database update` is optional if you just want to run the app.

### 4. Run the application

```bash
dotnet run
```

The app will be available at `https://localhost:5078` (or the port shown in the terminal).

On first startup the database is automatically seeded with sample AFC Champions League 2024 matches.

## Features

### Web UI (`/matches`)

| Route                          | Description                                       |
| ------------------------------ | ------------------------------------------------- |
| `GET /matches`                 | List all matches, with optional filters           |
| `GET /matches?sport=Football`  | Filter by sport name                              |
| `GET /matches?date=2024-01-03` | Filter by date                                    |
| `GET /matches/{id}`            | Match detail: result, goals, cards, period scores |
| `GET /matches/add`             | Form to add a new match                           |
| `POST /matches/add`            | Save new match to database                        |

### REST API (`/api/matches`)

| Method | Route               | Description                                                    |
| ------ | ------------------- | -------------------------------------------------------------- |
| `GET`  | `/api/matches`      | Get all matches (supports `?sport=` and `?date=` query params) |
| `GET`  | `/api/matches/{id}` | Get single match by ID                                         |
| `POST` | `/api/matches`      | Add a new match (JSON body)                                    |

#### POST `/api/matches` — example request body

```json
{
  "sportId": 1,
  "homeTeamId": 1,
  "awayTeamId": 2,
  "stadiumId": 1,
  "season": 2024,
  "status": "SCHEDULED",
  "dateVenue": "2024-03-15",
  "timeVenue": "18:00:00"
}
```

## Database schema

The schema follows the third normal form (3NF). Key relationships:

- `Match` → `Sport`, `HomeTeam`, `AwayTeam`, `Stadium` (many-to-one)
- `Match` ↔ `Result` (one-to-one)
- `Result` → `Goals`, `Cards`, `PeriodScores` (one-to-many)
- `Result` → `Winner` (many-to-one, nullable)

Foreign keys use the `_` prefix convention (e.g. `_sport_id`, `_home_team_id`) as required.

## Assumptions & decisions

- **Stadium is required** in the current model (`_stadium_id` is non-nullable). The seeder uses a placeholder "Unknown" stadium for matches where venue data was not provided in the source JSON.
- **No authentication** — the app is a read/write calendar without user accounts, as the exercise did not require it.
- **Seed data** is based on the AFC Champions League 2024 Round of 16 matches provided in the exercise JSON. The Final entry (Urawa Reds, no home team) was omitted from the seed as the data was incomplete.
- **EF Core migrations** are not committed to the repository — run `dotnet ef migrations add InitialCreate` locally as described above.
