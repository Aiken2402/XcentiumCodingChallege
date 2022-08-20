using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XcentiumCodingChallege.Models;
using XcentiumCodingChallege.Services;

namespace XcentiumCodingChallege.Controllers
{
    public class PageContentController : Controller
    {
        // GET: Carousel
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetContent(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    return RedirectToAction("ErrorMessage");
                }
                PageContent pageContent = new PageContent();
                pageContent = APIRequestor.LoadUrl(url).Result;
                return PartialView(pageContent);
            }
            catch
            {
               return RedirectToAction("ErrorMessage");
            }
        }

        public ActionResult ErrorMessage()
        {
            return PartialView();
        }
    }
}