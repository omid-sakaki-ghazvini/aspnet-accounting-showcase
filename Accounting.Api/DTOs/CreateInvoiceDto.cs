namespace Accounting.Api.DTOs;

public record CreateInvoiceDto(
    DateTime? IssueDate,
    DateTime? DueDate,
    decimal TaxAmount,
    string? Notes,
    List<CreateInvoiceItemDto> Items
);

public record CreateInvoiceItemDto(
    string Description,
    int Quantity,
    decimal UnitPrice
);