using Microsoft.Playwright;

namespace TrelloTestingPlaywright.Pages
{
    internal class MainPage
    {
        IPage _page;
        ILocator _btnAddNewBoard => _page.Locator(".board-tile.mod-add");
        ILocator _boardTile(string boardName) => _page.GetByRole(AriaRole.Link, new () { NameString = boardName }).Last;

        ILocator _btnMainPage => _page.GetByRole(AriaRole.Link, new() { Name = "Back to home" });

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
            return await _boardTile(boardName).IsVisibleAsync();
        }

        public async Task ClickBackToHomeButton()
        {
            await _btnMainPage.ClickAsync();
        }
    }
}