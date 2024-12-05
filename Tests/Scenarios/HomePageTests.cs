namespace buk_klab_Tests.Pages;

public class HomePageTests : BasePage
{
    [Test]
    public async Task VerifyHomePageIntroductoryText()
    {
        await _homePage.IsIntroductoryTextAsync();
    }        
}