namespace StoreService.APIs.Dtos;

public class OrderItemWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public ItemIdDto? Item { get; set; }

    public OrderIdDto? Order { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
