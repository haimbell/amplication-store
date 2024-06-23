namespace StoreService.APIs.Dtos;

public class OrderItemDto : OrderItemIdDto
{
    public DateTime CreatedAt { get; set; }

    public ItemIdDto? Item { get; set; }

    public OrderIdDto? Order { get; set; }

    public DateTime UpdatedAt { get; set; }
}
