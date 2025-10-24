
 # SampleMcpServer (.NET MCP Server)

 SampleMcpServer is a Model Context Protocol (MCP) server implemented in .NET. It provides small MCP tools for generating random numbers and accessing Microsoft 365 data via Microsoft Graph. The project is intended to be easy to run locally and deploy to Azure.

 ## Features

 - MCP HTTP server (default listening address: localhost:5000)
 - Included MCP tools:
	 - RandomNumberTools — generate random numbers and return Italian cities (north/south)
	 - MicrosoftGraphTools — helper methods to call Microsoft Graph (users, profiles, authentication)

 ## Requirements

 - .NET 9.0 SDK
 - Azure AD App registration (only required for Microsoft Graph functionality)
 - PowerShell on Windows (for the quick commands shown below)

 ## Build

 From the repository root run:

 ```powershell
 dotnet build SampleMcpServer.csproj
 ```

 ## Run (development)

 Run the MCP server locally using dotnet run:

 ```powershell
 dotnet run --project SampleMcpServer.csproj
 ```

 The server will start and listen on the configured HTTP port (default: 5000). You can also run a published executable if you prefer:

 ```powershell
 bin\Release\net9.0\win-x64\publish\SampleMcpServer.exe
 ```

 ## VS Code Tasks

 - build — compile the project
 - clean — remove build artifacts
 - publish-release — publish in Release configuration (used for Azure deployment)

 ## MCP Tools

 RandomNumberTools
 - GetRandomNumber(min, max) — returns a random number between min (inclusive) and max (exclusive)
 - GetItalyCities(northOrSouth) — returns an example Italian city from the north or south

 MicrosoftGraphTools
 - GetUsers, GetUserById, GetUserProfileAsync, GetGraphAuthenticationInfo
 - See `GRAPH_SETUP.md` for configuration details (Azure app registration, scopes, and settings)

 ## Debugging / Inspecting the MCP Server (Inspector)

 You can inspect and interact with the running MCP server using the official MCP Inspector. This is a Node-based tool distributed via npm and run with npx.

 1. Make sure you have Node.js and npm installed. If you don't, install them from https://nodejs.org/.
 2. Start the MCP server:

 ```powershell
 dotnet run --project SampleMcpServer.csproj
 ```

 3. In a separate terminal, run the inspector with npx:

 ```powershell
 npx @modelcontextprotocol/inspector
 ```

 4. The inspector will open a local web UI. Point your browser to the address it shows (commonly http://localhost:3000) and connect to your MCP server by entering the server URL, for example:

 ```text
 http://localhost:5000
 ```

 Notes:
 - If your MCP server is running on a different host or port, use that URL instead.
 - The inspector can connect to any MCP server that speaks the protocol over HTTP.

 ## Deploy to Azure

 See `DEPLOY_AZURE.md` for a step-by-step deployment guide. The basic flow is:

 1. Build and publish the project to the `bin\publish-azure` folder
 2. Compress the output folder to a zip file
 3. Login to Azure and create the required resources (Resource Group, App Service Plan, Web App)
 4. Deploy the zip package to the Web App
 5. Configure environment variables (Graph app client ID, tenant ID, client secret, etc.)

 ## Useful files

 - `GRAPH_SETUP.md` — Microsoft Graph configuration and required environment variables
 - `DEPLOY_AZURE.md` — Azure deployment guide

