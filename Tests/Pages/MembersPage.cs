using Microsoft.Playwright;

public class MembersPage
{
    private IPage _page;

    public MembersPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateToMembersPageAsync()
    {
        await _page.GotoAsync("http://localhost:5173/members");
    }
}