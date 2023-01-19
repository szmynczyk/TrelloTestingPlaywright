using Microsoft.Playwright;
using NUnit.Framework;
using System.Security.Policy;
using System.Text.Json;

namespace TrelloTestingPlaywright.Drivers
{
    public class TrelloApiDriver
    {
        const string TRELLO_API_KEY = "94dccbbfc9f2ff50c10b8066e2f01122";
        const string TRELLO_TOKEN = "ATTAc9d224eddb552c5efadb500024fcdc15f5137bf2cbfe5c6d8aedf73bfa2236ab53DF829F";

        IPlaywright _playwright;
        public IAPIRequestContext _requestContext;
        async Task<JsonElement?> GetBoards()
        {
            _playwright = await Playwright.CreateAsync();
            _requestContext = await _playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
            {
                 BaseURL = "https://api.trello.com/1/"
            });

            var response = await _requestContext.GetAsync($"members/me/boards?fields=name,url&key={TRELLO_API_KEY}&token={TRELLO_TOKEN}");
            return await response.JsonAsync();
        }
    }
}
