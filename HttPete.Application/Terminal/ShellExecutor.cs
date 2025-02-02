using System.Diagnostics;
using System.Runtime.InteropServices;
using HttPete.Services.Config;

namespace HttPete.Services.Terminal
{
    public class ShellExecutor
    {
        private readonly string _workingDir;

        public ShellExecutor(string workingDir)
        {
            _workingDir = workingDir;
        }

        /// <summary>
        /// Executes a shell command and returns the output and error messages.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="arguments">The arguments for the command.</param>
        /// <returns>The output from the shell command execution.</returns>
        public async Task<(string output, Process process)> ExecuteShellCommand(string command, string arguments)
        {
            try
            {
                // Determine the shell and argument prefix based on the operating system
                string shell;
                string shellArgumentPrefix;

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    shell = "powershell.exe";
                    shellArgumentPrefix = "-Command";
                }
                else
                {
                    shell = "/bin/bash";
                    shellArgumentPrefix = "-c";
                }

                var processStartInfo = new ProcessStartInfo
                {
                    FileName = shell,
                    Arguments = $"{shellArgumentPrefix} \"{command} {arguments}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = _workingDir
                };

                using var process = Processes.CreateProcess(processStartInfo);

                process.Start();
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    JobObjectManager.AddProcessToJob(process);
                }

                string output = await process.StandardOutput.ReadToEndAsync();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new InvalidOperationException($"Command failed with exit code {process.ExitCode}: {error}");
                }

                return (output, process);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while executing the command: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Executes a shell command in the background without waiting for it to complete.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="arguments">The arguments for the command.</param>
        /// <returns>The process instance running the command in the background.</returns>
        public Process ExecuteShellCommandInBackground(string command, string arguments)
        {
            try
            {
                // Determine the shell and argument prefix based on the operating system
                string shell;
                string shellArgumentPrefix;

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    shell = "powershell.exe";
                    shellArgumentPrefix = "-Command";
                }
                else
                {
                    shell = "/bin/bash";
                    shellArgumentPrefix = "-c";
                }

                var processStartInfo = new ProcessStartInfo
                {
                    FileName = shell,
                    Arguments = $"{shellArgumentPrefix} \"{command} {arguments}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = _workingDir
                };

                var process = Processes.CreateProcess(processStartInfo);

                // Start the process
                process.Start();

                // Optionally, you could attach event handlers to capture output or errors if needed
                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        Console.WriteLine($"Output: {e.Data}");
                    }
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        Console.WriteLine($"Error: {e.Data}");
                    }
                };

                // Begin asynchronous reading of the output
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                return process;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while starting the background process: {ex.Message}", ex);
            }
        }

    }

}
