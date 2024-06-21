# DataStreamingService
A windows service for streaming data

# DataStreamingProcessor

DataStreamingProcessor is a .NET 6 Windows Service application designed to stream processed data from a SQL Server database to clients via WebSockets. It incorporates fault tolerance using Polly, structured logging using Serilog, and includes business logic for filtering, transforming, and aggregating data.

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Configuration](#configuration)
- [Usage](#usage)
  - [Running the Service](#running-the-service)
  - [Testing](#testing)
- [Architecture](#architecture)
  - [Components](#components)
  - [Data Processing Logic](#data-processing-logic)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Data Streaming**: Streams data from SQL Server to WebSocket clients.
- **Fault Tolerance**: Implements retry and circuit breaker patterns using Polly.
- **Logging**: Uses Serilog for structured logging.
- **Business Logic**: Filters, transforms, and aggregates data before streaming.

## Getting Started

### Prerequisites

- .NET 6 SDK
- SQL Server
- Visual Studio 2022 or later (optional, for development)

### Installation

1. **Clone the Repository**:

   ```bash
   git clone https://github.com/your-repo/DataStreamingProcessor.git
   cd DataStreamingProcessor

