namespace TrelloApi.Tests
{
    internal class BoardsApiTest : ApiTestsBase
    {
        [Test]
        public async Task GetAllBoards()
        {
            var response = await apiDriver.GetAllBoards();
            response.Data.Count.Should().BeGreaterThan(0);
            response.Data.All(x => x is not null).Should().BeTrue();
        }

        [Test]
        public async Task GetBordById()
        {
            var response = await apiDriver.GetBoardById("63c85a48a476b00271d2f4b3");
            response.Data.Id.Should().NotBeEmpty();
        }

        [Test]
        public async Task CreateNewBoard()
        {
            var response = await apiDriver.CreateBoard("Board created by playwright");
            response.Should().NotBeNull();
            response.Data.Id.Should().NotBeEmpty();
        }

        [Test]
        public async Task DeleteBoard()
        {
            var allBoards = await apiDriver.GetAllBoards();
            var boardId = allBoards.Data[^1].Id;

            var response = await apiDriver.DeleteBoard(boardId);
            response.Should().NotBeNull();
            response.Ok.Should().BeTrue();
        }
    }
}