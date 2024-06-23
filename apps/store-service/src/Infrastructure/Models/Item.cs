using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Infrastructure.Models;

[Table("Items")]
public class Item
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    public List<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();

    [Range(-999999999, 999999999)]
    public double? Price { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
