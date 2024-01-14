using System.ComponentModel.DataAnnotations;

namespace TheRongRestaurant;

public class MenuItem
{
    [Key] public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
}
