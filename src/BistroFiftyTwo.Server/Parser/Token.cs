namespace BistroFiftyTwo.Server.Parser
{
    public class Token
    {
        public string Value { get; set; }
        public Token Next { get; set; }
        public Token Previous { get; set; }
    }
}