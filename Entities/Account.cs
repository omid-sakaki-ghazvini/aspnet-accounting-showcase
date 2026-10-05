using Accounting.Domain.Enums;

namespace Accounting.Domain.Entities;

/// <summary>
/// حساب — هسته‌ی سیستم حسابداری
/// </summary>
public class Account : BaseEntity
{
    /// <summary>کد حساب (مثلاً 1101 برای صندوق)</summary>
    public string Code { get; set; } = string.Empty;
    
    /// <summary>نام حساب</summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>نوع حساب</summary>
    public AccountType Type { get; set; }
    
    /// <summary>حساب والد (برای ساختار درختی)</summary>
    public int? ParentAccountId { get; set; }
    public Account? ParentAccount { get; set; }
    
    /// <summary>حساب‌های فرزند</summary>
    public ICollection<Account> ChildAccounts { get; set; } = new List<Account>();
    
    /// <summary>آیا حساب فعال است؟</summary>
    public bool IsActive { get; set; } = true;
    
    /// <summary>توضیحات</summary>
    public string? Description { get; set; }
}