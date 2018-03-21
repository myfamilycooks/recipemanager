namespace BistroFiftyTwo.Cli.Client
{
    public class Error
    {
        public int line { get; set; }
        public int errorType { get; set; }
        public int character { get; set; }
        public string unparsedLine { get; set; }
        public int errorCode { get; set; }
        public string description { get; set; }
    }
}