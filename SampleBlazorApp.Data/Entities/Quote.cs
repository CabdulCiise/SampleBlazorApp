using SampleBlazorApp.Core.DataTransferObjects;

namespace SampleBlazorApp.Data.Entities;

public class Quote
{
    public int Id { get; set; }
    public DateTime SentDate { get; set; }
    public int CustomerId { get; set; }
    public int SalesPersonId { get; set; }
    public int ProjectId { get; set; }

    public virtual Customer Customer { get; set; }
    public virtual SalesPerson SalesPerson { get; set; }
    public virtual Project Project { get; set; }

    public QuoteDto ToDto()
    {
        return new QuoteDto
        {
            QuoteSentDate = SentDate,
            SalesPerson = SalesPerson.FirstName + " " + SalesPerson.LastName,
            Customer = Customer.FirstName + " " + Customer.LastName,
            CustomerCity = Customer.City,
            CustomerState = Customer.State,
            MarketingCategory = Customer.MarketingCategory?.Name,
            NumberOfQuotes = Project.NumberOfQuotes,
            ProjectName = Project.Name,
            ProjectCode = Project.Code,
            TotalNet = Project.TotalNet
        };
    }
}
