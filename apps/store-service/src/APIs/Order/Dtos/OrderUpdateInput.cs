using StoreService.Core.Enums;

namespace StoreService.APIs.Dtos;

public class OrderUpdateInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public StatusEnum? Status { get; set; }

    public List<string>? OrderItems { get; set; }

    public string? Customer { get; set; }
}
