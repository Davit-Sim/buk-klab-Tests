using buk_klab_Tests.Tests.Layout;
using buk_klab_Tests.Tests.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

public class BasePage
{
    protected ServiceProvider _serviceProvider;
    protected IBrowser _browser;
    protected IPage page;
    protected AboutPage _aboutPage;
    protected BooksPage _booksPage;
    protected HomePage _homePage;
    protected MembersPage _membersPage;
    protected SignInPage _signinPage;
    protected SiteHeader _header;

    [SetUp]
    public async Task Setup()
    {
        try
        {
            _serviceProvider = DependencyContainer.BuildServiceProvider();

            _browser = await _serviceProvider.GetRequiredService<Task<IBrowser>>();
            page = _serviceProvider.GetRequiredService<IPage>();

            _aboutPage = ActivatorUtilities.CreateInstance<AboutPage>(_serviceProvider, page);
            _booksPage = ActivatorUtilities.CreateInstance<BooksPage>(_serviceProvider, page);
            _homePage = ActivatorUtilities.CreateInstance<HomePage>(_serviceProvider, page);
            _membersPage = ActivatorUtilities.CreateInstance<MembersPage>(_serviceProvider, page);
            _signinPage = ActivatorUtilities.CreateInstance<SignInPage>(_serviceProvider, page);
            _header = ActivatorUtilities.CreateInstance<SiteHeader>(_serviceProvider, page);

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
        var config = _serviceProvider.GetRequiredService<IConfiguration>();
        var baseUrl = config.GetValue<string>("Playwright:BaseUrl");

        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            throw new InvalidOperationException("Base URL is not defined.");
        }

        try
        {
            await page.GotoAsync(baseUrl);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error navigating to base URL: {baseUrl}. Message: {ex.Message}");
            throw;
        }
    }
}
