namespace Accounting.Api.DTOs;

public record InvoiceDto(
    int Id,
    string Number,
    DateTime IssueDate,
    DateTime? DueDate,
    string Status,
    decimal SubTotal,
    decimal TaxAmount,
    decimal Total,
    string? Notes,
    List<InvoiceItemDto> Items
);

public record InvoiceItemDto(
    int Id,
    string Description,
    int Quantity,
    decimal UnitPrice,
    decimal LineTotal
);