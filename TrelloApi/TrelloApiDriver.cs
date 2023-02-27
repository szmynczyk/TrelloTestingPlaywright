using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using System.Configuration;
using TrelloApi.Clients;
using TrelloApi.Models;

namespace TrelloApi
{
    public class TrelloApiDriver
    {
        private readonly string? TRELLO_API_KEY;
        private readonly string? TRELLO_TOKEN;
        private readonly string TRELLO_AUTHORIZATION_PARAMS;
        private readonly IAPIRequestContext _requestContext;
        private BoardsClient boardsClient;
        private CardsClient cardsClient;

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
            cardsClient = new CardsClient(_requestContext, TRELLO_AUTHORIZATION_PARAMS);
        }

        public async Task<IAPIRequestContext> InitializeApiDriver()
        {
            var trelloApiConfig = LoadConfiguration();

            var playwright = await Playwright.CreateAsync();
            return await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
            {
                BaseURL = trelloApiConfig.BaseApiUrl
            });
        }

        private TrelloApiConfig LoadConfiguration()
        {
            var config = new ConfigurationBuilder()
                            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                            .AddJsonFile("config.json").Build();

            var section = config.GetSection(nameof(TrelloApiConfig));
            var trelloConfig = section.Get<TrelloApiConfig>();

            if (trelloConfig is null)
            {
                throw new ConfigurationErrorsException("Reading Trello Api configuration failed!");
            }

            return trelloConfig;
        }

        public async Task<TrelloApiResponse<List<BoardResponse>>> GetAllBoards() => await boardsClient.GetAllBoards();
        public async Task<TrelloApiResponse<BoardResponse>> GetBoardById(string boardId) => await boardsClient.GetBoardById(boardId);
        public async Task<BoardResponse> GetBoardByName(string boardName) => await boardsClient.GetBoardByName(boardName);
        public async Task<TrelloApiResponse<BoardResponse>> CreateBoard(string boardName) => await boardsClient.CreateBoard(boardName);
        public async Task<IAPIResponse> DeleteBoard(string boardId) => await boardsClient.DeleteBoard(boardId);
        public async Task<TrelloApiResponse<CardResponse>> CreateCardOnBoardsList(string boardName, string listName, string cardName) => await cardsClient.CreateCardOnBoardsList(boardName, listName, cardName);

    }
}