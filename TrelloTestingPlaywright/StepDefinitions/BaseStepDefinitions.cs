using TrelloTestingPlaywright.Drivers;

namespace TrelloTestingPlaywright.StepDefinitions
{
    internal class BaseStepDefinitions
    {
        protected BasePlaywright BasePlaywrightDriver;

        public BaseStepDefinitions(BasePlaywright basePlaywrightDriver)
        {
            BasePlaywrightDriver = basePlaywrightDriver;
        }
    }
}
