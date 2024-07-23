namespace StoreService.APIs.Dtos;

public class OrderItemCreateInput
{
    public string? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Order? Order { get; set; }

    public Item? Item { get; set; }
}
