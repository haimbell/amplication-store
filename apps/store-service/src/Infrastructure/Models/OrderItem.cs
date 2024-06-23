using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Infrastructure.Models;

[Table("OrderItems")]
public class OrderItem
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? ItemId { get; set; }

    [ForeignKey(nameof(ItemId))]
    public Item? Item { get; set; } = null;

    public string? OrderId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public Order? Order { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
