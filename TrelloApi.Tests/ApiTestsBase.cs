namespace TrelloApi.Tests
{
    abstract class ApiTestsBase
    {
        protected TrelloApiDriver apiDriver;

        [SetUp]
        public void Setup()
        {
            apiDriver = new TrelloApiDriver();
        }
    }
}
