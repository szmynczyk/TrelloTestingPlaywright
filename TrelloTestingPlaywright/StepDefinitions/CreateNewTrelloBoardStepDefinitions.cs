using TrelloTestingPlaywright.Drivers;
using TrelloTestingPlaywright.Pages;

namespace TrelloTestingPlaywright.StepDefinitions
{
    [Binding]
    internal sealed class CreateNewTrelloBoardStepDefinitions : BaseStepDefinitions
    {
        MainPage _mainPage;
        CreateBoardDialog _createBoardDialog;

        public CreateNewTrelloBoardStepDefinitions(BasePlaywright driver) : base(driver)
        {
            _mainPage = new MainPage(BasePlaywrightDriver.Page);
            _createBoardDialog = new CreateBoardDialog(BasePlaywrightDriver.Page);
        }

        [When(@"I click on Create new board element")]
        public async Task WhenIClickOnCreateNewBoardElement()
        {
            await _mainPage.ClickCreateNewBoardButton();
        }

        [Then(@"new board dialog is displayed")]
        public async Task ThenNewBoardDialogIsDisplayed()
        {
            (await _createBoardDialog.IsCreateNewBoarDialogDisplayed()).Should().Be(true);
        }

        [When(@"create new board with title ""([^""]*)""")]
        public async Task WhenCreateNewBoardWithTitle(string boardTitle)
        {
            await _createBoardDialog.FillBoardName(boardTitle);
            await _createBoardDialog.ClickCreateButton();
        }

        [Then(@"new board with name ""([^""]*)"" is visible on main page")]
        public async Task ThenNewBoardWithNameIsVisibleOnMainPage()
        {
            await BasePlaywrightDriver.Page.GotoAsync("https://trello.com/u/szmynczyk_test/boards");
        }
    }
}
