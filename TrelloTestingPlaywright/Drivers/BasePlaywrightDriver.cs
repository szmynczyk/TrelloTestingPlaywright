using Microsoft.Playwright;

namespace TrelloTestingPlaywright.Drivers
{
    internal class BasePlaywrightDriver : IDisposable
    {
        IBrowser _browser;
        IBrowserContext _browserContext;
        public IPage Page { get; init; }

        public BasePlaywrightDriver() => Page = InitializeDriver().Result;

        async Task<IPage> InitializeDriver()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Channel = "chrome"
            });
            _browserContext = await _browser.NewContextAsync();

            return await _browserContext.NewPageAsync();
        }

        public void Dispose()
        {
            _browserContext?.CloseAsync();
            _browser?.CloseAsync();
        }
    }
}
