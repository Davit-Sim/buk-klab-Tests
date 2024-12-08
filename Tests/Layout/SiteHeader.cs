using Microsoft.Playwright;

namespace buk_klab_Tests.Tests.Layout
{
    public class SiteHeader
    {
        private readonly IPage _page;

        // Header locators
        private readonly ILocator _bookKlabTitle;
        private readonly ILocator _booksInHeader;
        private readonly ILocator _membersInHeader;
        private readonly ILocator _aboutInHeader;
        private readonly ILocator _signInInInHeader;
        private readonly ILocator _joinBookKlabButtonInHeader;

        public SiteHeader(IPage page)
        {
            _page = page ?? throw new ArgumentNullException(nameof(page));

            _bookKlabTitle = _page.Locator("section:has-text('buk klab') a");
            _booksInHeader = _page.Locator("a:has-text('books')");
            _membersInHeader = _page.Locator("a:has-text('members')");
            _aboutInHeader = _page.Locator("a:has-text('about')");
            _signInInInHeader = _page.Locator("a:has-text('sign in')");
            _joinBookKlabButtonInHeader = _page.Locator("ul li a:has-text('join buk klab')");
        }

        public async Task<bool> AreHeaderElemnentsVisible()
        {
            var locators = new List<ILocator>
            {
                _bookKlabTitle,
                _booksInHeader,
                _membersInHeader,
                _aboutInHeader,
                _signInInInHeader,
                _joinBookKlabButtonInHeader
            };

            foreach (var locator in locators)
            {
                if (!await locator.IsVisibleAsync())
                {
                    return false;
                }
            }
            return true;
        }

        public async Task ClickBooks() => await _booksInHeader.ClickAsync();
        public async Task ClickMembers() => await _membersInHeader.ClickAsync();
        public async Task ClickAbout() => await _aboutInHeader.ClickAsync();
        public async Task ClickSignIn() => await _signInInInHeader.ClickAsync();
        public async Task ClickJoin() => await _joinBookKlabButtonInHeader.ClickAsync();
    }
}
