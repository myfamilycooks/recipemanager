namespace BistroFiftyTwo.Server.Parser
{
    public enum ErrorType
    {
        Information,
        Warning,
        Error,
        Fatal,
        MissingSection
    }
    
    public enum ParseErrorCode
    {
        ExceptionOccurred = 556,
        UnknownError = 555,
        NoInput = 554,
        NoIngredients = 557,
        NoInstructions = 558,
        NoDescription = 559
    }
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
