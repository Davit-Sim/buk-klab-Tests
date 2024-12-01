using Microsoft.Playwright;
namespace buk_klab_Tests.Pages;

public class LandingPage
{
    private IPage _page;
    private readonly ILocator mainLogo;
    private readonly ILocator _logo;
    private readonly ILocator _navbar;
    private readonly ILocator _books;
    private readonly ILocator _members;
    private readonly ILocator _signIn;
    public LandingPage(IPage page)
    {
        _page = page;
    }

}