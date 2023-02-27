using Microsoft.Playwright;

namespace TrelloTestingPlaywright.Pages.Components
{
    internal class MainBarComponent
    {
        IPage _page;
        ILocator _btnGoToMainPage => _page.GetByRole(AriaRole.Link, new PageGetByRoleOptions() { Name = "Back to home" });

        public MainBarComponent(IPage page)
        {
            _page = page;
        }

        public async Task ClickBackToHomeButton()
        {
            await _btnGoToMainPage.ClickAsync();
        }
    }
}
