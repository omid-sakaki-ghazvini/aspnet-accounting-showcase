namespace Accounting.Api.DTOs;

public record AccountDto(
    int Id,
    string Code,
    string Name,
    string Type,
    bool IsActive,
    string? Description
);