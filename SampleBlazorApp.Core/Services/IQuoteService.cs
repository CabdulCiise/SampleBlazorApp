using SampleBlazorApp.Core.DataTransferObjects;

namespace SampleBlazorApp.Core.Services;

public interface IQuoteService
{
    List<QuoteDto> GetQuotes();
    void AddQuotes(List<QuoteDto> quotes);
}
