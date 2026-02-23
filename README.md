# AkkaSample

Reactive trading system application built with Akka.NET, demonstrating the actor model with CQRS and event sourcing patterns.

## Stack

[![Stack](https://skillicons.dev/icons?i=cs,dotnet&theme=dark)](https://skillicons.dev)
## Structure

- `Program.cs` -- Application entry point
- `Bootstrapper.cs` -- Dependency wiring and actor system setup
- `Actors/` -- Actor definitions
  - `AbstractReceiveActor.cs` -- Base actor class
  - `Handlers/TradeCommandsHandlerActor.cs` -- Processes trade commands
  - `Handlers/TradeEventsHandlerActor.cs` -- Handles trade events
  - `Managers/TradeManagerActor.cs` -- Manages trade lifecycle
  - `SQL/BuildSqlQueryActor.cs` -- SQL query construction
  - `SQL/ExecuteSqlQueryActor.cs` -- SQL query execution
- `Commands/` -- Command definitions (CreateNewTrade, ExecuteTrade)
- `Domains/Aggregates/` -- Domain aggregates (Trade)
- `Domains/Events/` -- Domain events (CreatedTradeEvent)
- `Contracts/` -- Interfaces (IActorsFactory, IEventStore, IRepository)
- `Factories/` -- Actor factory implementation
- `Repositories/` -- Event store and repository implementations
- `Services/TradingSystemService.cs` -- Trading system service layer
