using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace elasticsearch_myself.Controllers
{
    public class ElasticSearchController : Controller
    {
        // GET: ElasticSearch
        public ActionResult Index()
        {

            return View();
        }
        
    }
    
}