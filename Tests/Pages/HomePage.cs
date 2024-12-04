using Microsoft.Playwright;
namespace buk_klab_Tests.Tests.Pages;

public class HomePage
{
    private IPage _page;
    private readonly ILocator _introductoryText;

    public HomePage(IPage page)
    {
        _page = page;
        _introductoryText = page.Locator(selector: "text=buk klab is a place where" +
            " you can read books, discuss them with others, and meet new friends.");

    }

    public async Task<bool> IsIntroductoryTextAsync()
    {
        return await _introductoryText.IsVisibleAsync();
    }
}