using TrelloTestingPlaywright.Drivers;
using TrelloTestingPlaywright.Pages;

namespace TrelloTestingPlaywright.StepDefinitions
{
    [Binding]
    internal class SignInStepDefinitions
    {
        BasePlaywrightDriver _driver;
        HomePage _homePage;
        LoginPage _loginPage;

        public SignInStepDefinitions(BasePlaywrightDriver driver)
        {
            _driver = driver;
            _homePage = new HomePage(_driver.Page);
            _loginPage = new LoginPage(_driver.Page);
        }

        [Given(@"I go to main trello page")]
        public void GivenIGoToMainTrelloPage()
        {
            _driver.Page.GotoAsync("https://trello.com/");
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
            _driver.Page.Url.Should().Contain("/boards");
        }


    }
}
