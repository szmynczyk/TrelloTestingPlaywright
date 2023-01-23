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
    }
}
