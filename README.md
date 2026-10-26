# Music Practice Tracker

Music Practice Tracker is a small .NET microservices application for recording instrument practice and totaling practice time.

## Services

- **InstrumentService** manages instruments such as guitar and piano.
- **PracticeService** records practice sessions with an instrument ID, date, and duration in minutes.
- **ProgressService** requests an instrument's sessions from PracticeService and returns the total minutes practiced.

## Technologies

- C# and ASP.NET Core on .NET 10
- Entity Framework Core 10 with SQLite
- Docker and Docker Compose
- HTTP communication between ProgressService and PracticeService

## Run with Docker Compose

Install and start Docker Desktop, then run these commands from the repository folder:

```powershell
docker compose up --build -d
docker compose ps
```

The services are available at:

- InstrumentService: `http://localhost:5001`
- PracticeService: `http://localhost:5002`
- ProgressService: `http://localhost:5003`

InstrumentService and PracticeService apply their EF Core migrations when they start. Their SQLite databases are stored in named Docker volumes, so the data remains after containers are stopped or recreated.

To stop the services while keeping the databases, run `docker compose down`. To also remove the database volumes, run `docker compose down -v`.

## Test the APIs

The examples below use PowerShell. First, create an instrument and note the returned `id`:

```powershell
$instrument = Invoke-RestMethod -Method Post -Uri http://localhost:5001/api/instruments -ContentType 'application/json' -Body '{"name":"Guitar"}'
$instrument
Invoke-RestMethod -Uri "http://localhost:5001/api/instruments/$($instrument.id)"
```

Use that instrument ID to add a practice session and retrieve it:

```powershell
$body = '{"instrumentId":' + $instrument.id + ',"date":"2026-10-25T10:00:00","durationMinutes":30}'
$session = Invoke-RestMethod -Method Post -Uri http://localhost:5002/api/practice -ContentType 'application/json' -Body $body
Invoke-RestMethod -Uri "http://localhost:5002/api/practice/$($session.id)"
```

Get the total practice time. ProgressService makes this request to PracticeService over the Compose network using the `practiceservice` service name:

```powershell
Invoke-RestMethod -Uri "http://localhost:5003/api/progress/$($instrument.id)"
```

Other PracticeService routes are `GET /api/practice`, `GET /api/practice/instrument/{instrumentId}`, and `DELETE /api/practice/{id}`. InstrumentService also supports `GET /api/instruments` and `DELETE /api/instruments/{id}`.

View container status and logs with:

```powershell
docker compose ps
docker compose logs
```