using Accounting.Api.DTOs;
using Accounting.Domain.Entities;
using Accounting.Domain.Enums;
using Accounting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Api.Endpoints;

public static class InvoiceEndpoints
{
    public static void MapInvoiceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/invoices")
            .WithTags("Invoices");

        // GET: همه‌ی فاکتورها
        group.MapGet("/", async (AccountingDbContext db) =>
        {
            var invoices = await db.Invoices
                .Include(i => i.Items)
                .OrderByDescending(i => i.IssueDate)
                .Select(i => new InvoiceDto(
                    i.Id,
                    i.Number,
                    i.IssueDate,
                    i.DueDate,
                    i.Status.ToString(),
                    i.SubTotal,
                    i.TaxAmount,
                    i.Total,
                    i.Notes,
                    i.Items.Select(item => new InvoiceItemDto(
                        item.Id,
                        item.Description,
                        item.Quantity,
                        item.UnitPrice,
                        item.LineTotal)).ToList()))
                .ToListAsync();

            return Results.Ok(invoices);
        })
        .WithName("GetAllInvoices");

        // GET: یک فاکتور خاص
        group.MapGet("/{id:int}", async (int id, AccountingDbContext db) =>
        {
            var invoice = await db.Invoices
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice is null)
                return Results.NotFound(new { message = $"Invoice {id} not found" });

            return Results.Ok(new InvoiceDto(
                invoice.Id,
                invoice.Number,
                invoice.IssueDate,
                invoice.DueDate,
                invoice.Status.ToString(),
                invoice.SubTotal,
                invoice.TaxAmount,
                invoice.Total,
                invoice.Notes,
                invoice.Items.Select(item => new InvoiceItemDto(
                    item.Id,
                    item.Description,
                    item.Quantity,
                    item.UnitPrice,
                    item.LineTotal)).ToList()));
        })
        .WithName("GetInvoiceById");

        // POST: ساخت فاکتور جدید
        group.MapPost("/", async (CreateInvoiceDto dto, AccountingDbContext db) =>
        {
            if (dto.Items is null || dto.Items.Count == 0)
                return Results.BadRequest(new { message = "Invoice must have at least one item" });

            // ساخت شماره فاکتور خودکار
            var count = await db.Invoices.CountAsync();
            var number = $"INV-{DateTime.UtcNow:yyyyMMdd}-{(count + 1):D4}";

            var invoice = new Invoice
            {
                Number = number,
                IssueDate = dto.IssueDate ?? DateTime.UtcNow,
                DueDate = dto.DueDate,
                TaxAmount = dto.TaxAmount,
                Notes = dto.Notes,
                Status = InvoiceStatus.Draft,
                Items = dto.Items.Select(item => new InvoiceItem
                {
                    Description = item.Description,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };

            invoice.CalculateTotals();

            db.Invoices.Add(invoice);
            await db.SaveChangesAsync();

            return Results.Created($"/api/invoices/{invoice.Id}", new
            {
                invoice.Id,
                invoice.Number,
                invoice.Total
            });
        })
        .WithName("CreateInvoice");

        // PATCH: تغییر وضعیت
        group.MapPatch("/{id:int}/status", async (int id, InvoiceStatus status, AccountingDbContext db) =>
        {
            var invoice = await db.Invoices.FindAsync(id);
            if (invoice is null)
                return Results.NotFound(new { message = $"Invoice {id} not found" });

            invoice.Status = status;
            invoice.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();

            return Results.Ok(new { message = "Status updated", status = status.ToString() });
        })
        .WithName("UpdateInvoiceStatus");

        // DELETE: حذف فاکتور
        group.MapDelete("/{id:int}", async (int id, AccountingDbContext db) =>
        {
            var invoice = await db.Invoices.FindAsync(id);
            if (invoice is null)
                return Results.NotFound(new { message = $"Invoice {id} not found" });

            db.Invoices.Remove(invoice);
            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("DeleteInvoice");
    }
}