using Microsoft.Playwright;
namespace buk_klab_Tests.Tests.Pages;

public class HomePage
{
    private IPage _page;

    private readonly ILocator _bookKlabTitle;
    private readonly ILocator _booksInHeader;
    private readonly ILocator _membersInHeader;
    private readonly ILocator _aboutInHeader;
    private readonly ILocator _signInInInHeader;
    private readonly ILocator _joinBookKlabButtonInHeader;
    private readonly ILocator _joinBookKlabButtonInBody;
    private readonly ILocator _textInMainBody;
    private readonly ILocator _howDoesItWork;
    private readonly ILocator _currentReading;

    public HomePage(IPage page)
    {
        _page = page;

        //Element locators
        _bookKlabTitle = page.Locator("section:has-text('buk klab') a");
        _booksInHeader = page.Locator("a:has-text('books')");
        _membersInHeader = page.Locator("a:has-text('members')");
        _aboutInHeader = page.Locator("a:has-text('about')");
        _signInInInHeader = page.Locator("a:has-text('sign in')");
        _joinBookKlabButtonInHeader = page.Locator("ul li a:has-text('join buk klab')");
        _joinBookKlabButtonInBody = page.Locator("section:has-text('Welcome tobuk klabbuk klab is')").Locator("a");
        _textInMainBody = page.Locator("section:has-text('Welcome tobuk klabbuk klab is')");
        _howDoesItWork = page.Locator("section:has-text('how does it work?')");
        _currentReading = page.Locator("section:has-text('what are we currently reading?')");
    }

    public async Task ClickJoinBookKlab() => await _joinBookKlabButtonInHeader.ClickAsync();

    public async Task<bool> AllElementsAreVisible()
    {
        var locators = new List<ILocator>
        {
            _bookKlabTitle,
            _booksInHeader,
            _membersInHeader,
            _aboutInHeader,
            _signInInInHeader,
            _joinBookKlabButtonInHeader,
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