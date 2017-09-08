using System;
using System.Collections.Generic;
using System.Text;

namespace BistroFiftyTwo.Server.Entities
{
    public class Ingredient
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
