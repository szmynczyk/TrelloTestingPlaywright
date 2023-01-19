using Microsoft.Playwright;

namespace TrelloTestingPlaywright.Pages
{
    internal class HomePage
    {
        private readonly IPage _page;
        ILocator _btnLogin => _page.GetByTestId("bignav").GetByRole(AriaRole.Link, new() { Name = "Log in" });

        public HomePage(IPage page)
        {
            _page = page;
        }

        public async Task ClickLoginButton()
        {
            await _btnLogin.ClickAsync();
            await _page.WaitForURLAsync("**/login");
        }
    }
}
