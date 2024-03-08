namespace SampleBlazorApp.Data.Entities;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int NumberOfQuotes { get; set; }
    public double TotalNet { get; set; }

    public virtual ICollection<Quote> Quotes { get; set; }
}
