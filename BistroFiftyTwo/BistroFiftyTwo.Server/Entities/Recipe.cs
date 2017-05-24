using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BistroFiftyTwo.Server.Entities
{
    public class Recipe : IBistroEntity
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Key { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate {get;set;}
        public string CreatedBy {get;set;}
        public DateTime ModifiedDate {get;set;}
        public string ModifiedBy {get;set;}
        public IEnumerable<RecipeIngredient> Ingredients { get; internal set; }
        public IEnumerable<Step> Steps { get; internal set; }

        public List<string> Columns()
        {
            return GetType()
                .GetProperties()
                .Where(p => p.PropertyType.IsConstructedGenericType == false)
                .Select(p => p.Name).ToList();
        }

        public string TableName()
        {
            return "recipes";
        }
    }
}
