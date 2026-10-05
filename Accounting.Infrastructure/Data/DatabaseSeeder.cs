using Accounting.Domain.Entities;
using Accounting.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Data;

/// <summary>
/// داده‌های اولیه‌ی سیستم حسابداری
/// کدینگ استاندارد حساب‌ها
/// </summary>
public static class DatabaseSeeder
{
    public static async Task SeedAsync(AccountingDbContext db)
    {
        // اگر حساب‌ها از قبل وجود دارند، seed نکن
        if (await db.Accounts.AnyAsync())
            return;

        // ============================================================
        // 1. دارایی‌ها (Assets)
        // ============================================================
        var assets = new Account
        {
            Code = "1",
            Name = "دارایی‌ها",
            Type = AccountType.Asset,
            IsActive = true,
            Description = "کل دارایی‌های شرکت"
        };
        db.Accounts.Add(assets);
        await db.SaveChangesAsync();

        var currentAssets = new Account
        {
            Code = "11",
            Name = "دارایی‌های جاری",
            Type = AccountType.Asset,
            ParentAccountId = assets.Id,
            IsActive = true
        };
        db.Accounts.Add(currentAssets);
        await db.SaveChangesAsync();

        db.Accounts.AddRange(
            new Account { Code = "1101", Name = "صندوق", Type = AccountType.Asset, ParentAccountId = currentAssets.Id, IsActive = true, Description = "موجودی نقدی صندوق" },
            new Account { Code = "1102", Name = "بانک", Type = AccountType.Asset, ParentAccountId = currentAssets.Id, IsActive = true, Description = "موجودی حساب‌های بانکی" },
            new Account { Code = "1103", Name = "حساب‌های دریافتنی", Type = AccountType.Asset, ParentAccountId = currentAssets.Id, IsActive = true, Description = "مطالبات از مشتریان" },
            new Account { Code = "1104", Name = "موجودی کالا", Type = AccountType.Asset, ParentAccountId = currentAssets.Id, IsActive = true, Description = "موجودی انبار" }
        );
        await db.SaveChangesAsync();

        var fixedAssets = new Account
        {
            Code = "12",
            Name = "دارایی‌های ثابت",
            Type = AccountType.Asset,
            ParentAccountId = assets.Id,
            IsActive = true
        };
        db.Accounts.Add(fixedAssets);
        await db.SaveChangesAsync();

        db.Accounts.Add(
            new Account { Code = "1201", Name = "تجهیزات", Type = AccountType.Asset, ParentAccountId = fixedAssets.Id, IsActive = true }
        );
        await db.SaveChangesAsync();

        // ============================================================
        // 2. بدهی‌ها (Liabilities)
        // ============================================================
        var liabilities = new Account
        {
            Code = "2",
            Name = "بدهی‌ها",
            Type = AccountType.Liability,
            IsActive = true
        };
        db.Accounts.Add(liabilities);
        await db.SaveChangesAsync();

        var currentLiabilities = new Account
        {
            Code = "21",
            Name = "بدهی‌های جاری",
            Type = AccountType.Liability,
            ParentAccountId = liabilities.Id,
            IsActive = true
        };
        db.Accounts.Add(currentLiabilities);
        await db.SaveChangesAsync();

        db.Accounts.AddRange(
            new Account { Code = "2101", Name = "حساب‌های پرداختنی", Type = AccountType.Liability, ParentAccountId = currentLiabilities.Id, IsActive = true, Description = "بدهی به تأمین‌کنندگان" },
            new Account { Code = "2102", Name = "حقوق پرداختنی", Type = AccountType.Liability, ParentAccountId = currentLiabilities.Id, IsActive = true }
        );
        await db.SaveChangesAsync();

        // ============================================================
        // 3. سرمایه (Equity)
        // ============================================================
        var equity = new Account
        {
            Code = "3",
            Name = "سرمایه",
            Type = AccountType.Equity,
            IsActive = true
        };
        db.Accounts.Add(equity);
        await db.SaveChangesAsync();

        db.Accounts.AddRange(
            new Account { Code = "3101", Name = "سرمایه اولیه", Type = AccountType.Equity, ParentAccountId = equity.Id, IsActive = true },
            new Account { Code = "3102", Name = "سود و زیان انباشته", Type = AccountType.Equity, ParentAccountId = equity.Id, IsActive = true }
        );
        await db.SaveChangesAsync();

        // ============================================================
        // 4. درآمدها (Revenue)
        // ============================================================
        var revenues = new Account
        {
            Code = "4",
            Name = "درآمدها",
            Type = AccountType.Revenue,
            IsActive = true
        };
        db.Accounts.Add(revenues);
        await db.SaveChangesAsync();

        db.Accounts.AddRange(
            new Account { Code = "4101", Name = "درآمد فروش", Type = AccountType.Revenue, ParentAccountId = revenues.Id, IsActive = true, Description = "درآمد حاصل از فروش کالا و خدمات" },
            new Account { Code = "4102", Name = "درآمد خدمات", Type = AccountType.Revenue, ParentAccountId = revenues.Id, IsActive = true }
        );
        await db.SaveChangesAsync();

        // ============================================================
        // 5. هزینه‌ها (Expenses)
        // ============================================================
        var expenses = new Account
        {
            Code = "5",
            Name = "هزینه‌ها",
            Type = AccountType.Expense,
            IsActive = true
        };
        db.Accounts.Add(expenses);
        await db.SaveChangesAsync();

        var operatingExpenses = new Account
        {
            Code = "51",
            Name = "هزینه‌های عملیاتی",
            Type = AccountType.Expense,
            ParentAccountId = expenses.Id,
            IsActive = true
        };
        db.Accounts.Add(operatingExpenses);
        await db.SaveChangesAsync();

        db.Accounts.AddRange(
            new Account { Code = "5101", Name = "هزینه حقوق و دستمزد", Type = AccountType.Expense, ParentAccountId = operatingExpenses.Id, IsActive = true },
            new Account { Code = "5102", Name = "هزینه اجاره", Type = AccountType.Expense, ParentAccountId = operatingExpenses.Id, IsActive = true },
            new Account { Code = "5103", Name = "هزینه آب، برق و گاز", Type = AccountType.Expense, ParentAccountId = operatingExpenses.Id, IsActive = true },
            new Account { Code = "5104", Name = "هزینه‌های اداری", Type = AccountType.Expense, ParentAccountId = operatingExpenses.Id, IsActive = true }
        );
        await db.SaveChangesAsync();

        // ============================================================
        // 6. نمونه فاکتورها
        // ============================================================
        var invoice1 = new Invoice
        {
            Number = $"INV-{DateTime.UtcNow:yyyyMMdd}-0001",
            IssueDate = DateTime.UtcNow.AddDays(-30),
            DueDate = DateTime.UtcNow.AddDays(0),
            Status = InvoiceStatus.Paid,
            TaxAmount = 900000,
            Notes = "فاکتور نمونه ۱",
            Items = new List<InvoiceItem>
            {
                new() { Description = "خدمات مشاوره فناوری اطلاعات", Quantity = 1, UnitPrice = 5000000 },
                new() { Description = "پشتیبانی فنی ماهانه", Quantity = 2, UnitPrice = 2000000 }
            }
        };
        invoice1.CalculateTotals();

        var invoice2 = new Invoice
        {
            Number = $"INV-{DateTime.UtcNow:yyyyMMdd}-0002",
            IssueDate = DateTime.UtcNow.AddDays(-15),
            DueDate = DateTime.UtcNow.AddDays(15),
            Status = InvoiceStatus.Sent,
            TaxAmount = 450000,
            Notes = "فاکتور نمونه ۲",
            Items = new List<InvoiceItem>
            {
                new() { Description = "طراحی وب‌سایت", Quantity = 1, UnitPrice = 4500000 }
            }
        };
        invoice2.CalculateTotals();

        db.Invoices.AddRange(invoice1, invoice2);

        await db.SaveChangesAsync();
    }
}