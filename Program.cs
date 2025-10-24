using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Azure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using DotNetEnv;

// Load environment variables from .env file
Env.Load();


var builder = WebApplication.CreateBuilder(args);


// Configure the server to listen on port 5000
/*
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.ListenLocalhost(5000);
});
*/

// Configure Azure AD app settings



// Usa DefaultAzureCredential per gestire automaticamente le credenziali disponibili
var credential = new DefaultAzureCredential();

// Register GraphServiceClient as a singleton service
builder.Services.AddSingleton<GraphServiceClient>(serviceProvider =>
{
    return new GraphServiceClient(credential);
});

// Add the MCP services: the transport to use (HTTP) and the tools to register.
builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithTools<RandomNumberTools>()
    .WithTools<MicrosoftGraphTools>();

var app = builder.Build();
app.MapMcp();
//app.MapMcp("/mcp");

app.UseRouting();

// Optional: if you're using HTTPS redirection
app.UseHttpsRedirection();

await app.RunAsync();
