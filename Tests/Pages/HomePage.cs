using Microsoft.Playwright;
namespace buk_klab_Tests.Tests.Pages;

public class HomePage
{
    private IPage _page;

    //Locators
    private const string HeaderLocator = "header";

    public HomePage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateToLoginPageAsync()
    {
        await _page.GotoAsync("http://localhost:5173/");
    }

    public async Task<bool> IsHeaderVisibleAsync()
    {
        return await _page.IsVisibleAsync(HeaderLocator);
    }
}