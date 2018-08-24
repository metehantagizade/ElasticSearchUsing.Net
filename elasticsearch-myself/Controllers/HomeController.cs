using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace elasticsearch_myself.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ElasticClient client = BasicConnection();
            CreateIndexAndSave(client);
            Search(client);
            return View();
        }

        public bool Search(ElasticClient client)
        {
            try
            {
                var searchResponse = client.Search<Person>(s => s
                                .Query(q => q
                                    .Match(m => m
                                        .Field(f => f.FirstName)
                                        .Query("tohid")
                                    )
                                ).Sort(ss=> ss.Ascending(p=>p.Id))
                );


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public void DeleteAllDataFromType(ElasticClient client)
        {
            client.DeleteByQuery<Person>(d => d.MatchAll());
        }
        public void CreateIndexAndSave(ElasticClient client)
        {
            for (int i = 0; i < 25; i++)
            {
                var person = new Person
                {
                    Id = i,
                    FirstName = "tohid",
                    LastName = "taghizad-" + i
                };

                var indexResponse = client.IndexDocument(person);
            }


        }


        public ElasticClient BasicConnection()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200/")).DefaultIndex("people").RequestTimeout(TimeSpan.FromMinutes(2)).ThrowExceptions();

            var client = new ElasticClient(settings);
            return client;
        }
        public ElasticClient SniffingConnectionnPool()
        {
            var uris = new[]
                            {
                                new Uri("http://localhost:9200"),
                                new Uri("http://localhost:9201"),
                                new Uri("http://localhost:9202"),
                            };

            var connectionPool = new SniffingConnectionPool(uris);
            var settings = new ConnectionSettings(connectionPool)
                .DefaultIndex("people");

            var client = new ElasticClient(settings);
            return client;
        }
    }
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}