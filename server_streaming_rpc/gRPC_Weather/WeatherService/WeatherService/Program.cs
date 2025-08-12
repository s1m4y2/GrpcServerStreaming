using WeatherService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
var app = builder.Build();

app.MapGrpcService<WeatherService.Services.WeatherServiceImpl>();
app.MapGet("/", () => "gRPC Weather Service running...");
app.Run();
