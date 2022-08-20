﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XcentiumCodingChallege.Models
{
    public class PageContent
    {
        public string website { get; set; }

        public List<string> ImageUrls { get; set; }

        public int Count { get; set; }

        public List<Word> Words { get; set; }
    }
}