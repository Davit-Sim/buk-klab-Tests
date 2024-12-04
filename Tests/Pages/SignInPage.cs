using Microsoft.Playwright;

public class SignInPage
{
    private readonly IPage _page;

    public SignInPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateToSignInPageAsync()
    {
        await _page.GotoAsync("http://localhost:5173/signin");
    }
}
