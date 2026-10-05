namespace Accounting.Domain.Enums;

/// <summary>
/// وضعیت سند حسابداری
/// </summary>
public enum JournalEntryStatus
{
    /// <summary>پیش‌نویس</summary>
    Draft = 1,
    
    /// <summary>قطعی شده</summary>
    Posted = 2,
    
    /// <summary>برگشت خورده</summary>
    Reversed = 3
}