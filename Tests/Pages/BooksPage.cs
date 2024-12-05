using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
namespace buk_klab_Tests.Tests.Pages;

public class BooksPage
{
    private IPage _page;
    private readonly string _baseUrl;

    public BooksPage(IPage page, IConfiguration configuration)
    {
        _page = page;
        _baseUrl = configuration.GetValue<string>("Playwright:BaseUrl");
    }


    public async Task NavigateToBooksPageAsync()
    {
        await _page.GotoAsync($"{_baseUrl}books");
    }
}