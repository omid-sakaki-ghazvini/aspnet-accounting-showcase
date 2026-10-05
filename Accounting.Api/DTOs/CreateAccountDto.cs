namespace Accounting.Api.DTOs;

public record CreateAccountDto(
    string Code,
    string Name,
    int Type,  // 1=Asset, 2=Liability, 3=Equity, 4=Revenue, 5=Expense
    int? ParentAccountId,
    string? Description
);