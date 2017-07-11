namespace BistroFiftyTwo.Server.Parser
{
    public class ParseError
    {
        public int Line { get; set; }
        public ErrorType ErrorType { get; set; }
        public int Character { get; set; }
        public string UnparsedLine { get; set; }
        public ParseErrorCode ErrorCode { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Line}, {Character} - {ErrorType}-{ErrorCode} {Description} in {UnparsedLine}";
        }
    }
}
