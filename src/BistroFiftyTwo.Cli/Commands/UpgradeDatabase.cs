using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BistroFiftyTwo.Cli.Commands
{
    public class UpgradeDatabase : ICommand
    {
        public string CommandName { get; set; } = "UpgradeDatabase";

        public async Task Execute(CommandArguments arguments)
        {
            throw new NotImplementedException();
        }
    }
}
