using System;
using System.Collections.Generic;

namespace BistroFiftyTwo.Cli.Client
{
    public class Recipe
    {
        public string title { get; set; }
        public string key { get; set; }
        public string tags { get; set; }
        public string description { get; set; }
        public object notes { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public List<Step> steps { get; set; }
        public string fullTextReference { get; set; }
        public string id { get; set; }
        public DateTime createdDate { get; set; }
        public object createdBy { get; set; }
        public DateTime modifiedDate { get; set; }
        public object modifiedBy { get; set; }
    }
}