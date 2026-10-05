namespace Accounting.Api.DTOs;

public record CreateJournalEntryDto(
    DateTime? Date,
    string Description,
    string? Reference,
    List<CreateJournalLineDto> Lines
);

public record CreateJournalLineDto(
    int AccountId,
    decimal Debit,
    decimal Credit,
    string? Description
);