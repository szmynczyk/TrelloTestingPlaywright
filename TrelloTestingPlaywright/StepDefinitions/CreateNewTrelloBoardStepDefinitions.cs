using TrelloTestingPlaywright.Drivers;
using TrelloTestingPlaywright.Pages;
using TrelloTestingPlaywright.Pages.Components;

namespace TrelloTestingPlaywright.StepDefinitions
{
    [Binding]
    internal sealed class CreateNewTrelloBoardStepDefinitions : BaseStepDefinitions
    {
        MainPage _mainPage;
        CreateBoardDialog _createBoardDialog;
        MainBarComponent _mainBarComponent;
        private readonly ScenarioContext _scenarioContext;

        public CreateNewTrelloBoardStepDefinitions(BasePlaywright driver, ScenarioContext scenarioContext) : base(driver)
        {
            _mainPage = new MainPage(BasePlaywrightDriver.Page);
            _createBoardDialog = new CreateBoardDialog(BasePlaywrightDriver.Page);
            _mainBarComponent = new MainBarComponent(BasePlaywrightDriver.Page);
            _scenarioContext = scenarioContext;
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
            var boardName = await _createBoardDialog.FillBoardName(boardTitle);
            _scenarioContext.Add("NewBoardName", boardName);
            await _createBoardDialog.ClickCreateButton();
        }

        [Then(@"new board is visible on main page")]
        public async Task ThenNewBoardIsVisibleOnMainPage()
        {
            var newBoardName = _scenarioContext["NewBoardName"].ToString();
            await _mainBarComponent.ClickBackToHomeButton();
            (await _mainPage.IsBoardWithNameVisibleOnMainPage(newBoardName)).Should().BeTrue();
        }
    }
}
