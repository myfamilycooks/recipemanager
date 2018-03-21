using System;

namespace BistroFiftyTwo.Cli.Client
{
    public class Step
    {
        public int ordinal { get; set; }
        public string recipeId { get; set; }
        public string instructions { get; set; }
        public string id { get; set; }
        public DateTime createdDate { get; set; }
        public object createdBy { get; set; }
        public DateTime modifiedDate { get; set; }
        public object modifiedBy { get; set; }
    }
}