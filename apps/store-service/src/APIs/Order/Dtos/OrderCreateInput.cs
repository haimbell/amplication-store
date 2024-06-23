using StoreService.Core.Enums;

namespace StoreService.APIs.Dtos;

public class OrderCreateInput
{
    public DateTime CreatedAt { get; set; }

    public CustomerIdDto? Customer { get; set; }

    public string? Id { get; set; }

    public List<OrderItemIdDto>? OrderItems { get; set; }

    public StatusEnum? Status { get; set; }

    public DateTime UpdatedAt { get; set; }
}
