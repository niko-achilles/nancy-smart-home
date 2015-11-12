// ****************************************************************************
// <author>Nikolaos Kokkinos</author>
// <email>nik.kokkinos@windowslive.com</email>
// <date>28.01.2015</date>
// <project>SmartHomeNancy</project>
// <web>http://nikolaoskokkinos.wordpress.com/</web>
// ****************************************************************************


using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;
using Nancy.ViewEngines.Razor;
using System;

namespace SmartHomeNancy
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private RazorViewEngine ensureRazorIsLoaded;

        private string logAllRequestsString = String.Empty;
        private string logAllResponsesCodeString = String.Empty;
        private string logUnhandledExceptionsSting = String.Empty;

        
        public string LogAllRequestsString
        {
            get
            {
                return logAllRequestsString;
            }
        }

        public string LogAllResponsesCodeString
        {
            get
            {
                return logAllResponsesCodeString;
            }
        }

        public string LogUnhandledExceptionString
        {
            get
            {
                return logUnhandledExceptionsSting;
            }
        }

        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            LogAllRequests(pipelines);
            LogAllResponseCodes(pipelines);
            LogUnhandledExceptions(pipelines);
        }

        private void LogAllRequests(IPipelines pipelines)
        {
            pipelines.BeforeRequest += ctx =>
            {
                logAllRequestsString = string.Format("Handling request {0} {1}",
                    ctx.Request.Method, ctx.Request.Path);
                return null;
            };
        }

        private void LogAllResponseCodes(IPipelines pipelines)
        {
            pipelines.AfterRequest += ctx =>
              logAllResponsesCodeString = string.Format("Responding {0} to {1} {2}",
              ctx.Response.StatusCode, ctx.Request.Method, ctx.Request.Path);
        }

        private void LogUnhandledExceptions(IPipelines pipelines)
        {
            pipelines.OnError.AddItemToStartOfPipeline((ctx, err) =>
            {
                logUnhandledExceptionsSting = string.Format
                    ("Request {0} {1} failed. Error Message: {2}",
                    ctx.Request.Method, ctx.Request.Path, err.Message);
                return null;
            });
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            var datastore = new InMemoryDataStore();
            container.Register<IDataStore>(datastore);
        }

        protected override 
            void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);

            conventions.StaticContentsConventions.Add(
              StaticContentConventionBuilder.AddDirectory
              ("/docs", "Docs"));
        }
    }
}
