namespace RecipeManager.WebApp.Entities
{
    public class Recipe : DataObject
    {
        public string Title { get; set; }
        public string Key { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
    }
}