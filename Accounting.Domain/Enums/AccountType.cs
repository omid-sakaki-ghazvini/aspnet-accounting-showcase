namespace Accounting.Domain.Enums;

/// <summary>
/// نوع حساب در سیستم حسابداری
/// </summary>
public enum AccountType
{
    /// <summary>دارایی</summary>
    Asset = 1,
    
    /// <summary>بدهی</summary>
    Liability = 2,
    
    /// <summary>سرمایه</summary>
    Equity = 3,
    
    /// <summary>درآمد</summary>
    Revenue = 4,
    
    /// <summary>هزینه</summary>
    Expense = 5
}