using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
namespace buk_klab_Tests.Tests.Pages;

public class AboutPage
{
    private IPage _page;
    private readonly string _baseUrl;

    public AboutPage(IPage page, IConfiguration configuration)
    {
        _page = page;
        _baseUrl = configuration.GetValue<string>("Playwright:BaseUrl");
    }

    public async Task NavigateToAboutPageAsync()
    {
        await _page.GotoAsync($"{_baseUrl}about");
    }
}