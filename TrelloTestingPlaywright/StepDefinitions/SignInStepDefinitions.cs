using TrelloTestingPlaywright.Drivers;
using TrelloTestingPlaywright.Pages;

namespace TrelloTestingPlaywright.StepDefinitions
{
    [Binding]
    internal class SignInStepDefinitions : BaseStepDefinitions
    {
        HomePage _homePage;
        LoginPage _loginPage;

        public SignInStepDefinitions(BasePlaywright basePlaywright) : base(basePlaywright)
        {
            _homePage = new HomePage(BasePlaywrightDriver.Page);
            _loginPage = new LoginPage(BasePlaywrightDriver.Page);
        }

        [Given(@"I go to main trello page")]
        public void GivenIGoToMainTrelloPage()
        {
            _homePage.GoTo();
        }

        [Given(@"I login with credentials ""([^""]*)"" ""([^""]*)""")]
        public async Task GivenILoginWithCredentials(string userName, string password)
        {
            await _homePage.ClickLoginButton();
            await _loginPage.EnterCredentialsAndLogin();
        }

        [Then(@"main page is displayed")]
        public void ThenMainPageIsDisplayed()
        {
            BasePlaywrightDriver.Page.Url.Should().Contain("/boards");
        }
    }
}
