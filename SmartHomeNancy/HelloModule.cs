// ****************************************************************************
// <author>Nikolaos Kokkinos</author>
// <email>nik.kokkinos@windowslive.com</email>
// <date>28.01.2015</date>
// <project>SmartHomeNancy</project>
// <web>http://nikolaoskokkinos.wordpress.com/</web>
// ****************************************************************************

using Nancy;

namespace SmartHomeNancy
{
    public class HelloModule:NancyModule
    {
        public HelloModule()
        {
            Get["/"] = _ => HttpStatusCode.OK;

            Get["/hello"] = _ => View["Hello"];
        }
    }
}
