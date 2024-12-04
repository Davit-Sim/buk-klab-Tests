using buk_klab_Tests.Tests.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

public class BasePage
{
    protected ServiceProvider _serviceProvider;
    protected IBrowser _browser;
    protected SignInPage _signinPage;
    protected HomePage _homePage;
    private const string BaseUrl = "http://localhost:5173/";

    [SetUp]
    public async Task Setup()
    {
        try
        {
            _serviceProvider = DependencyContainer.BuildServiceProvider();

            _browser = await _serviceProvider.GetRequiredService<Task<IBrowser>>();
            var page = await _browser.NewPageAsync();

            _signinPage = ActivatorUtilities.CreateInstance<SignInPage>(_serviceProvider, page);
            _homePage = ActivatorUtilities.CreateInstance<HomePage>(_serviceProvider, page);

            await NavigateToHomePageUrlAsync(page);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during setup: {ex.Message}");
            throw;
        }
    }

    [TearDown]
    public async Task Teardown()
    {
        try
        {
            if (_browser != null)
            {
                await _browser.CloseAsync();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during teardown: {ex.Message}");
        }
        finally
        {
            _serviceProvider?.Dispose();
        }        
    }

    protected async Task NavigateToHomePageUrlAsync(IPage page)
    {
        if (string.IsNullOrWhiteSpace(BaseUrl))
        {
            throw new InvalidOperationException("Base URL is not defined.");
        }

        try
        {
            await page.GotoAsync(BaseUrl);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error navigating to base URL: {BaseUrl}. Message: {ex.Message}");
            throw;
        }
    }
}
