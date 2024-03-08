using Microsoft.EntityFrameworkCore;
using SampleBlazorApp.Core.DataTransferObjects;
using SampleBlazorApp.Core.Services;
using SampleBlazorApp.Data.Contexts;

namespace SampleBlazorApp.Infrastructure.Services;

public class QuoteService : IQuoteService
{
    private readonly SampleContext sampleContext;

    public QuoteService(SampleContext sampleContext)
    {
        this.sampleContext = sampleContext;
    }

    public void AddQuotes(List<QuoteDto> quotes)
    {
        if (quotes != null && quotes.Any())
        {
            foreach (var quote in quotes)
            {
                var projectEntity = sampleContext.Projects.FirstOrDefault(x => x.Name == quote.ProjectName);

                if (projectEntity == null)
                {
                    projectEntity = new Data.Entities.Project
                    {
                        Name = quote.ProjectName,
                        Code = quote.ProjectCode,
                        NumberOfQuotes = quote.NumberOfQuotes,
                        TotalNet = quote.TotalNet,
                    };

                    sampleContext.Projects.Add(projectEntity);
                    sampleContext.SaveChanges();
                }

                var salesPersonEntity = sampleContext.SalesPeople.FirstOrDefault(x => (x.FirstName + " " + x.LastName).ToLower() == quote.SalesPerson.ToLower());

                if (salesPersonEntity == null)
                {
                    salesPersonEntity = new Data.Entities.SalesPerson
                    {
                        FirstName = quote.SalesPerson.Split(' ')[0],
                        LastName = quote.SalesPerson.Split(' ')[1],
                    };

                    sampleContext.SalesPeople.Add(salesPersonEntity);
                    sampleContext.SaveChanges();
                }

                int? marketingCategoryId = null;

                if (!string.IsNullOrEmpty(quote.MarketingCategory))
                {
                    var marketingCategoryEntity = sampleContext.MarketingCategories.FirstOrDefault(x => x.Name.ToLower() == quote.MarketingCategory.ToLower());

                    if (marketingCategoryEntity == null)
                    {
                        marketingCategoryEntity = new Data.Entities.MarketingCategory
                        {
                            Name = quote.MarketingCategory,
                        };

                        sampleContext.MarketingCategories.Add(marketingCategoryEntity);
                        sampleContext.SaveChanges();

                        marketingCategoryId = marketingCategoryEntity.Id;
                    }
                }

                var customerEntity = sampleContext.Customers
                    .Include(x => x.MarketingCategory)
                    .FirstOrDefault(x =>
                        (
                            x.FirstName + " " + x.LastName).ToLower() == quote.Customer.ToLower() &&
                            x.City == quote.CustomerCity &&
                            x.State == quote.CustomerState &&
                            (
                                x.MarketingCategory == null ||
                                x.MarketingCategory.Name == quote.MarketingCategory
                            )
                        );

                if (customerEntity == null)
                {
                    customerEntity = new Data.Entities.Customer
                    {
                        FirstName = quote.Customer.Split(' ')[0],
                        LastName = quote.Customer.Split(' ')[1],
                        City = quote.CustomerCity,
                        State = quote.CustomerState,
                        MarketingCategoryId = marketingCategoryId
                    };

                    sampleContext.Customers.Add(customerEntity);
                    sampleContext.SaveChanges();
                }

                if (!IsExistingQuote(quote))
                {
                    var quoteEntity = new Data.Entities.Quote()
                    {
                        SentDate = quote.QuoteSentDate,
                        ProjectId = projectEntity.Id,
                        SalesPersonId = salesPersonEntity.Id,
                        CustomerId = customerEntity.Id
                    };

                    sampleContext.Quotes.Add(quoteEntity);
                    sampleContext.SaveChanges();
                }
            }
        }
    }

    public List<QuoteDto> GetQuotes()
    {
        return sampleContext.Quotes
            .Include(x => x.Project)
            .Include(x => x.SalesPerson)
            .Include(x => x.Customer)
                .ThenInclude(x => x.MarketingCategory)
            .OrderByDescending(x => x.SentDate)
            .ThenBy(x => x.SalesPerson.FirstName)
            .Select(x => x.ToDto())
            .ToList();
    }

    private bool IsExistingQuote(QuoteDto quote)
    {
        return sampleContext.Quotes.Any(x =>
                x.SentDate == quote.QuoteSentDate &&
                x.Project.Name == quote.ProjectName &&
               (x.SalesPerson.FirstName + " " + x.SalesPerson.LastName).ToLower() == quote.SalesPerson.ToLower());
    }
}
