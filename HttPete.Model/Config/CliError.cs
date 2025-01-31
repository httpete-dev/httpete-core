namespace HttPete.Services.Config
{
    public record CliError
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }

    public enum CliCommandType
    {
        CREATE,
        BUILD,
        PACKAGE,
        DEV,
        UNKNOWN = 999
    }

    public static class CliHelpers
    {
        public static IEnumerable<CliCommandType> AvailableCommands() => Enum.GetValues<CliCommandType>().Cast<CliCommandType>();

        public static CliCommandType GetCommand(string name)
        {
            foreach (var command in AvailableCommands())
            {
                if (command.ToString().Equals(name))
                    return command;
            }

            return CliCommandType.UNKNOWN;
        }
    }

    public static class CliExceptionFactory
    {
        public static Exception InvalidCommand = new Exception($"Invalid Command. Options are: {CliHelpers.AvailableCommands()}");
        public static Exception InvalidNotificationOptions = new Exception($"Invalid Notification Options."); // TODO: Be more descriptive

    }
}
