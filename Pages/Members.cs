using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace buk_klab_Tests.Pages;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Members : PageTest
{
    [SetUp]
    public async Task Setup()
    {
        await Page.GotoAsync("http://localhost:5173/");

    }   
}