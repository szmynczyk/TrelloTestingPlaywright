using Microsoft.Playwright;
using TrelloApi.Models;

namespace TrelloApi.Clients
{
    internal class BoardsClient
    {
        IAPIRequestContext _requestContext;
        private readonly string TRELLO_AUTHORIZATION_PARAMS;

        public BoardsClient(IAPIRequestContext requestContext, string trelloAuthorizationParams)
        {
            _requestContext = requestContext;
            TRELLO_AUTHORIZATION_PARAMS = trelloAuthorizationParams;
        }

        public async Task<BoardResponse> GetBoardByName(string boardName)
        {
            var boards = await GetAllBoards();
            var singleBoard = boards.Data?.FirstOrDefault(x => x.Name == boardName);
            return singleBoard;
        }

        public async Task<BoardListResponse> GetListFromBoard(string boardName, string listName)
        {
            var board = await GetBoardByName(boardName);
            var list = board?.Lists.FirstOrDefault(x => x.Name == listName);
            return list;
        }

        public async Task<TrelloApiResponse<CardResponse>> CreateCardOnBoardsList(string boardName, string listName, string cardName)
        {
            var list = await GetListFromBoard(boardName, listName);

            var response = await _requestContext.PostAsync($"cards?idList={list.Id}&name={cardName}&{TRELLO_AUTHORIZATION_PARAMS}");
            var trelloApiResponse = new TrelloApiResponse<CardResponse>
            {
                StatusCode = response.Status,
                Data = response.Ok ? await response.JsonAsync<CardResponse>() : null
            };

            return trelloApiResponse;
        }

        public async Task<TrelloApiResponse<List<BoardResponse>>> GetAllBoards()
        {
            var response = await _requestContext.GetAsync($"members/me/boards?name,url,desc&lists=open&{TRELLO_AUTHORIZATION_PARAMS}");
            var trelloApiResponse = new TrelloApiResponse<List<BoardResponse>>
            {
                StatusCode = response.Status,
                Data = response.Ok ? await response.JsonAsync<List<BoardResponse>>() : null
            };

            return trelloApiResponse;
        }

        public async Task<TrelloApiResponse<BoardResponse>> GetBoardById(string boardId)
        {
            var response = await _requestContext.GetAsync($"boards/{boardId}?fields=name,desc,url,shortUrl&lists=all&{TRELLO_AUTHORIZATION_PARAMS}");
            var trelloApiResponse = new TrelloApiResponse<BoardResponse>
            {
                StatusCode = response.Status,
                Data = response.Ok ? await response.JsonAsync<BoardResponse>() : null
            };

            return trelloApiResponse;
        }

        public async Task<TrelloApiResponse<BoardResponse>> CreateBoard(string boardName, string description = "")
        {
            var response = await _requestContext.PostAsync($"boards?name={boardName}&desc={description}&{TRELLO_AUTHORIZATION_PARAMS}");

            var trelloApiResponse = new TrelloApiResponse<BoardResponse>
            {
                StatusCode = response.Status,
                Data = response.Ok ? await response.JsonAsync<BoardResponse>() : null
            };

            return trelloApiResponse;
        }

        public async Task<IAPIResponse> DeleteBoard(string boardId)
        {
            var response = await _requestContext.DeleteAsync($"boards/{boardId}?&{TRELLO_AUTHORIZATION_PARAMS}");
            return response;
        }
    }
}
