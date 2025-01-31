using HttPete.Services.Config;
using SAK25.Extensions;

namespace HttPete.Crosscutting
{
    public enum HttPeteLoggerType
    {
        JSON,
        SQLITE
    }

    public record PLog
    {
        public string Message { get; set; }

        public DateTime TimestampUTC { get; set; }

        public string MyProperty { get; set; }
    }

    public interface IPLogger
    {
        void Initialize(HttPeteLoggerType pulseLoggerType = HttPeteLoggerType.SQLITE);
        
        void Dispose();
                
        void Log(string message);
        
        void Error(string message, Exception exception);
        
        void Warn(string message);
        
        void Info(string message);
    }

    public class HttPeteLogger : IPLogger
    {
        private static int bufferSize;
        private static string[] buffer;

        private static List<HttPeteLogger> _instances = new List<HttPeteLogger> ();

        /// <summary>
        /// The number of elements already filled.
        /// </summary>
        private static int bufferFilled;
        private static HttPeteLoggerType type;

        private bool initialized = false;

        public void Initialize(HttPeteLoggerType pulseLoggerType = HttPeteLoggerType.SQLITE)
        {
            _instances.Add(this);

            bufferSize = HttPeteSettings.MAX_BUFFER_MEMORY / HttPeteSettings.MAX_LOG_LENGTH;
            buffer = new string[bufferSize];
            type = pulseLoggerType;
            initialized = true;
        }

        public void Dispose()
        {
            ClearBuffer();
        }

        public static void DisposeAll()
        {
            foreach (HttPeteLogger p in _instances)
            {
                p.Dispose();
            }
        }

        private void ClearBuffer()
        {
            string output = "";
            string filename = "";

            switch (type)
            {
                case HttPeteLoggerType.JSON:
                    output = string.Join('\n', buffer);
                    filename = $"{HttPeteSettings.PULSE_VERSION}_{DateTime.UtcNow.ToString("yyyy-MM-dd")}.json";
                    break;
                case HttPeteLoggerType.SQLITE:
                    //_sqliteConnector.SaveLogs(buffer);
                    break;
                default:
                    output = string.Empty;
                    break;
            }

            File.AppendAllText(HttPeteSettings.LogDir + filename, output);

            // TODO: Limit log file size

            bufferFilled = 0;
        }

        public void Log(string msg)
        {
            if (!initialized)
                throw new Exception("HttPeteLogger has not been properly initialized. Please make sure to call `_logger.Initialize([HttPeteLoggerType.PULSE | HttPeteLoggerType.JSON | HttPeteLoggerType.SQLITE])` for every instance being used.");

            if (bufferFilled == buffer.Length - 1)
                ClearBuffer();
            
            buffer[bufferFilled++] = $"[{DateTime.UtcNow.ToString("HH:mm:ss.ff")}]{msg}";
        }

        public void Error (string msg, Exception e)
        {
            string result = $"[ERROR]: {msg} - [e: {e.Message}]";
            Log(msg);
        }

        public void Warn(string msg)
        {
            Log($"[warn]: {msg}");
        }

        public void Info(string msg)
        {
            Log($"[info]: {msg}");
        }
    }
}