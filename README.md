# C# CLI Task Tracker Application

A simple C# console application designed for task management, programmatic verification, and persistent JSON storage.

## Features
- **Task Management**: Create, track, and complete console tasks.
- **JSON Serialization**: Saves task state to local `tasks.json`.
- **Programmatic Assertion**: Embedded `RunSelfChecks()` verifies JSON serialization integrity before output rendering.

## How to Run
```zsh
dotnet run