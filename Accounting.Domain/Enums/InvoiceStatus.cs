namespace Accounting.Domain.Enums;

/// <summary>
/// وضعیت فاکتور فروش
/// </summary>
public enum InvoiceStatus
{
    /// <summary>پیش‌نویس</summary>
    Draft = 1,
    
    /// <summary>ارسال شده</summary>
    Sent = 2,
    
    /// <summary>پرداخت شده</summary>
    Paid = 3,
    
    /// <summary>سررسید گذشته</summary>
    Overdue = 4,
    
    /// <summary>لغو شده</summary>
    Cancelled = 5
}