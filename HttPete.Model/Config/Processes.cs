using System.Diagnostics;
using SAK25.Helpers;

namespace HttPete.Services.Config
{
    public static class Processes
    {
        private static readonly Dictionary<string, Process> _cache = new Dictionary<string, Process>();

        /// <summary>
        /// The number of processes actively running.
        /// </summary>
        public static int Count => _cache.Count;

        public static Process CreateProcess(ProcessStartInfo startInfo)
        {
            if (_cache.TryGetValue($"{startInfo.FileName}_{startInfo.Arguments}", out Process? p))
                return p;

            p = new Process
            {
                StartInfo = startInfo,
            };

            _cache.Add($"{startInfo.FileName}_{startInfo.Arguments}", p);
            return p;
        }

        public static void DisposeAll()
        {
            string result = $"---- [DISPOSE ALL PROCESSES] ----\n";
            result += $"==> Processes: {_cache.Count}";

            foreach (var p in _cache)
            {
                result += $"==== {p.Value}\n";

                try
                {
                    result += $"====== Disposing...\n";
                    p.Value.Dispose();
                    result += $"====== [OK]: Disposed successfully\n";
                }
                catch (Exception de)
                {
                    result += $"====== [EXCEPTION_DISPOSE]: `{de.Message}`, attempting to kill...";

                    try
                    {
                        p.Value.Kill();
                        result += $"======== [OK]: killed successfully\n";
                    }
                    catch (Exception ke)
                    {
                        var s = $"======== [EXCEPTION_KILL]: Unable to kill process - {ke.Message}\n";
                        result += s;
                        HttPeteSettings.AddError(s, result);

                        throw;
                    }
                    throw;
                }
            }
        }
    }

}
