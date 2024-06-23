using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Infrastructure.Models;

[Table("Customers")]
public class Customer
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    public List<Order>? Orders { get; set; } = new List<Order>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
