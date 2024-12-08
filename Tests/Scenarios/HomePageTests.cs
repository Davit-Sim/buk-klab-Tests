using buk_klab_Tests.Tests.Layout;
using buk_klab_Tests.Tests.Pages;

namespace buk_klab_Tests.Pages;

public class HomePageTests : BasePage
{
    [Test]
    public async Task AllElementsLoadedOnHomePage()
    {
        HomePage homePage = new HomePage(page, new SiteHeader(page));

        Assert.IsTrue(await homePage.SiteHeader.AreHeaderElemnentsVisible(), "Header elements are not visible.");
        Assert.IsTrue(await homePage.AreHomePageElementsVisible(), "HomePage elements are not visible.");

        await homePage.SiteHeader.ClickBooks();
    }
}