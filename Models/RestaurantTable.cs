using System.ComponentModel.DataAnnotations;

namespace TheRongRestaurant;

public class RestaurantTable
{
    [Key] public int Id { get; set; }
    public string? TableName { get; set; }
    public bool IsOccupied { get; set; }
}
