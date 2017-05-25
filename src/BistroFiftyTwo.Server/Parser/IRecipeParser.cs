namespace BistroFiftyTwo.Server.Parser
{
    public interface IRecipeParser
    { 
        ParserResult Parse(string input);
        bool TryParse(string input, out ParserResult result);
        
    }
}
