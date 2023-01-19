using TrelloTestingPlaywright.Drivers;
using TrelloTestingPlaywright.Pages;

namespace TrelloTestingPlaywright.StepDefinitions
{
    [Binding]
    internal sealed class CreateNewTrelloBoardStepDefinitions
    {
        BasePlaywrightDriver _driver;
        MainPage _mainPage;
        CreateBoardDialog _createBoardDialog;
        string newBoardName;

        public CreateNewTrelloBoardStepDefinitions(BasePlaywrightDriver driver)
        {
            _driver = driver;
            _mainPage = new MainPage(_driver.Page);
            _createBoardDialog = new CreateBoardDialog(_driver.Page);
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
            newBoardName = await _createBoardDialog.FillBoardName(boardTitle);
            await _createBoardDialog.ClickCreateButton();
        }

        [Then(@"new board with name ""([^""]*)"" is visible on main page")]
        public async Task ThenNewBoardWithNameIsVisibleOnMainPage(string boardName)
        {
            await _driver.Page.GotoAsync("https://trello.com/u/szmynczyk_test/boards");
        }
    }
}
