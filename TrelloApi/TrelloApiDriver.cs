using Microsoft.Playwright;
using System.Text.Json;
using TrelloApi.Models;

namespace TrelloApi
{
    internal class TrelloApiDriver
    {
        const string TRELLO_API_KEY = "94dccbbfc9f2ff50c10b8066e2f01122";
        const string TRELLO_TOKEN = "ATTAc9d224eddb552c5efadb500024fcdc15f5137bf2cbfe5c6d8aedf73bfa2236ab53DF829F";
        const string TRELLO_AUTHORIZATION_PARAMS = $"key={TRELLO_API_KEY}&token={TRELLO_TOKEN}";

        public IAPIRequestContext RequestContext;

        public TrelloApiDriver() => RequestContext = InitializeApiDriver().Result;

        public async Task<IAPIRequestContext> InitializeApiDriver()
        {
            var playwright = await Playwright.CreateAsync();
            return await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
            {
                BaseURL = "https://api.trello.com/1/"
            });
        }

        public async Task<List<BoardResponse>> GetAllBoards()
        {
            var response = await RequestContext.GetAsync($"members/me/boards?fields=name&{TRELLO_AUTHORIZATION_PARAMS}");
            return await response.JsonAsync<List<BoardResponse>>();
        }

        public async Task<BoardResponse> GetBoardById(string boardId)
        {
            var response = await RequestContext.GetAsync($"boards/{boardId}?fields=name,desc,url,shortUrl&lists=all&{TRELLO_AUTHORIZATION_PARAMS}");
            return await response.JsonAsync<BoardResponse>();
        }

        public async Task<BoardResponse> CreateBoard(string boardName, string description = "")
        {
            var response = await RequestContext.PostAsync($"boards?name={boardName}&desc={description}&{TRELLO_AUTHORIZATION_PARAMS}");
            return await response.JsonAsync<BoardResponse>();

        }

        public async Task<IAPIResponse> DeleteBoard(string boardId)
        {
            var response = await RequestContext.DeleteAsync($"boards/{boardId}?&{TRELLO_AUTHORIZATION_PARAMS}");
            return response;
        }
    }
}
