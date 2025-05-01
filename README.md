# Minimal Blazor Client/Server Template

A clean, minimal template for ASP.NET Core with Blazor WebAssembly client-side rendering. This project demonstrates a modern web application with minimal boilerplate.

## Features

- **ASP.NET Core 8.0** - Latest .NET framework
- **Blazor WebAssembly** - Client-side rendering with C#
- **Minimal Server** - No Razor files on the server
- **Tailwind CSS** - Via CDN for styling
- **Entity Framework Core** - With SQLite database
- **SignalR** - For real-time communication

## Project Structure

- **BlazorMinimalApp.Client** - Blazor WebAssembly client application
- **BlazorMinimalApp.Server** - ASP.NET Core server application

## Demo Pages

- **Home** - Basic information about the template
- **Todo** - Demonstrates Entity Framework and SignalR with a todo list application

## Getting Started

### Prerequisites

- .NET 8.0 SDK or later

### Running the Application

1. Clone the repository
2. Navigate to the project directory
3. Run the application:

```bash
dotnet run --project BlazorMinimalApp.Server
```

4. Open your browser and navigate to `http://localhost:5000`

## License

This project is licensed under the MIT License - see the LICENSE file for details.