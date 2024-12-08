using Microsoft.Playwright;
namespace buk_klab_Tests.Tests.Pages;

public class HomePage
{
    private IPage _page;
    private Layout.SiteHeader _header;

    private readonly ILocator _joinBookKlabButtonInBody;
    private readonly ILocator _textInMainBody;
    private readonly ILocator _howDoesItWork;
    private readonly ILocator _currentReading;

    public HomePage(IPage page, Layout.SiteHeader header)
    {
        _page = page;
        _header = header;

        //Element locators
        _joinBookKlabButtonInBody = page.Locator("section:has-text('Welcome tobuk klabbuk klab is')").Locator("a");
        _textInMainBody = page.Locator("section:has-text('Welcome tobuk klabbuk klab is')");
        _howDoesItWork = page.Locator("section:has-text('how does it work?')");
        _currentReading = page.Locator("section:has-text('what are we currently reading?')");
    }

    public async Task ClickJoinBookKlab() => await _joinBookKlabButtonInBody.ClickAsync();
    public Layout.SiteHeader SiteHeader => _header;

    public async Task<bool> AreHomePageElementsVisible()
    {
        var locators = new List<ILocator>
        {
            _joinBookKlabButtonInBody,
            _textInMainBody,
            _howDoesItWork,
            _currentReading
        };

        foreach (var locator in locators)
        {
            if (!await locator.IsVisibleAsync())
            {
                return false;
            }
        }
        return true;
    }

}