using Cors;

var builder = WebApplication.CreateBuilder(args);

builder.AddCorsPolicy();
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("Cors");
app.MapControllers();
app.Run();
