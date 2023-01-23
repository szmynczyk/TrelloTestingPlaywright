using Microsoft.Playwright;
using TrelloApi.Clients;
using TrelloApi.Models;

namespace TrelloApi
{
    internal class TrelloApiDriver
    {
        private readonly string? TRELLO_API_KEY;
        private readonly string? TRELLO_TOKEN;
        private readonly string TRELLO_AUTHORIZATION_PARAMS;
        private readonly IAPIRequestContext _requestContext;
        private BoardsClient boardsClient;

        public TrelloApiDriver()
        {
            TRELLO_TOKEN = Environment.GetEnvironmentVariable("TRELLO_TOKEN");
            TRELLO_API_KEY = Environment.GetEnvironmentVariable("TRELLO_API_KEY");

            if (TRELLO_TOKEN is null || (TRELLO_API_KEY is null))
            {
                throw new NullReferenceException("TRELLO_TOKEN and TRELLO_API_KEY environment variables have to be set!");
            }

            TRELLO_AUTHORIZATION_PARAMS = $"key={TRELLO_API_KEY}&token={TRELLO_TOKEN}";

            _requestContext = InitializeApiDriver().Result;
            boardsClient = new BoardsClient(_requestContext, TRELLO_AUTHORIZATION_PARAMS);
        }

        public async Task<IAPIRequestContext> InitializeApiDriver()
        {
            var playwright = await Playwright.CreateAsync();
            return await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
            {
                BaseURL = "https://api.trello.com/1/"
            });
        }

        public async Task<TrelloApiResponse<List<BoardResponse>>> GetAllBoards() => await boardsClient.GetAllBoards();
        public async Task<TrelloApiResponse<BoardResponse>> GetBoardById(string boardId) => await boardsClient.GetBoardById(boardId);
        public async Task<TrelloApiResponse<BoardResponse>> CreateBoard(string boardName, string description = "") => await boardsClient.CreateBoard(boardName, description);
        public async Task<IAPIResponse> DeleteBoard(string boardId) => await boardsClient.DeleteBoard(boardId);
        public async Task<TrelloApiResponse<CardResponse>> CreateCardOnBoardsList(string boardName, string listName, string cardName) => await boardsClient.CreateCardOnBoardsList(boardName, listName, cardName);

    }
}