namespace BookScanner.Domain.Entities;

public class Book
{
    public Guid Id { get; set; }
    public string Isbn { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public int? Year { get; set; }
    public string Description { get; set; } = string.Empty;
    public string CoverImageUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
