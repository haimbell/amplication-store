namespace StoreService.APIs.Dtos;

public class OrderItemWhereInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Order { get; set; }

    public string? Item { get; set; }
}
