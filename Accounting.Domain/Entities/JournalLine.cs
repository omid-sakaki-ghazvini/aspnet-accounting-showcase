namespace Accounting.Domain.Entities;

/// <summary>
/// ردیف سند حسابداری
/// </summary>
public class JournalLine : BaseEntity
{
    /// <summary>حساب مربوطه</summary>
    public int AccountId { get; set; }
    public Account Account { get; set; } = null!;
    
    /// <summary>مبلغ بدهکار</summary>
    public decimal Debit { get; set; }
    
    /// <summary>مبلغ بستانکار</summary>
    public decimal Credit { get; set; }
    
    /// <summary>شرح ردیف</summary>
    public string? Description { get; set; }
    
    /// <summary>ارتباط با سند</summary>
    public int JournalEntryId { get; set; }
    public JournalEntry JournalEntry { get; set; } = null!;
}