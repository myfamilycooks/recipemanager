using System;
using System.Collections.Generic;
using System.Linq;

namespace BistroFiftyTwo.Server.Entities
{
    public class Step : IBistroEntity
    {
        public int Ordinal { get; set; }
        public Guid RecipeId { get; set; }
        public string Instructions { get; set; }
        public Guid ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public List<string> Columns()
        {
            return GetType()
                .GetProperties()
                .Where(p => p.PropertyType.IsConstructedGenericType == false)
                .Select(p => p.Name).ToList();
        }

        public string TableName()
        {
            return "recipe_steps";
        }
    }
}