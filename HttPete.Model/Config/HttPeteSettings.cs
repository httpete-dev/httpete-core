namespace HttPete.Services.Config
{
    public static class HttPeteSettings
    {
        /* ### PACKAGE ### */
        public static readonly string PULSE_VERSION = "genesis001";

        /* ### API ### */
        public static readonly string DEFAULT_RESPONSE_MESSAGE = "Unknown behavior, no response message specified.";

        /* ### LOGS ### */
        /// <summary>
        /// The maximum allowed characters per log entry. Default is 2000.
        /// This means, effectively, each log entry can take up to 2kb (1byte/char)
        /// </summary>
        public static readonly int MAX_LOG_LENGTH = 2_000; // 2kB
        public static readonly int MAX_BUFFER_MEMORY = 20_000; // 20 kB
        public static readonly int PORT = 56969;
        public static readonly string STARTUP_COMMAND = $"npm run dev";
        public static readonly string CONFIG_PATH = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile).Replace('\\', '/').Replace("//", "/")}/.httpete";

        private static bool hasUpdateAvailable = false;
        public static bool AutoUpdate { get; set; } = true;
        public static bool UpdateAvailable { get { return AutoUpdate && hasUpdateAvailable; } set { hasUpdateAvailable = value; } }

        public static string Branch { get; set; }
        public static string RootDir { get; set; }
        public static string LogDir { get; set; }

        public static bool HasError => Errors != null && Errors?.Length > 0;


        public static CliError[]? Errors {get; private set;}

        public static void Init(string branch)
        {
            UpdateAvailable = false;
            Branch = branch;
        }

        public static void AddError(string title, string message)
        {
            var err = new CliError
            {
                Title = title,
                Message = message
            };

            
            Errors = [..(Errors ?? []), err];    
        }
    }
}
