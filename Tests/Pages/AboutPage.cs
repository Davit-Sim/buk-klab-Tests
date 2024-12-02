using Microsoft.Playwright;

namespace buk_klab_Tests.Tests.Pages;
public class AboutPage
{
    private IPage _page;

    public AboutPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateToLoginPageAsync()
    {
        await _page.GotoAsync("http://localhost:5173/about");
    }
}