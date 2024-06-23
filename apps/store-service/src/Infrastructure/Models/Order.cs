using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StoreService.Core.Enums;

namespace StoreService.Infrastructure.Models;

[Table("Orders")]
public class Order
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    public List<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();

    public StatusEnum? Status { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
