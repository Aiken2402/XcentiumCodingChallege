using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using XcentiumCodingChallege.Models;

namespace XcentiumCodingChallege.Services
{
    public class URLDownloader
    {
        private HtmlDocument document;

        public URLDownloader(string url)
        {
            try
            {
                document = new HtmlWeb().Load(url);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<string> ImageDownloader()
        {
            try
            {
                //images with the following extension
                List<string> imageType = new List<string> { ".jpg",".jpeg" };

                //retrieve all src from website
                var urls = document.DocumentNode.Descendants("img")
                                                .Select(e => e.GetAttributeValue("src", null))
                                                .Where(s => !String.IsNullOrEmpty(s) && imageType.Any(y => s.Contains(y))).Distinct().ToList();

                return urls;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public List<string> WordDownloader()
        {
            try
            {

                List<string> allData = new List<string>();

                //check whether the data retrieved is a word
                Regex regexItem = new Regex("[^a-zA-Z']+");

                //get all words from website
                var words = document.DocumentNode.SelectSingleNode("//body").DescendantsAndSelf().Where(y => y.NodeType == HtmlNodeType.Text && y.ParentNode.Name != "script" && y.InnerHtml != string.Empty).Select(x => x.InnerHtml);

                //split each word from html and add to a list
                foreach (var word in words)
                {
                    allData.AddRange(word.Split(' ').Where(x => x != string.Empty && x.Length > 1 && !regexItem.Match(x).Success));
                }


                return allData;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}