using Microsoft.Playwright;
using TrelloApi.Models;

namespace TrelloApi.Clients
{
    internal class CardsClient
    {
        IAPIRequestContext _requestContext;
        private readonly string TRELLO_AUTHORIZATION_PARAMS;

        private BoardsClient boardsClient;

        public CardsClient(IAPIRequestContext requestContext, string trelloAuthorizationParams)
        {
            _requestContext = requestContext;
            TRELLO_AUTHORIZATION_PARAMS = trelloAuthorizationParams;

            boardsClient = new BoardsClient(requestContext, TRELLO_AUTHORIZATION_PARAMS);
        }

        public async Task<TrelloApiResponse<CardResponse>> CreateCardOnBoardsList(string boardName, string listName, string cardName)
        {
            var list = await boardsClient.GetListFromBoard(boardName, listName);

            var response = await _requestContext.PostAsync($"cards?idList={list.Id}&name={cardName}&{TRELLO_AUTHORIZATION_PARAMS}");
            var trelloApiResponse = new TrelloApiResponse<CardResponse>
            {
                StatusCode = response.Status,
                Data = response.Ok ? await response.JsonAsync<CardResponse>() : null
            };

            return trelloApiResponse;
        }
    }
}
