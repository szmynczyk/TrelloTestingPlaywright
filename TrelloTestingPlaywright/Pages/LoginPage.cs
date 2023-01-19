using Microsoft.Playwright;

namespace TrelloTestingPlaywright.Pages
{
    internal class LoginPage
    {
        private readonly IPage _page;
        ILocator _intputEmail => _page.GetByPlaceholder("Enter email");
        ILocator _btnContinue => _page.GetByRole(AriaRole.Button, new() { Name = "Continue" });
        ILocator _inputPassword => _page.GetByPlaceholder("Enter password");
        ILocator _btnLogin => _page.GetByRole(AriaRole.Button, new() { Name = "Log in" });
        public LoginPage(IPage page)
        {
            _page = page;
        }

        public async Task EnterCredentialsAndLogin()
        {
            await _intputEmail.FillAsync("szmynczyk.test@interia.pl");
            await _btnContinue.ClickAsync();
            await _inputPassword.FillAsync("Test1234!");

            await _btnLogin.ClickAsync();
            await _page.WaitForURLAsync("https://trello.com/");
        }
    }
}
