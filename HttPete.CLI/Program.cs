using CommandLine;

namespace HttPete.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CommandLine.Parser.Default.ParseArguments<HttPeteCliOptions>(args)
                .WithParsed(ParseArguments)
                .WithNotParsed(HandleParseError);
        }

        static void ParseArguments(HttPeteCliOptions opts)
        {
            
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {

        }
    }
}
