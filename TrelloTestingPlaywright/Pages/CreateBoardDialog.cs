using Microsoft.Playwright;

namespace TrelloTestingPlaywright.Pages
{
    internal class CreateBoardDialog
    {
        private readonly IPage _page;
        ILocator _inputBoardName => _page.GetByTestId("create-board-title-input");
        ILocator _btnCreateBoard => _page.GetByTestId("create-board-submit-button");
        ILocator _dialogCreateBoard => _page.Locator("[title='Create board']");
        public CreateBoardDialog(IPage page)
        {
            _page = page;
        }

        public async Task<bool> IsCreateNewBoarDialogDisplayed()
        {
            return await _dialogCreateBoard.IsVisibleAsync();
        }

        public async Task<string> FillBoardName(string name)
        {
            var newName = $"{name}{Guid.NewGuid()}";
            await _inputBoardName.FillAsync(newName);

            return newName;
        }

        public async Task ClickCreateButton()
        {
            await _btnCreateBoard.ClickAsync();
            await _btnCreateBoard.WaitForAsync( new LocatorWaitForOptions()
            {
                State = WaitForSelectorState.Hidden
            });

            await _page.WaitForURLAsync("**/b/**/some-test-board**");
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
        }
    }
}
