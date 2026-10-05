using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Data;

/// <summary>
/// DbContext اصلی سیستم حسابداری
/// </summary>
public class AccountingDbContext : DbContext
{
    public AccountingDbContext(DbContextOptions<AccountingDbContext> options)
        : base(options)
    {
    }

    // ============================================================
    // DbSets
    // ============================================================
    
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
    public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();
    public DbSet<JournalLine> JournalLines => Set<JournalLine>();

    // ============================================================
    // Model Configuration
    // ============================================================
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ------------------------------------------------------------
        // Account
        // ------------------------------------------------------------
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(20);
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);
            
            entity.Property(e => e.Description)
                .HasMaxLength(500);
            
            entity.HasIndex(e => e.Code)
                .IsUnique();
            
            // رابطه‌ی درختی (Parent-Child)
            entity.HasOne(e => e.ParentAccount)
                .WithMany(e => e.ChildAccounts)
                .HasForeignKey(e => e.ParentAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ------------------------------------------------------------
        // Invoice
        // ------------------------------------------------------------
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Number)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.SubTotal)
                .HasPrecision(18, 2);
            
            entity.Property(e => e.TaxAmount)
                .HasPrecision(18, 2);
            
            entity.Property(e => e.Total)
                .HasPrecision(18, 2);
            
            entity.Property(e => e.Notes)
                .HasMaxLength(1000);
            
            entity.HasIndex(e => e.Number)
                .IsUnique();
            
            entity.HasMany(e => e.Items)
                .WithOne(e => e.Invoice)
                .HasForeignKey(e => e.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ------------------------------------------------------------
        // InvoiceItem
        // ------------------------------------------------------------
        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);
            
            entity.Property(e => e.UnitPrice)
                .HasPrecision(18, 2);
            
            // LineTotal یک computed property است، در دیتابیس ذخیره نمی‌شود
            entity.Ignore(e => e.LineTotal);
        });

        // ------------------------------------------------------------
        // JournalEntry
        // ------------------------------------------------------------
        modelBuilder.Entity<JournalEntry>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Number)
                .IsRequired()
                .HasMaxLength(50);
            
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500);
            
            entity.Property(e => e.Reference)
                .HasMaxLength(100);
            
            entity.HasIndex(e => e.Number)
                .IsUnique();
            
            entity.HasMany(e => e.Lines)
                .WithOne(e => e.JournalEntry)
                .HasForeignKey(e => e.JournalEntryId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // محاسبات، در دیتابیس ذخیره نمی‌شوند
            entity.Ignore(e => e.TotalDebit);
            entity.Ignore(e => e.TotalCredit);
            entity.Ignore(e => e.IsBalanced);
        });

        // ------------------------------------------------------------
        // JournalLine
        // ------------------------------------------------------------
        modelBuilder.Entity<JournalLine>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Debit)
                .HasPrecision(18, 2);
            
            entity.Property(e => e.Credit)
                .HasPrecision(18, 2);
            
            entity.Property(e => e.Description)
                .HasMaxLength(500);
            
            entity.HasOne(e => e.Account)
                .WithMany()
                .HasForeignKey(e => e.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}