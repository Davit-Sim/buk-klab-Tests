using buk_klab_Tests.Tests.Pages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

public static class DependencyContainer
{
    public static ServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();
                
        services.AddSingleton<IPlaywright>(Playwright.CreateAsync().Result);
        services.AddSingleton(async serviceProvider =>
        {
            var playwright = serviceProvider.GetRequiredService<IPlaywright>();
            return await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        });


        services.AddScoped(async serviceProvider =>
        {
            var browser = await serviceProvider.GetRequiredService<Task<IBrowser>>();
            return await browser.NewPageAsync();
        });

        services.AddTransient<HomePage>();
        services.AddTransient<SignInPage>();
        services.AddTransient<BooksPage>();
        services.AddTransient<AboutPage>();
        services.AddTransient<MembersPage>();


        return services.BuildServiceProvider();
    }
}
