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

// ثبت Endpoints
app.MapAccountEndpoints();
app.MapInvoiceEndpoints();
app.MapJournalEntryEndpoints();

app.Run();