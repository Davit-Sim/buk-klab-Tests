using Microsoft.Extensions.Configuration;
using buk_klab_Tests.Tests.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;
using buk_klab_Tests.Tests.Layout;

public static class DependencyContainer
{
    public static ServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();

        // Load configuration files with Development.json taking precedence
        var configuration = new ConfigurationBuilder()
             .AddJsonFile("Configuration/appsettings.json", optional: false, reloadOnChange: true)
             .AddJsonFile("Configuration/appsettings.Development.json", optional: true, reloadOnChange: true)
             .Build();

        services.AddSingleton<IConfiguration>(configuration);

        services.AddSingleton<IPlaywright>(provider =>
        {
            try
            {
                return InitializePlaywrightAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error during Playwright initialization: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        });

        services.AddSingleton(async serviceProvider =>
        {
            var config = serviceProvider.GetRequiredService<IConfiguration>();
            var headless = config.GetValue<bool>("Playwright:BrowserOptions:Headless");

            var playwright = serviceProvider.GetRequiredService<IPlaywright>();
            return await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = headless });
        });

        services.AddSingleton(async provider =>
        {
            var browser = await provider.GetRequiredService<Task<IBrowser>>();
            return await browser.NewPageAsync();
        });

        services.AddSingleton(provider =>
        {
            var pageTask = provider.GetRequiredService<Task<IPage>>();
            return pageTask.GetAwaiter().GetResult();
        });

        // Register page objects
        services.AddTransient<HomePage>(provider =>
        {
            var page = provider.GetRequiredService<Task<IPage>>().Result;
            var header = provider.GetRequiredService<SiteHeader>();
            return new HomePage(page, header);
        });

         services.AddTransient<SiteHeader>(provider =>
        {
            var page = provider.GetRequiredService<IPage>();
            return new SiteHeader(page);
        });

        services.AddTransient<BooksPage>(provider =>
        {
            var page = provider.GetRequiredService<Task<IPage>>().Result;
            var configuration = provider.GetRequiredService<IConfiguration>();
            return new BooksPage(page, configuration);
        });

        services.AddTransient<AboutPage>(provider =>
        {
            var page = provider.GetRequiredService<Task<IPage>>().Result;
            var configuration = provider.GetRequiredService<IConfiguration>();
            return new AboutPage(page, configuration);
        });

        services.AddTransient<MembersPage>(provider =>
        {
            var page = provider.GetRequiredService<Task<IPage>>().Result;
            var configuration = provider.GetRequiredService<IConfiguration>();
            return new MembersPage(page, configuration);
        });

        return services.BuildServiceProvider();
    }

    private static async Task<IPlaywright> InitializePlaywrightAsync()
    {
        try
        {
            return await Playwright.CreateAsync();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Playwright initialization failed: {ex.Message}\n{ex.StackTrace}");
            throw;
        }
    }
}
