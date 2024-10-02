using FluentValidation;
using GrpcGreeeter.Services;
using GrpcGreeeter.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddValidatorsFromAssemblyContaining<CoordinateValidator>(ServiceLifetime.Singleton);
builder.Services.AddSingleton<ValidationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<CalculatorService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
