using StoreService.Core.Enums;

namespace StoreService.APIs.Dtos;

public class OrderDto : OrderIdDto
{
    public DateTime CreatedAt { get; set; }

    public CustomerIdDto? Customer { get; set; }

    public List<OrderItemIdDto>? OrderItems { get; set; }

    public StatusEnum? Status { get; set; }

    public DateTime UpdatedAt { get; set; }
}
