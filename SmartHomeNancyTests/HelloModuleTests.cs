// ****************************************************************************
// <copyright file="HelloModuleTests.cs" author="Nikolaos Kokkinos">
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

namespace SmartHomeNancyTests
{
    [TestFixture]
    public class HomeModuleTests
    {

        [TestCase("/")]
        public void Should_answer_HttpStatusCode_ok_on_root_Path(string path)
        {
            var sut = new Browser(new DefaultNancyBootstrapper());

            var actualResponse = sut.Get(path);

            Assert.AreEqual(HttpStatusCode.OK, actualResponse.StatusCode);
        }

    }
}
