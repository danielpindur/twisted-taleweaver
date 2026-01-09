# Twisted Taleweaver

Twisted Taleweaver is a turn-based dark-fantasy roleplaying platform built around narrative-first combat, persistent expeditions, and a modern service-oriented architecture.

It blends tabletop-inspired game design with real-time web technology: a C# backend that owns all game logic and data integrity, and a React frontend focused on immersive presentation and player interaction.

## Key features
- Turn-based "expeditions" (party sessions) and game engine logic
- Character creation and AI-assisted content generation via an OpenAI adapter
- GraphQL gateway with real-time updates (subscriptions) and auth integration
- Data access layer with migrations and Dapper-style SQL access

## Main components (high-level)
- `TwistedTaleweaver.Gateway`: GraphQL endpoint, authentication and subscriptions
- `TwistedTaleweaver.Database`: migrations and database runner
- `TwistedTaleweaver.DataAccess`: repositories and data mappers
- `TwistedTaleweaver.Integration.OpenAi`: OpenAI integration/adapters
- `TwistedTaleweaver.Integration.Clerk`: authentication integration (Clerk)
- `twisted-taleweaver-app`: React + Vite frontend

## Quick start
1. Create a `.env` file in the repository root, based on `.env.example` (if present).
2. Build and run services with Docker Compose:

```bash
docker compose up --build
```

3. Create a `.env.local` file in the `twisted-taleweaver-app` directory, based on `twisted-taleweaver-app/.env.example`.

4. Start the frontend development server:

```bash
cd twisted-taleweaver-app
npm install
npm run dev
```

## Authentication (Clerk)
- The project uses Clerk as the user and authentication provider. Server-side and frontend integrations are present in the repository (`TwistedTaleweaver.Integration.Clerk` and frontend environment variables).
- Backend: Clerk-related secrets are expected in configuration (example key names used in code include `Clerk:SecretKey`).
- Frontend: a publishable key is required in the app environment (commonly `VITE_CLERK_PUBLISHABLE_KEY`).
- See `src/TwistedTaleweaver.Integration.Clerk` for the server-side setup and `twisted-taleweaver-app` for frontend usage.

## Important configuration / environment variables
- Database connection strings: configured in `ConnectionStrings` (see `src/TwistedTaleweaver.Database/Program.cs`).
- OpenAI: `OpenAi:ApiKey` and `OpenAi:Model` (used by the OpenAI integration).
- Clerk: backend secret(s) (e.g., `Clerk:SecretKey`) and frontend publishable key (`VITE_CLERK_PUBLISHABLE_KEY`).
