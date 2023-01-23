using Microsoft.Playwright;
using TrelloApi.Models;

namespace TrelloApi
{
    internal class TrelloApiDriver
    {
        private readonly string? TRELLO_API_KEY;
        private readonly string? TRELLO_TOKEN;
        private readonly string TRELLO_AUTHORIZATION_PARAMS;

        public IAPIRequestContext RequestContext;

        public TrelloApiDriver()
        {
            TRELLO_TOKEN = Environment.GetEnvironmentVariable("TRELLO_TOKEN");
            TRELLO_API_KEY = Environment.GetEnvironmentVariable("TRELLO_API_KEY");

            if(TRELLO_TOKEN is null || (TRELLO_API_KEY is null))
            {
                throw new NullReferenceException("TRELLO_TOKEN and TRELLO_API_KEY environment variables have to be set!");
            }

            TRELLO_AUTHORIZATION_PARAMS = $"key={TRELLO_API_KEY}&token={TRELLO_TOKEN}";
            RequestContext = InitializeApiDriver().Result;
        }

        public async Task<IAPIRequestContext> InitializeApiDriver()
        {
            var playwright = await Playwright.CreateAsync();
            return await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
            {
                BaseURL = "https://api.trello.com/1/"
            });
        }

        public async Task<TrelloApiResponse<List<BoardResponse>>> GetAllBoards()
        {
            var response = await RequestContext.GetAsync($"members/me/boards?fields=name&{TRELLO_AUTHORIZATION_PARAMS}");
            var trelloApiResponse = new TrelloApiResponse<List<BoardResponse>>
            {
                StatusCode = response.Status,
                Data = response.Ok ? await response.JsonAsync<List<BoardResponse>>() : null
            };

            return trelloApiResponse;
        }

        public async Task<TrelloApiResponse<BoardResponse>> GetBoardById(string boardId)
        {
            var response = await RequestContext.GetAsync($"boards/{boardId}?fields=name,desc,url,shortUrl&lists=all&{TRELLO_AUTHORIZATION_PARAMS}");
            var trelloApiResponse = new TrelloApiResponse<BoardResponse>
            {
                StatusCode = response.Status,
                Data = response.Ok ? await response.JsonAsync<BoardResponse>() : null
            };

            return trelloApiResponse;
        }

        public async Task<TrelloApiResponse<BoardResponse>> CreateBoard(string boardName, string description = "")
        {
            var response = await RequestContext.PostAsync($"boards?name={boardName}&desc={description}&{TRELLO_AUTHORIZATION_PARAMS}");

            var trelloApiResponse = new TrelloApiResponse<BoardResponse>
            {
                StatusCode = response.Status,
                Data = response.Ok ? await response.JsonAsync<BoardResponse>() : null
            };

            return trelloApiResponse;
        }

        public async Task<IAPIResponse> DeleteBoard(string boardId)
        {
            var response = await RequestContext.DeleteAsync($"boards/{boardId}?&{TRELLO_AUTHORIZATION_PARAMS}");
            return response;
        }
    }
}
