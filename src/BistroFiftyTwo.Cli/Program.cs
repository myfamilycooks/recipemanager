using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BistroFiftyTwo.Cli.Commands;
using Fclp;

namespace BistroFiftyTwo.Cli
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var p = new FluentCommandLineParser();
            var commandArgs = new CommandArguments();

            //   -h http://localhost:60679 -u chef -p mustard -a import -f "C:\source\github\myfamilycooks\recipemanager\samples" --pattern *.txt
            p.Setup<string>("api-host").Callback(host => commandArgs.Host = host).Required();
            p.Setup<string>("password").Callback(pass => commandArgs.Password = pass).Required();
            p.Setup<string>("username").Callback(user => commandArgs.Username = user).Required();
            p.Setup<string>("action").Callback(a => commandArgs.ActionName = a).Required();
            p.Setup<string>("folder").Callback(f => commandArgs.Folder = f).Required();
            p.Setup<string>("pattern").Callback(pat => commandArgs.FilePattern = pat).Required();

            p.Parse(args);

            var commands = GetAll<ICommand>();
            var commandFound = false;
            foreach (var c in commands)
                if (c.CommandName.Equals(commandArgs.ActionName))
                {
                    commandFound = true;
                    c.Execute(commandArgs).GetAwaiter().GetResult();
                }

            if (!commandFound)
                Console.WriteLine(
                    @"Usage: bistrofiftytwo.cli -h http://localhost:60679 -u youruser -p yourpass -a import -f 'C:\source\github\myfamilycooks\recipemanager\samples' --pattern *.txt");

            Console.ReadLine();
        }

        public static IEnumerable<T> GetAll<T>()
        {
            var assembly = Assembly.GetExecutingAssembly();

            foreach (var ti in assembly.DefinedTypes)
                if (ti.ImplementedInterfaces.Contains(typeof(T)))
                    yield return (T) assembly.CreateInstance(ti.FullName);
        }
    }
}