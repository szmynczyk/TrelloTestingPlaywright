using FluentAssertions;

namespace TrelloApi
{
    public class Tests
    {
        TrelloApiDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new TrelloApiDriver();
        }

        [Test]
        public async Task GetAllBoards()
        {
            var response = await driver.GetAllBoards();
            response.Count.Should().BeGreaterThan(0);
        }

        [Test]
        public async Task GetBordById()
        {
            var response = await driver.GetBoardById("63c85a48a476b00271d2f4b3");
            response.Should().NotBeNull();
            response.Id.Should().NotBeEmpty();
        }

        [Test]
        public async Task CreateNewBoard()
        {
            var response = await driver.CreateBoard("Board created by playwright");
            response.Should().NotBeNull();
            response.Id.Should().NotBeEmpty();
        }

        [Test]
        public async Task DeleteBoard()
        {
            var allBoards = await driver.GetAllBoards();
            var boardId = allBoards[^1].Id;

            var response = await driver.DeleteBoard(boardId);
            response.Should().NotBeNull();
            response.Ok.Should().BeTrue();
        }
    }
}