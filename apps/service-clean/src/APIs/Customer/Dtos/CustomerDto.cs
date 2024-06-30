namespace ServiceClean.APIs.Dtos;

public class CustomerDto : CustomerIdDto
{
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
