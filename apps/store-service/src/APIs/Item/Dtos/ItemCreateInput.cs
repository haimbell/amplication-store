namespace StoreService.APIs.Dtos;

public class ItemCreateInput
{
    public string? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Name { get; set; }

    public double? Price { get; set; }

    public List<OrderItem>? OrderItems { get; set; }
}
