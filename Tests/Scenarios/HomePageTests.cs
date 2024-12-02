using buk_klab_Tests.Tests.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using System.Threading.Tasks;

[TestFixture]
public class HomePageTests : BasePage
{
 

    [SetUp]
    public async Task SetUpHomePage()
    {        
        _homePage = ActivatorUtilities.CreateInstance<HomePage>(_serviceProvider, _page);
    }

    [Test]
    public async Task VerifyHomePageLoadsSuccessfully()
    {       
        await _homePage.NavigateToLoginPageAsync();        
        bool isTitleVisible = await _homePage.IsTitleVisibleAsync();
        Assert.IsTrue(isTitleVisible, "Header is not visible.");
    }
}
