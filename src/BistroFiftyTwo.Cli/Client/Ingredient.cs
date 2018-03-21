using System;

namespace BistroFiftyTwo.Cli.Client
{
    public class Ingredient
    {
        public int ordinal { get; set; }
        public string recipeId { get; set; }
        public double quantity { get; set; }
        public string units { get; set; }
        public string ingredient { get; set; }
        public string notes { get; set; }
        public string id { get; set; }
        public DateTime createdDate { get; set; }
        public object createdBy { get; set; }
        public DateTime modifiedDate { get; set; }
        public object modifiedBy { get; set; }
    }
}