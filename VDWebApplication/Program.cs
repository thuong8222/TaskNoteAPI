using Microsoft.AspNetCore.WebSockets;
using Microsoft.Extensions.DependencyInjection;
using VDWebApplication.Controllers;
using WebSocketManager;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddWebSocketManager();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection(); 
app.MapControllers();

var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;

app.UseWebSockets();
//app.MapWebSocketManager("/connect", serviceProvider.GetService<ChatMessageHandler>());
app.Run();
