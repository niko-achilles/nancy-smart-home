// ****************************************************************************
// <copyright file="DatapointsModule.cs" author="Nikolaos Kokkinos">
// Copyright © Nikolaos Kokkinos
// </copyright>
// ****************************************************************************
// <author>Nikolaos Kokkinos</author>
// <email>nik.kokkinos@windowslive.com</email>
// <date>28.01.2015</date>
// <project>SmartHomeNancy</project>
// <web>http://nikolaoskokkinos.wordpress.com/</web>
// ****************************************************************************

using Nancy;
using System.Linq;
using Nancy.ModelBinding;
using SmartHomeNancy.Model;

namespace SmartHomeNancy
{
    public class DatapointsModule:NancyModule
    {
        public DatapointsModule(IDataStore datapointStore):
            base("datapoints")
       {
            Get["/"] = _ => Negotiate
                
                .WithModel(datapointStore.GetAll().ToArray())
                .WithView("Datapoint");

            Post["/"] = _ =>
                {
                    var newDatapoint = this.Bind<Datapoint>();
                    
                    if (!datapointStore.TryAdd(newDatapoint))
                    {
                        return HttpStatusCode.NotAcceptable;
                    }

                        return Negotiate.WithModel(newDatapoint)
                        .WithStatusCode(HttpStatusCode.Created)
                        .WithView("Created"); 

                    //return Response
                    //    .AsJson(newTodo)
                    //    .WithStatusCode(HttpStatusCode.Created);
                };

            Put["/{name}"] = p =>
                {

                    var updatedDatapoint = this.Bind<Datapoint>();
                    if (!datapointStore.TryUpdate(updatedDatapoint))
                    {
                        return HttpStatusCode.NotFound;
                    }

                    //return Response.AsJson(updatedDatapoint);
                    return Negotiate.WithModel(updatedDatapoint)
                        .WithStatusCode(HttpStatusCode.OK);
                };

            Delete["/{name}"] = p =>
                {
                    if (datapointStore.TryRemove(p.name))
                    {
                        return HttpStatusCode.OK;
                        
                    }
                    return HttpStatusCode.NotFound;
                };
        }
    }
}
