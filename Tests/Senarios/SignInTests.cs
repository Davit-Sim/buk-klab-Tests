using Microsoft.Playwright;
namespace buk_klab_Tests.Tests.Pages;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class SignIn
{
    private IPage _page;
    private readonly ILocator _locator;


    public SignIn(IPage page)
    {
        _page = page;
    }
}