using System.ComponentModel.DataAnnotations;
namespace TheRongRestaurant;

public class Order
{
    [Key] public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public List<MenuItem>? Items { get; set; }
    public decimal TotalAmount { get; set; }
    public string? CustomerName { get; set; }
}

