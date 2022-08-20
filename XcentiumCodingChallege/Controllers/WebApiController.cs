using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XcentiumCodingChallege.Models;
using XcentiumCodingChallege.Services;

namespace XcentiumCodingChallege.Controllers
{
    public class WebApiController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetPageContent()
        {
            PageContent model = new PageContent();
            List<string> words = new List<string>();

            words = URLDownloader.WordDownloader("https://www.football.london/arsenal-fc/transfer-news/arsenal-news-transfers-live-tielemans-24796044");

            model.ImageUrls = URLDownloader.ImageDownloader("https://www.football.london/arsenal-fc/transfer-news/arsenal-news-transfers-live-tielemans-24796044");
            model.Count = words.Count();
            model.Words = words.GroupBy(x => x).ToDictionary(kvp => kvp.Key, kvp => kvp.Count()).OrderByDescending(x => x.Value).Select(x => x.Key).Take(10).ToList();

            return Json(model);
        }

        [HttpGet]
        public IHttpActionResult GetPageContent(string url)
        {
            PageContent model = new PageContent();
            List<string> words = new List<string>();

            words = URLDownloader.WordDownloader(url);

            model.ImageUrls = URLDownloader.ImageDownloader(url);
            model.Count = words.Count();
            model.Words = words.GroupBy(x => x).ToDictionary(kvp => kvp.Key, kvp => kvp.Count()).OrderByDescending(x => x.Value).Select(x => x.Key).Take(10).ToList();

            return Json(model);
        }
    }
}
