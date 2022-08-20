using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using XcentiumCodingChallege.Models;

namespace XcentiumCodingChallege.Services
{
    public static class URLDownloader
    {
        public static List<string> ImageDownloader(string url)
        {
            HtmlDocument document = new HtmlWeb().Load(url);
            var urls = document.DocumentNode.Descendants("img")
                                            .Select(e => e.GetAttributeValue("src", null))
                                            .Where(s => !String.IsNullOrEmpty(s)).Distinct().ToList();
            return urls;
        }

        public static List<string> WordDownloader(string url)
        {
            List<string> allData = new List<string>();
            Regex regexItem = new Regex("[^a-zA-Z_.]+");

            var doc = new HtmlWeb().Load(url);
            var words = doc.DocumentNode.SelectSingleNode("//body").DescendantsAndSelf().Where(y => y.NodeType == HtmlNodeType.Text && y.ParentNode.Name != "script" && y.InnerHtml != string.Empty).Select(x => x.InnerHtml);

            foreach (var word in words)
            {
                allData.AddRange(word.Split(' ').Where(x => x != string.Empty && x.Length > 1));
            }
            
            return allData;
        }

        private static HtmlDocument DownloadHtml(string url)
        {
            return new HtmlWeb().Load(url);
        }
    }
}