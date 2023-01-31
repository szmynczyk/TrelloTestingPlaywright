namespace TrelloApi.Tests
{
    internal class CardsApiTests : ApiTestsBase
    {
        [Test]
        public async Task CreateCardOnBoardList()
        {
            var response = await apiDriver.CreateCardOnBoardsList("Test board", "To Do", "Example card");
            response.Data.Should().NotBeNull();
        }
    }
}
