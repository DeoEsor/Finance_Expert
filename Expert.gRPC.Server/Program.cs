using Expert.gRPC.Server.Services;
using KnowledgeBase;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<KnowledgeDb>();
builder.Services.AddGrpc();
var app = builder.Build();
app.Urls.Add("https://localhost:7260");
// Configure the HTTP request pipeline.
app.MapGrpcService<Expert_Service>();
app.Run();