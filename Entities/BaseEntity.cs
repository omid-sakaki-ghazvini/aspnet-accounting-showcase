namespace Accounting.Domain.Entities;

/// <summary>
/// کلاس پایه برای تمام موجودیت‌ها
/// شامل فیلدهای مشترک: Id, CreatedAt, UpdatedAt
/// </summary>
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}