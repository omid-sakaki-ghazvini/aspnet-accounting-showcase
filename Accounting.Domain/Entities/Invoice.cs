using Accounting.Domain.Enums;

namespace Accounting.Domain.Entities;

/// <summary>
/// فاکتور فروش
/// </summary>
public class Invoice : BaseEntity
{
    /// <summary>شماره فاکتور</summary>
    public string Number { get; set; } = string.Empty;
    
    /// <summary>تاریخ صدور</summary>
    public DateTime IssueDate { get; set; } = DateTime.UtcNow;
    
    /// <summary>تاریخ سررسید</summary>
    public DateTime? DueDate { get; set; }
    
    /// <summary>وضعیت فاکتور</summary>
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;
    
    /// <summary>جمع کل قبل از مالیات</summary>
    public decimal SubTotal { get; set; }
    
    /// <summary>مبلغ مالیات</summary>
    public decimal TaxAmount { get; set; }
    
    /// <summary>مبلغ نهایی</summary>
    public decimal Total { get; set; }
    
    /// <summary>توضیحات</summary>
    public string? Notes { get; set; }
    
    /// <summary>ردیف‌های فاکتور</summary>
    public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    
    /// <summary>محاسبه‌ی مجموع مبلغ</summary>
    public void CalculateTotals()
    {
        SubTotal = Items.Sum(i => i.LineTotal);
        Total = SubTotal + TaxAmount;
    }
}