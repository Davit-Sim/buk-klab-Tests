using Microsoft.Playwright;
namespace buk_klab_Tests.Tests.Pages;

public class BooksPage
{
    private IPage _page;

    public BooksPage(IPage page)
    {
        _page = page;
    }

    public async Task NavigateToBooksPageAsync()
    {
        await _page.GotoAsync("http://localhost:5173/books");
    }
}