using System;
using System.IO;
using System.Linq;
using SpecFlow.Reporting;
using SpecFlow.Reporting.Json;
using SpecFlow.Reporting.Text;
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
            if (false)
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
            if (false)
            {
                Reporters.Add(new JsonReporter());

                Reporters.FinishedReport += (sender, args) =>
                {
                    var reporter = args.Reporter as JsonReporter;
                    if (reporter != null)
                    {
                        reporter.WriteToFile(@"doc\data.json");
                    }
                };
            }

            if (true)
            {
                Reporters.Add(new PlainTextReporter());

                Reporters.FinishedReport += (sender, args) =>
                {
                    var reporter = args.Reporter as PlainTextReporter;
                    if (reporter != null)
                    {
                        var outputFile = @"doc\data.txt";
                        File.Delete(outputFile);
                        reporter.WriteToFile(outputFile);
                    }
                };
            }
        }
    }
}