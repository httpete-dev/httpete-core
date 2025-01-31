using CommandLine;
using HttPete.Services.Config;

namespace HttPete.CLI
{
    internal class HttPeteCliOptions
    {
        [Option('c', "config", Required = false, HelpText = "Path to the configuration file.")]
        public string ConfigPath { get; set; }

        [Option('v', "verbose", Required = false, HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [Option('q', "quiet", Required = false, HelpText = "Prints only errors to standard output.")]
        public bool Quiet { get; set; }

        public CliCommandType GetCommand(string name) 
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception($"Invalid Command; Options are: [{CliHelpers.AvailableCommands()}]");

            var command = CliHelpers.GetCommand(name);

            return command.Equals(CliCommandType.UNKNOWN) 
                ? throw CliExceptionFactory.InvalidCommand 
                : command;
        }

    }
}
