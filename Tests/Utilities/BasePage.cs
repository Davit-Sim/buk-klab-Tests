using buk_klab_Tests.Tests.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using NUnit.Framework;

public class BasePage
{
    protected ServiceProvider _serviceProvider;
    protected IBrowser _browser;
    protected SignInPage _signinPage;
    protected HomePage _homePage;

    [SetUp]
    public async Task Setup()
    {
        _serviceProvider = DependencyContainer.BuildServiceProvider();

        _browser = await _serviceProvider.GetRequiredService<Task<IBrowser>>();
        var page = await _browser.NewPageAsync();
               
        _signinPage = ActivatorUtilities.CreateInstance<SignInPage>(_serviceProvider, page);
        _homePage = ActivatorUtilities.CreateInstance<HomePage>(_serviceProvider, page);
    }

    [TearDown]
    public async Task Teardown()
    {
        if (_browser != null)
        {
            await _browser.CloseAsync();
        }

        _serviceProvider?.Dispose();
    }
}
