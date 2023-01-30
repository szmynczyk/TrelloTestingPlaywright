using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using System.Configuration;
using TrelloTestingPlaywright.Support;

namespace TrelloTestingPlaywright.Drivers
{
    internal class BasePlaywright : IDisposable
    {
        IBrowser _browser;
        IBrowserContext _browserContext;
        public IPage Page { get; init; }
        public TrelloConfig TrelloConfig { get; set; }

        public BasePlaywright()
        {
            Page = InitializeDriver().Result;
            TrelloConfig = LoadConfiguration();
        }

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

        private TrelloConfig LoadConfiguration()
        {
            var config = new ConfigurationBuilder()
                            .SetBasePath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Support"))
                            .AddJsonFile("config.json").Build();

            var section = config.GetSection(nameof(TrelloConfig));
            var trelloConfig = section.Get<TrelloConfig>();

            if(trelloConfig is null)
            {
                throw new ConfigurationErrorsException("Reading Trello configuration failed!");
            }

            return trelloConfig;
        }

        public void Dispose()
        {
            _browserContext?.CloseAsync();
            _browser?.CloseAsync();
        }
    }
}
