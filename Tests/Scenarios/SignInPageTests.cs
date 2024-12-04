using Microsoft.Playwright;
namespace buk_klab_Tests.Tests.Pages;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class SignInPageTests
{
    private IPage _page;
    private readonly ILocator _locator;


    public SignInPageTests(IPage page)
    {
        _page = page;
    }
}