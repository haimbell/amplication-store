using StoreService.Core.Enums;

namespace StoreService.APIs.Dtos;

public class OrderCreateInput
{
    public string? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public StatusEnum? Status { get; set; }

    public List<OrderItem>? OrderItems { get; set; }

    public Customer? Customer { get; set; }
}
