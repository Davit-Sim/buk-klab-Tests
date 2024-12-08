using buk_klab_Tests.Tests.Pages;

namespace buk_klab_Tests.Pages;

public class HomePageTests : BasePage
{
    [Test]
    public async Task AllElementsLoadedOnHomePage()
    {
        HomePage homePage = new HomePage(page);
        var elemtsExists = await homePage.AllElementsAreVisible();
        Assert.IsTrue(elemtsExists);
        await homePage.ClickJoinBookKlab();
    }
}