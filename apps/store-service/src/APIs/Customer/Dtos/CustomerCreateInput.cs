namespace StoreService.APIs.Dtos;

public class CustomerCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public List<OrderIdDto>? Orders { get; set; }

    public DateTime UpdatedAt { get; set; }
}
