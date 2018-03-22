namespace BistroFiftyTwo.Cli
{
    public class CommandArguments
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Folder { get; set; }
        public string FilePattern { get; set; } = "*.txt";
        public string ActionName { get; set; }
    }
}