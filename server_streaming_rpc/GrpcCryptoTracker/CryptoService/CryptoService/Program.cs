var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<CryptoService.Services.CryptoServiceImpl>(); //CryptoServiceImpl�i kullanaca��n� belirtir.

app.MapGet("/", () => "CryptoService gRPC API is running...");

app.Run();
