using Users.gRPC.Server.Contexts.Core;
using Users.gRPC.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<UsersRepository>();
builder.Services.AddGrpc();
var app = builder.Build();
app.Urls.Add("https://localhost:7280");
// Configure the HTTP request pipeline.
app.MapGrpcService<UsersService>();
app.Run();