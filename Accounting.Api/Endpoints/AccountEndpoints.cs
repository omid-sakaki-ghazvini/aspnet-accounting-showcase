using Accounting.Api.DTOs;
using Accounting.Domain.Entities;
using Accounting.Domain.Enums;
using Accounting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Api.Endpoints;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/accounts")
            .WithTags("Accounts");

        // GET: همه‌ی حساب‌ها
        group.MapGet("/", async (AccountingDbContext db) =>
        {
            var accounts = await db.Accounts
                .OrderBy(a => a.Code)
                .Select(a => new AccountDto(
                    a.Id,
                    a.Code,
                    a.Name,
                    a.Type.ToString(),
                    a.IsActive,
                    a.Description))
                .ToListAsync();

            return Results.Ok(accounts);
        })
        .WithName("GetAllAccounts");

        // GET: یک حساب خاص
        group.MapGet("/{id:int}", async (int id, AccountingDbContext db) =>
        {
            var account = await db.Accounts.FindAsync(id);
            
            if (account is null)
                return Results.NotFound(new { message = $"Account {id} not found" });

            return Results.Ok(new AccountDto(
                account.Id,
                account.Code,
                account.Name,
                account.Type.ToString(),
                account.IsActive,
                account.Description));
        })
        .WithName("GetAccountById");

        // POST: ساخت حساب جدید
        group.MapPost("/", async (CreateAccountDto dto, AccountingDbContext db) =>
        {
            // چک تکراری بودن کد
            var exists = await db.Accounts.AnyAsync(a => a.Code == dto.Code);
            if (exists)
                return Results.BadRequest(new { message = $"Account with code '{dto.Code}' already exists" });

            var account = new Account
            {
                Code = dto.Code,
                Name = dto.Name,
                Type = (AccountType)dto.Type,
                ParentAccountId = dto.ParentAccountId,
                Description = dto.Description,
                IsActive = true
            };

            db.Accounts.Add(account);
            await db.SaveChangesAsync();

            return Results.Created($"/api/accounts/{account.Id}", new AccountDto(
                account.Id,
                account.Code,
                account.Name,
                account.Type.ToString(),
                account.IsActive,
                account.Description));
        })
        .WithName("CreateAccount");

        // DELETE: حذف حساب
        group.MapDelete("/{id:int}", async (int id, AccountingDbContext db) =>
        {
            var account = await db.Accounts.FindAsync(id);
            if (account is null)
                return Results.NotFound(new { message = $"Account {id} not found" });

            // چک: آیا حساب در اسناد استفاده شده؟
            var hasLines = await db.JournalLines.AnyAsync(l => l.AccountId == id);
            if (hasLines)
                return Results.BadRequest(new { message = "Cannot delete account with journal lines" });

            db.Accounts.Remove(account);
            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("DeleteAccount");
    }
}