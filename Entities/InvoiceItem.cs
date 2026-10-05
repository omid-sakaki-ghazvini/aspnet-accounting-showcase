namespace Accounting.Domain.Entities;

/// <summary>
/// ردیف فاکتور
/// </summary>
public class InvoiceItem : BaseEntity
{
    /// <summary>شرح کالا یا خدمت</summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>تعداد</summary>
    public int Quantity { get; set; }
    
    /// <summary>قیمت واحد</summary>
    public decimal UnitPrice { get; set; }
    
    /// <summary>مبلغ کل ردیف (محاسبه خودکار)</summary>
    public decimal LineTotal => Quantity * UnitPrice;
    
    /// <summary>ارتباط با فاکتور</summary>
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; } = null!;
}