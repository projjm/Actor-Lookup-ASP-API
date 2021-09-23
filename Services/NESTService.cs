using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace ActorLookupREST.Services
{
    public static class NESTService
    {
        public static IServiceCollection AddElasticSearch(this IServiceCollection collection)
        {
            var settings = new ConnectionSettings(new Uri("https://actorlookupsearch.es.uksouth.azure.elastic-cloud.com:9243/"))
              .DefaultIndex("title-search");

            settings.BasicAuthentication("elastic", "");

            collection.AddSingleton(new ElasticClient(settings));
            return collection;
        }
    }
}
