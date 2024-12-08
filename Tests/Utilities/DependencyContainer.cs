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
        services.AddTransient<HomePage>(provider =>
        {
            var page = provider.GetRequiredService<Task<IPage>>().Result;
            return new HomePage(page);
        });

        services.AddTransient<SignInPage>(provider =>
        {
            var page = provider.GetRequiredService<Task<IPage>>().Result;
            var configuration = provider.GetRequiredService<IConfiguration>();
            return new SignInPage(page, configuration);
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
