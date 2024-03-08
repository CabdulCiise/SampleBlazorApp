namespace SampleBlazorApp.Core.DataTransferObjects;

public class QuoteDto
{
    public DateTime QuoteSentDate { get; set; }
    public string SalesPerson { get; set; }
    public string ProjectName { get; set; }
    public string ProjectCode { get; set; }
    public string Customer { get; set; }
    public string CustomerCity { get; set; }
    public string CustomerState { get; set; }
    public string MarketingCategory { get; set; }
    public int NumberOfQuotes { get; set; }
    public double TotalNet { get; set; }
}
