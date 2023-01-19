using Microsoft.Playwright;

namespace TrelloTestingPlaywright.Pages
{
    internal class MainPage
    {
        IPage _page;
        ILocator _btnAddNewBoard => _page.Locator(".board-tile.mod-add");

        public MainPage(IPage page)
        {
            _page = page;
        }

        public async Task ClickCreateNewBoardButton()
        {
            await _btnAddNewBoard.ClickAsync();
        }

        public async Task<bool> IsBoardWithNameVisibleOnMainPage(string boardName)
        {
            return await _page.Locator("Your workspaces").IsVisibleAsync();
        }
    }
}
