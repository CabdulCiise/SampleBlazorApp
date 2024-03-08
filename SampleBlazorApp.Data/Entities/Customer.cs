namespace SampleBlazorApp.Data.Entities;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int? MarketingCategoryId { get; set; }

    public virtual MarketingCategory MarketingCategory { get; set; }
}
