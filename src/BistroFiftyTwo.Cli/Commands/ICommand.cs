using System.Threading.Tasks;

namespace BistroFiftyTwo.Cli.Commands
{
    public interface ICommand
    {
        string CommandName { get; set; }
        Task Execute(CommandArguments arguments);
    }
}