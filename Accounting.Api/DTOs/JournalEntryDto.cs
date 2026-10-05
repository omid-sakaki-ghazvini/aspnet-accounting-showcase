namespace Accounting.Api.DTOs;

public record JournalEntryDto(
    int Id,
    string Number,
    DateTime Date,
    string Description,
    string? Reference,
    string Status,
    decimal TotalDebit,
    decimal TotalCredit,
    bool IsBalanced,
    List<JournalLineDto> Lines
);

public record JournalLineDto(
    int Id,
    int AccountId,
    string AccountName,
    decimal Debit,
    decimal Credit,
    string? Description
);