using System.Collections.Generic;
using System.IO;
using System.Linq;
using KS.FiksProtokollValidator.WebAPI.Models;
using Newtonsoft.Json.Linq;

namespace KS.FiksProtokollValidator.WebAPI.Data
{
    public class TestSeeder : ITestSeeder
    {
        private readonly FiksIOMessageDBContext _context;

        public TestSeeder(FiksIOMessageDBContext context)
        {
            _context = context;

            Seed();
        }

        private void Seed()
        {
            var tests = new DirectoryInfo(@"../KS.FiksProtokollValidator.TestCases/Tests/");

            foreach (var testDirectory in tests.GetDirectories())
            {
                var testCriteriaJson = File.ReadAllText(Path.Combine(testDirectory.FullName, "testCriteria.json"));
                var testCriteria = JObject.Parse(testCriteriaJson);

                var testName = testDirectory.Name;

                if (TestExistInDatabase(testName))
                    continue;

                var test = new TestCase
                {
                    TestName = testName,
                    MessageType = (string) testCriteria["messageType"],
                    PayloadFileName = "arkivmelding.xml",
                    FiksResponseTests = new List<FiksResponseTest>()
                };

                var attachmentDirectory = Path.Combine(testDirectory.FullName, "Attachments");
                if (Directory.Exists(attachmentDirectory))
                {
                    var payloadAttachmentFileNames = "";

                    foreach (var fileInfo in new DirectoryInfo(attachmentDirectory)
                        .GetFiles())
                    {
                        payloadAttachmentFileNames += fileInfo.Name + ";";
                    }

                    test.PayloadAttachmentFileNames = payloadAttachmentFileNames.TrimEnd(';');
                }

                foreach (var queryWithExpectedValue in testCriteria["queriesWithExpectedValues"])
                {
                    var fiksResponseTest = new FiksResponseTest
                    {
                        PayloadQuery = (string) queryWithExpectedValue["payloadQuery"],
                        ExpectedValue = (string) queryWithExpectedValue["expectedValue"],
                        ValueType = (SearchValueType) (int) queryWithExpectedValue["valueType"]
                    };

                    test.FiksResponseTests.Add(fiksResponseTest);
                }

                _context.TestCases.Add(test);
                _context.SaveChanges();
            }
        }

        private bool TestExistInDatabase(string testName)
        {
            return _context.TestCases.Any(a => a.TestName.Equals(testName));
        }
    }
}
