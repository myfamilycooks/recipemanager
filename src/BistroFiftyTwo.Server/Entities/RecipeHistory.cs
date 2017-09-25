using System;
using System.Collections.Generic;

namespace BistroFiftyTwo.Server.Entities
{
    public class RecipeHistory : IBistroEntity
    {
        public Guid ID { get; set; }
        public Guid RecipeID { get; set; }
        public int Version { get; set; }
        public string FullText { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public string TableName()
        {
            return "Recipe_Histories";
        }

        public List<string> Columns()
        {
            throw new NotImplementedException();
        }
    }
}