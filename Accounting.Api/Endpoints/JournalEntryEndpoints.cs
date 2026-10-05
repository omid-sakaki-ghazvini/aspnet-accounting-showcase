using Accounting.Api.DTOs;
using Accounting.Domain.Entities;
using Accounting.Domain.Enums;
using Accounting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Api.Endpoints;

public static class JournalEntryEndpoints
{
    public static void MapJournalEntryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/journal-entries")
            .WithTags("Journal Entries");

        // GET: همه‌ی اسناد
        group.MapGet("/", async (AccountingDbContext db) =>
        {
            var entries = await db.JournalEntries
                .Include(e => e.Lines)
                    .ThenInclude(l => l.Account)
                .OrderByDescending(e => e.Date)
                .Select(e => new JournalEntryDto(
                    e.Id,
                    e.Number,
                    e.Date,
                    e.Description,
                    e.Reference,
                    e.Status.ToString(),
                    e.TotalDebit,
                    e.TotalCredit,
                    e.IsBalanced,
                    e.Lines.Select(l => new JournalLineDto(
                        l.Id,
                        l.AccountId,
                        l.Account.Name,
                        l.Debit,
                        l.Credit,
                        l.Description)).ToList()))
                .ToListAsync();

            return Results.Ok(entries);
        })
        .WithName("GetAllJournalEntries");

        // POST: ساخت سند جدید
        group.MapPost("/", async (CreateJournalEntryDto dto, AccountingDbContext db) =>
        {
            if (dto.Lines is null || dto.Lines.Count < 2)
                return Results.BadRequest(new { message = "Journal entry must have at least 2 lines" });

            // اعتبارسنجی: مجموع بدهکار = مجموع بستانکار
            var totalDebit = dto.Lines.Sum(l => l.Debit);
            var totalCredit = dto.Lines.Sum(l => l.Credit);

            if (totalDebit != totalCredit)
                return Results.BadRequest(new
                {
                    message = "Journal entry is not balanced",
                    totalDebit,
                    totalCredit,
                    difference = totalDebit - totalCredit
                });

            // چک وجود حساب‌ها
            var accountIds = dto.Lines.Select(l => l.AccountId).Distinct().ToList();
            var existingAccounts = await db.Accounts
                .Where(a => accountIds.Contains(a.Id))
                .Select(a => a.Id)
                .ToListAsync();

            var missingAccounts = accountIds.Except(existingAccounts).ToList();
            if (missingAccounts.Count > 0)
                return Results.BadRequest(new
                {
                    message = "Some accounts were not found",
                    missingAccountIds = missingAccounts
                });

            // ساخت شماره سند خودکار
            var count = await db.JournalEntries.CountAsync();
            var number = $"JE-{DateTime.UtcNow:yyyyMMdd}-{(count + 1):D4}";

            var entry = new JournalEntry
            {
                Number = number,
                Date = dto.Date ?? DateTime.UtcNow,
                Description = dto.Description,
                Reference = dto.Reference,
                Status = JournalEntryStatus.Draft,
                Lines = dto.Lines.Select(l => new JournalLine
                {
                    AccountId = l.AccountId,
                    Debit = l.Debit,
                    Credit = l.Credit,
                    Description = l.Description
                }).ToList()
            };

            db.JournalEntries.Add(entry);
            await db.SaveChangesAsync();

            return Results.Created($"/api/journal-entries/{entry.Id}", new
            {
                entry.Id,
                entry.Number,
                entry.TotalDebit,
                entry.TotalCredit,
                entry.IsBalanced
            });
        })
        .WithName("CreateJournalEntry");

        // DELETE
        group.MapDelete("/{id:int}", async (int id, AccountingDbContext db) =>
        {
            var entry = await db.JournalEntries.FindAsync(id);
            if (entry is null)
                return Results.NotFound(new { message = $"Journal entry {id} not found" });

            if (entry.Status == JournalEntryStatus.Posted)
                return Results.BadRequest(new { message = "Cannot delete a posted journal entry" });

            db.JournalEntries.Remove(entry);
            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("DeleteJournalEntry");
    }
}