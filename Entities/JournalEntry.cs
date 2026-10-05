using Accounting.Domain.Enums;

namespace Accounting.Domain.Entities;

/// <summary>
/// سند حسابداری
/// هر سند باید حداقل دو ردیف (بدهکار و بستانکار) داشته باشد
/// مجموع بدهکار = مجموع بستانکار
/// </summary>
public class JournalEntry : BaseEntity
{
    /// <summary>شماره سند</summary>
    public string Number { get; set; } = string.Empty;
    
    /// <summary>تاریخ سند</summary>
    public DateTime Date { get; set; } = DateTime.UtcNow;
    
    /// <summary>شرح سند</summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>ارجاع (مثلاً شماره فاکتور)</summary>
    public string? Reference { get; set; }
    
    /// <summary>وضعیت سند</summary>
    public JournalEntryStatus Status { get; set; } = JournalEntryStatus.Draft;
    
    /// <summary>ردیف‌های سند</summary>
    public ICollection<JournalLine> Lines { get; set; } = new List<JournalLine>();
    
    /// <summary>مجموع بدهکار</summary>
    public decimal TotalDebit => Lines.Sum(l => l.Debit);
    
    /// <summary>مجموع بستانکار</summary>
    public decimal TotalCredit => Lines.Sum(l => l.Credit);
    
    /// <summary>آیا سند متوازن است؟</summary>
    public bool IsBalanced => TotalDebit == TotalCredit;
}