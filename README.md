# KemNetEngine

KemNetEngine is a .NET library that provides foundational components for building TCP network applications, including engine lifecycle management, event processing, client connections, and server-side connection handling.

## Features

- `Engine` provides lifecycle management, frame-based updates, and synchronous/asynchronous operations.
- `Timer` and `ITime` support time tracking within an engine.
- The `KemNetEngine.Network` namespace provides `Connecter`, `Listener`, `IClient`, and configuration and result types for socket operations.
- The `KemNetEngine.Server` namespace provides `ServerEngine`, player and connection management, and server-side event handlers.
- `TestNetEngine` is a Windows Forms sample application for experimenting with client connections, listeners, and servers.

## Requirements

- .NET 10 SDK.
- NuGet package `Kem.Lib.Core` version `1.0.1` (declared by the library project).
- Windows to run the `TestNetEngine` sample application (`net10.0-windows`).

## Build

From the repository root, run:

```bash
dotnet build KemNetEngine.slnx
```

To build only the library:

```bash
dotnet build KemNetEngine/KemNetEngine.csproj
```

## Run the sample application

On Windows, run:

```bash
dotnet run --project TestNetEngine/TestNetEngine.csproj
```

The sample application provides a Windows Forms interface for experimenting with the client, listener, and server components.

## Project structure

```text
KemNetEngine/
  Engine.cs                 Engine lifecycle and update loop
  EngineRunner.cs           Engine runner support
  Timer.cs                  Timer
  Network/                  Client, listener, connections, and socket configuration
  Server/                   Server engine, players, and handlers
TestNetEngine/              Windows Forms sample application
```

## Dependencies

The library uses `Kem.Lib.Core` for shared types and utilities. NuGet restores this package during the build.

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE).
