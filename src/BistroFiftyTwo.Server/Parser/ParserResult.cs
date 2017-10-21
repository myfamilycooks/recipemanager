using System.Collections.Generic;
using BistroFiftyTwo.Server.Entities;

namespace BistroFiftyTwo.Server.Parser
{
    public class ParserResult
    {
        public ParserResult()
        {
            Errors = new List<ParseError>();
            Output = new Recipe();
        }

        public List<ParseError> Errors { get; set; }
        public ParseStatus Status { get; set; }
        public string Input { get; set; }
        public Recipe Output { get; set; }
    }
}