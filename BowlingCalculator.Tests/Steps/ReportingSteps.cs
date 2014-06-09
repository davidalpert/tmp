using System.Linq;
using SpecFlow.Reporting;
using SpecFlow.Reporting.WebApp;
using TechTalk.SpecFlow;

namespace BowlingCalculator.Tests.Steps
{
    [Binding]
    public class ReportingSteps : ReportingStepDefinitions
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var webApp = new WebAppReporter();
            webApp.Settings.Title = "Bowling Calculator Features";
        
            Reporters.Add(webApp);

            Reporters.FinishedReport += (sender, args) =>
            {
                var reporter = args.Reporter as WebAppReporter;
                if (reporter != null)
                {
                    reporter.WriteToFolder("doc", true);
                }
            };
        }
    }
}