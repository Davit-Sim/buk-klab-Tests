using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;

public class MembersPage
{
    private IPage _page;
    private readonly string _baseUrl;


    public MembersPage(IPage page, IConfiguration configuration)
    {
        _page = page;
        _baseUrl = configuration.GetValue<string>("Playwright:BaseUrl");

    }

    public async Task NavigateToMembersPageAsync()
    {
        await _page.GotoAsync($"{_baseUrl}members");
    }
}