
using Microsoft.Playwright;

public class SignInPage
{
    private readonly IPage _page;

    public SignInPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateToLoginPageAsync()
    {
        await _page.GotoAsync("http://localhost:5173/join");
    }

    public async Task LoginAsync(string username, string password)
    {
        await _page.FillAsync("#username", username);
        await _page.FillAsync("#password", password);
        await _page.ClickAsync("#loginButton");
    }
}
