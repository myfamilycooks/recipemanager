using System.Collections.Generic;

namespace BistroFiftyTwo.Cli.Client
{
    public class ParsedRecipe
    {
        public List<Error> errors { get; set; }
        public int status { get; set; }
        public object input { get; set; }
        public Recipe output { get; set; }
    }
}