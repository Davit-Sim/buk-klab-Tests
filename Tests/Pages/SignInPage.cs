using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;

public class SignInPage
{
    private readonly IPage _page;
    private readonly string _baseUrl;


    public SignInPage(IPage page, IConfiguration configuration)
    {
        _page = page;
        _baseUrl = configuration.GetValue<string>("Playwright:BaseUrl");
    }

    public async Task NavigateToSignInPageAsync()
    {
        await _page.GotoAsync($"{_baseUrl}signin");
    }
}
