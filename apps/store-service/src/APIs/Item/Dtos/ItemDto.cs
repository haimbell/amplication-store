namespace StoreService.APIs.Dtos;

public class ItemDto : ItemIdDto
{
    public DateTime CreatedAt { get; set; }

    public string? Name { get; set; }

    public List<OrderItemIdDto>? OrderItems { get; set; }

    public double? Price { get; set; }

    public DateTime UpdatedAt { get; set; }
}
