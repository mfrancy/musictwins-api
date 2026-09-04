# Music Twins API

Backend API for [Music Twins](https://github.com/mfrancy/musictwins), responsible for communicating with the Last.fm API and providing the data required by the frontend.

## About

Music Twins is a music comparison application that allows users to explore their Last.fm profiles and discover similarities between their listening habits.

This API acts as an intermediary between the Angular frontend and Last.fm, keeping the Last.fm API key on the backend instead of exposing it in the client.

```text
Angular
   ↓
Music Twins API
   ↓
Last.fm API
```

## Technologies

- .NET 10
- ASP.NET Core Web API
- C#
- AutoMapper
- HttpClient
- Last.fm API
- Docker

## Architecture

The project follows a simple separation of responsibilities:

```text
Controllers/
    HTTP endpoints and request handling

DTOs/
    Data exposed by the API

Models/
    Last.fm response models

Services/
    Business logic and communication with Last.fm

Mapping/
    AutoMapper profiles
```

## API Endpoints

### Get User Profile

```http
GET /api/LastFm?username={username}
```

Returns the Last.fm user profile.

Example:

```http
GET /api/LastFm?username=franthepawn
```

Response:

```json
{
  "username": "franthepawn",
  "realname": "mari",
  "image": "https://...",
  "playCount": 107812,
  "artistCount": 1693,
  "trackCount": 10550
}
```

### Get Top Artists

```http
GET /api/LastFm/top-artists?username={username}
```

Returns the user's top artists.

Example:

```http
GET /api/LastFm/top-artists?username=franthepawn
```

Response:

```json
[
  {
    "name": "Artist Name",
    "playCount": 1234,
    "image": "https://...",
    "rank": "1"
  }
]
```

## Error Handling

The API validates the username before making requests to Last.fm.

| Status | Description |
|--------|-------------|
| `200` | Request completed successfully |
| `400` | Username is missing or invalid |
| `404` | User was not found |

## Configuration

The Last.fm API key is kept outside the source code using configuration secrets.

Expected configuration:

```text
LastFm:ApiKey
LastFm:BaseUrl
```

The API key should **never be committed to the repository**.

For local development, use .NET User Secrets.

For deployment, configure the API key as an environment variable/secret in the hosting provider.

## Running Locally

Clone the repository:

```bash
git clone https://github.com/mfrancy/musictwins-api.git
cd musictwins-api
```

Restore dependencies:

```bash
dotnet restore
```

Run the API:

```bash
dotnet run
```

The API will be available at the local URL provided by ASP.NET Core.

## Docker

The project includes a `Dockerfile` for containerized deployment.

Build the image:

```bash
docker build -t musictwins-api .
```

Run the container:

```bash
docker run -p 8080:8080 musictwins-api
```

## Project Structure

```text
musictwins-api/
├── Controllers/
│   └── LastFmController.cs
├── DTOs/
│   ├── TopArtistsDto.cs
│   └── UserProfileDto.cs
├── Mapping/
│   ├── TopArtistsMapper.cs
│   └── UserProfileMapper.cs
├── Models/
│   └── LastFmResponse.cs
├── Services/
│   └── LastFmService.cs
├── Dockerfile
├── Program.cs
├── appsettings.json
└── musictwins-api.csproj
```

## Architecture Flow

```text
Client
  │
  │ HTTP Request
  ▼
LastFmController
  │
  ▼
LastFmService
  │
  │ HTTP Request + API Key
  ▼
Last.fm API
  │
  │ JSON Response
  ▼
LastFmService
  │
  ▼
AutoMapper
  │
  ▼
DTO
  │
  ▼
HTTP Response
  │
  ▼
Client
```

## Music Twins

Music Twins is composed of:

- **Frontend:** Angular
- **Backend:** ASP.NET Core Web API
- **External API:** Last.fm

The backend is responsible for protecting the Last.fm API key and providing a simplified API for the frontend.
