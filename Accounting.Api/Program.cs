using Accounting.Api.Endpoints;
using Accounting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// Services
// ============================================================

builder.Services.AddDbContext<AccountingDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ CORS — این بخش حیاتی است
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",
                "http://localhost:5174",
                "http://127.0.0.1:5173"
              )
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

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

// ✅ CORS — باید قبل از UseHttpsRedirection باشد
app.UseCors("AllowReact");

app.UseHttpsRedirection();

// ============================================================
// Seed
// ============================================================

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AccountingDbContext>();
    await db.Database.MigrateAsync();
    await DatabaseSeeder.SeedAsync(db);
}

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

app.MapAccountEndpoints();
app.MapInvoiceEndpoints();
app.MapJournalEntryEndpoints();

app.Run();