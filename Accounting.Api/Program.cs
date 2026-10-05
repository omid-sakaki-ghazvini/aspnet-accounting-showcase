using Accounting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// Services
// ============================================================

// DbContext
builder.Services.AddDbContext<AccountingDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// OpenAPI / Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ============================================================
// Middleware
// ============================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ============================================================
// Endpoints
// ============================================================

app.MapGet("/", () => new
{
    Name = "Accounting Showcase API",
    Version = "1.0.0",
    Author = "Omid Sakaki",
    Status = "Running"
});

app.Run();