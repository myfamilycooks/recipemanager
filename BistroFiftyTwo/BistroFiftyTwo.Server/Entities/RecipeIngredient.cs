using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BistroFiftyTwo.Server.Entities
{
    public class RecipeIngredient : IBistroEntity
    {
        public Guid ID {get; set;}
        public int Ordinal { get; set; }
        public Guid RecipeId { get; set; }
        public Double Quantity { get; set; }
        public string Units { get; set; }
        public string Ingredient { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate {get; set;}
        public string CreatedBy {get; set;}
        public DateTime ModifiedDate {get; set;}
        public string ModifiedBy {get; set;}

        public List<string> Columns()
        {
            return GetType()
                .GetProperties()
                .Where(p => p.PropertyType.IsAssignableFrom(typeof(IEnumerable<>)) == false)
                .Select(p => p.Name).ToList();
        }

        public string TableName()
        {
            return "recipe_ingredients";
        }
    }
}
