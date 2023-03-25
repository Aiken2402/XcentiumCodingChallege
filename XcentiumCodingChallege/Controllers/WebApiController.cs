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
        public IHttpActionResult GetPageContent(string url)
        {
            PageContent model = new PageContent();
            List<string> words = new List<string>();

            try
            {
                URLDownloader urlDownloader = new URLDownloader(url);

                //get website url 
                string path = url.Substring(0, url.LastIndexOf("/") + 1);

                //get the list of words
                words = urlDownloader.WordDownloader();

                //bind website data to model
                model.website = path;

                //Get all images from the url
                model.ImageUrls = urlDownloader.ImageDownloader();

                //Get total word count
                model.Count = words.Count();

                //Get graph of the top 10 words and their counts
                model.Words = words.GroupBy(x => x).ToDictionary(kvp => kvp.Key, kvp => kvp.Count()).OrderByDescending(x => x.Value).Select(y => new Word { Text = y.Key, Count = y.Value }).Take(10).ToList();

                return Json(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
