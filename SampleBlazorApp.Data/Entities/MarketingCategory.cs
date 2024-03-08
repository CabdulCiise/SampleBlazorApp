namespace SampleBlazorApp.Data.Entities;

public class MarketingCategory
{
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Customer> Customers { get; set; }
}
