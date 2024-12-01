using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace buk_klab_Tests.Pages;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class LandingPage : PageTest
{
    [SetUp]
    public async Task Setup()
    {
        await Page.GotoAsync("http://localhost:5173/");

    }

    [Test]
    public async Task HasTitle()
    {
        await Expect(Page).ToHaveTitleAsync(new Regex("buk klab"));
        string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "Screenshot" + currentTime + ".jpg"
        });
        //await Expect(Page.Locator(selector: "text='join buk klab'")).ToBeVisibleAsync();


    }
    [Test]
    public async Task HasTitle2()
    {
        await Expect(Page).ToHaveTitleAsync(new Regex("buk klab"));
        string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "Screenshot" + currentTime + ".jpg"
        });
        await Expect(Page.Locator("section").Filter(new() { HasText = "Welcome tobuk klabbuk klab is" }).GetByRole(AriaRole.Link)).ToBeVisibleAsync();


    }

    [Test]
    public async Task ObrazekClena()
    {
        await Expect(Page).ToHaveTitleAsync(new Regex("buk klab"));
        await Page.GetByRole(AriaRole.Link, new() { Name = "members" }).ClickAsync();
        await Page.Locator("img").ClickAsync();


    }

}