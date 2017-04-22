using System;

namespace RecipeManager.WebApp.Entities
{
    public class RecipeIngredient : DataObject
    {
        public int Ordinal { get; set; }
        public Guid RecipeId { get; set; }
        public Decimal Quantity { get; set; }
        public string Units { get; set; }
        public string Item { get; set; }
        public string Notes { get; set; }
    }
}