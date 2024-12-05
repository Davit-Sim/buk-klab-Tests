using Microsoft.Extensions.Configuration;
using buk_klab_Tests.Tests.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

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

        // Register page objects
        services.AddTransient<HomePage>();
        services.AddTransient<SignInPage>();
        services.AddTransient<BooksPage>();
        services.AddTransient<AboutPage>();
        services.AddTransient<MembersPage>();

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
