// ****************************************************************************
// <copyright file="RequestLoggingTests.cs" author="Nikolaos Kokkinos">
// Copyright © Nikolaos Kokkinos
// </copyright>
// ****************************************************************************
// <author>Nikolaos Kokkinos</author>
// <email>nik.kokkinos@windowslive.com</email>
// <date>28.01.2015</date>
// <project>SmartHomeNancyTests</project>
// <web>http://nikolaoskokkinos.wordpress.com/</web>
// ****************************************************************************
using Nancy;
using Nancy.Testing;
using NUnit.Framework;

using SmartHomeNancy;

namespace SmartHomeNancyTests
{
    [TestFixture]
    public class RequestLoggingTests
    {
        private Browser sut;
        private Bootstrapper bootStrapper;

        [SetUp]
        public void SetUp()
        {
            bootStrapper = new Bootstrapper();
            sut = new Browser(bootStrapper);

        }

        [TestCase("/", HttpStatusCode.OK)]
        [TestCase("/datapoints/", HttpStatusCode.OK)]
        [TestCase("/shouldnotbefound/", HttpStatusCode.NotFound)]
        public void Should_log_status_code_of_responses(string path, HttpStatusCode expectedStatusCode)
        {
            sut.Get(path);
            string expectedString = string.Format("Responding {0} to {1} {2}",
              expectedStatusCode, "GET", path);
            Assert.AreEqual(expectedString, bootStrapper.LogAllResponsesCodeString);

        }


        [TestCase("/")]
        [TestCase("/datapoints/")]
        public void Should_not_log_error_on_successful_request(string path)
        {
            sut.Get(path);

            Assert.AreEqual(string.Format("Handling request GET {0}", path),
                bootStrapper.LogAllRequestsString);

        }

      
    }
}
