namespace StoreService.APIs.Dtos;

public class ItemUpdateInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Name { get; set; }

    public double? Price { get; set; }

    public List<string>? OrderItems { get; set; }
}
