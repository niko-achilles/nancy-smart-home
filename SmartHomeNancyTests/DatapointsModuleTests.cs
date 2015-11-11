// ****************************************************************************
// <copyright file="DatapointsModuleTests.cs" author="Nikolaos Kokkinos">
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
using SmartHomeNancy.Model;

namespace SmartHomeNancyTests
{
    [TestFixture]
    public class DatapointsModuleTests
    {
        private Browser sut;
        private Datapoint aDatapoint;
        private Datapoint aDuplicateDatapoint;
        private Datapoint aStateChangedDatapoint;

        [SetUp]
        public void SetUp()
        {
            var dataStore = new InMemoryDataStore();
            dataStore.Store.Clear();


            sut = new Browser(new Bootstrapper());

            aDatapoint = new Datapoint
            {
                Name = "Switch;OnOff"
            };

            aDuplicateDatapoint = new Datapoint
            {
                Name = "Switch;OnOff"
            };

            aStateChangedDatapoint = new Datapoint
            {
                Name = "Switch;OnOff",
                Value = "0"
            };

        }


        [Test]
        public void Should_return_empty_list_on_get_when_no_datapoints_have_been_posted()
        {
            var actual = sut.Get("/datapoints/", 
                context=>context.Accept("application/json"));

            Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
            Assert.IsEmpty(actual.Body.DeserializeJson<Datapoint[]>());
        }

        [Test]
        public void Should_return_201_created_and_posted_datapoint_as_json_when_a_datapoint_is_posted_as_json()
        {
            var actualResponse = sut.Post("/datapoints/",
                context =>
                {
                    context.JsonBody(aDatapoint);
                    context.Accept("application/json");
                });

            var actualBody = actualResponse.Body.DeserializeJson<Datapoint>();

            Assert.AreEqual(HttpStatusCode.Created, actualResponse.StatusCode);
            AssertAreSame(aDatapoint, actualBody);
        }

        private void AssertAreSame(Datapoint expected, Datapoint actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Value, actual.Value);
        }

        [Test]
        public void Should_return_201_created_and_posted_datapoint_as_xml_when_a_datapoint_is_posted_as_xml()
        {
            var actualResponse = sut.Post("/datapoints/",
                context =>
                {
                    context.XMLBody(aDatapoint);
                    context.Accept("application/xml");
                });

            var actualBody = actualResponse.Body.DeserializeXml<Datapoint>();

            Assert.AreEqual(HttpStatusCode.Created, actualResponse.StatusCode);
            AssertAreSame(aDatapoint, actualBody);
        }

        [Test]
        public void Should_not_accept_posting_to_with_duplicate_datapoint_name()
        {
            var actualResponse = sut.Post("/datapoints/",
                context =>
                {
                    context.JsonBody(aDatapoint);
                    context.Accept("application/json");
                })
                .Then
                .Post("/datapoints/", context => 
                    context.JsonBody(aDuplicateDatapoint));

            Assert.AreEqual(HttpStatusCode.NotAcceptable, actualResponse.StatusCode);

        }

        [Test]
        public void Should_be_able_to_get_posted_datapoint()
        {
            var actualResponse = sut.Post("/datapoints/",
                context =>
                {
                    context.JsonBody(aDatapoint);
                    context.Accept("application/json");
                })
                .Then
                
                .Get("/datapoints/", 
                context => context.Accept("application/json"));

            var actualBody = actualResponse.Body.DeserializeJson<Datapoint[]>();
            Assert.AreEqual(1, actualBody.Length);
            AssertAreSame(aDatapoint, actualBody[0]);
        }


        [TestCase("Switch;OnOff")]
        public void Should_be_able_to_update_datapoint_state_with_put_as_json(string datapointName)
        {
            var actualResponse = sut.Post("/datapoints/",
                context =>
                {
                    context.JsonBody(aDatapoint);
                    context.Accept("application/json");
                })
                .Then
                .Put("/datapoints/"+datapointName,
                context =>
                {
                    context.JsonBody(aStateChangedDatapoint);
                    context.Accept("application/json");
                })
                .Then
                .Get("/datapoints/", context => context.Accept("application/json"));

            var actualBody = actualResponse.Body.DeserializeJson<Datapoint[]>();
            Assert.AreEqual(1, actualBody.Length);
            AssertAreSame(aStateChangedDatapoint, actualBody[0]);
        }

        [Test]
        public void hould_be_able_to_update_datapoint_state_with_put_as_xml()
        {
            var actualResponse = sut.Post("/datapoints/",
                context =>
                {
                    context.XMLBody(aDatapoint);
                    context.Accept("application/xml");
                })
                .Then
                .Put("/datapoints/Switch;OnOff",
                context =>
                {
                    context.XMLBody(aStateChangedDatapoint);
                    context.Accept("application/xml");
                })
                .Then
                .Get("/datapoints/", context => context.Accept("application/xml"));

            var actualBody = actualResponse.Body.DeserializeXml<Datapoint[]>();
            Assert.AreEqual(1, actualBody.Length);
            AssertAreSame(aStateChangedDatapoint, actualBody[0]);
        }


        [Test]
        public void Should_be_able_to_delete_datapoint_with_delete()
        {
            var actualResponse = sut.Post("/datapoints/",
                context =>
                {
                    context.JsonBody(aDatapoint);
                    context.Accept("application/json");
                })
                .Then
                .Delete("/datapoints/Switch;OnOff", context => context.Accept("application/json"))
                .Then
                .Get("/datapoints/", context => context.Accept("application/json"));

            var actualBody = actualResponse.Body.DeserializeJson<Datapoint[]>();
            Assert.AreEqual(HttpStatusCode.OK, actualResponse.StatusCode);
            Assert.IsEmpty(actualBody);
        }

        [Test]
        public void Should_be_able_to_get_datapoint_as_xml()
        {
            var actualResponse = sut.Post("/datapoints/",
                context =>
                {
                    context.XMLBody(aDatapoint);
                    context.Accept("application/xml");
                })
                .Then
                .Get("/datapoints/", context => context.Accept("application/xml"));

            var actualBody = actualResponse.Body.DeserializeXml<Datapoint[]>();
            Assert.AreEqual(1, actualBody.Length);
            AssertAreSame(aDatapoint, actualBody[0]);
        }

        [Test]
        public void Should_be_able_to_get_posted_datapoint_as_json()
        {
            var actualResponse = sut.Post("/datapoints/",
                context =>
                {
                    context.XMLBody(aDatapoint);
                    context.Accept("application/json");
                })
                .Then
                .Get("/datapoints/", context => context.Accept("application/json"));

            var actualBody = actualResponse.Body.DeserializeJson<Datapoint[]>();
            Assert.AreEqual(1, actualBody.Length);
            AssertAreSame(aDatapoint, actualBody[0]);
        }

        [Test]
        public void Should_give_access_to_overview_documentation()
        {
            var actualResponse = sut.Get("/docs/overview.htm", with =>
                with.Accept("text/html"));
            Assert.AreEqual(HttpStatusCode.OK, actualResponse.StatusCode);
        }
    }
}
