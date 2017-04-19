using System;

namespace RecipeManager.WebApp.Entities
{
    public class Step : DataObject
    {
        public int Ordinal { get; set; }
        public Guid RecipeId { get; set; }
        public string Instruction { get; set; }
    }
}