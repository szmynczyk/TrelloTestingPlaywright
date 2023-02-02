using TrelloApi;

namespace TrelloTestingPlaywright.Hooks
{
    [Binding]
    public class Hooks
    {
        ScenarioContext _scenarioContext;
        TrelloApiDriver _apiDriver;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _apiDriver = new TrelloApiDriver();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_scenarioContext.ContainsKey("NewBoardName"))
            {
                var newBoardName = _scenarioContext["NewBoardName"].ToString();
                var boardToDelete = _apiDriver.GetBoardByName(newBoardName).Result;
                var result = _apiDriver.DeleteBoard(boardToDelete.Id).Result;

                _scenarioContext.Remove("NewBoardName");
            }
        }
    }
}
