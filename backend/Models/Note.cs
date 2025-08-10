namespace Backend.Models;

public class Note
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}
