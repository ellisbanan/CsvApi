var builder = WebApplication.CreateBuilder(args);

// Add controllers and services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<CsvApi.Services.ICsvService, CsvApi.Services.CsvService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
