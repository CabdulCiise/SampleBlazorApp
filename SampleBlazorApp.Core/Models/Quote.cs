namespace SampleBlazorApp.Core.Models;

public class Quote
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int SalesPersonId { get; set; }
    public int ProjectId { get; set; }
    public double Price { get; set; }
}
