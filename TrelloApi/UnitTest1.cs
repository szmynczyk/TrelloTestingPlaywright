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
    }
}