using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace HttPete.Services.Terminal
{
    public static class JobObjectManager
    {
        private static SafeFileHandle? _jobHandle;

        static JobObjectManager()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _jobHandle = CreateJobObject(nint.Zero, null);
                if (_jobHandle == null || _jobHandle.IsInvalid)
                {
                    throw new InvalidOperationException("Failed to create job object.");
                }

                var extendedInfo = new JOBOBJECT_EXTENDED_LIMIT_INFORMATION
                {
                    BasicLimitInformation = new JOBOBJECT_BASIC_LIMIT_INFORMATION
                    {
                        LimitFlags = JOB_OBJECT_LIMIT_FLAGS.JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE
                    }
                };

                int length = Marshal.SizeOf(typeof(JOBOBJECT_EXTENDED_LIMIT_INFORMATION));
                nint extendedInfoPtr = Marshal.AllocHGlobal(length);
                try
                {
                    Marshal.StructureToPtr(extendedInfo, extendedInfoPtr, false);

                    if (!SetInformationJobObject(_jobHandle, JobObjectInfoClass.ExtendedLimitInformation, extendedInfoPtr, (uint)length))
                    {
                        throw new InvalidOperationException("Failed to set information on job object.");
                    }
                }
                finally
                {
                    Marshal.FreeHGlobal(extendedInfoPtr);
                }
            }
        }

        public static void AddProcessToJob(Process process)
        {
            if (_jobHandle != null && !_jobHandle.IsInvalid)
            {
                if (!AssignProcessToJobObject(_jobHandle, process.Handle))
                {
                    throw new InvalidOperationException("Failed to assign process to job object.");
                }
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern SafeFileHandle CreateJobObject(nint lpJobAttributes, string? lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AssignProcessToJobObject(SafeFileHandle hJob, nint hProcess);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetInformationJobObject(SafeFileHandle hJob, JobObjectInfoClass infoClass, nint lpJobObjectInfo, uint cbJobObjectInfoLength);

        [StructLayout(LayoutKind.Sequential)]
        private struct JOBOBJECT_BASIC_LIMIT_INFORMATION
        {
            public long PerProcessUserTimeLimit;
            public long PerJobUserTimeLimit;
            public JOB_OBJECT_LIMIT_FLAGS LimitFlags;
            public nint MinimumWorkingSetSize;
            public nint MaximumWorkingSetSize;
            public int ActiveProcessLimit;
            public nint Affinity;
            public int PriorityClass;
            public int SchedulingClass;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
        {
            public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;
            public IO_COUNTERS IoInfo;
            public nint ProcessMemoryLimit;
            public nint JobMemoryLimit;
            public nint PeakProcessMemoryUsed;
            public nint PeakJobMemoryUsed;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct IO_COUNTERS
        {
            public ulong ReadOperationCount;
            public ulong WriteOperationCount;
            public ulong OtherOperationCount;
            public ulong ReadTransferCount;
            public ulong WriteTransferCount;
            public ulong OtherTransferCount;
        }

        private enum JobObjectInfoClass
        {
            BasicAccountingInformation = 1,
            BasicLimitInformation = 2,
            BasicProcessIdList = 3,
            BasicUIRestrictions = 4,
            SecurityLimitInformation = 5,
            EndOfJobTimeInformation = 6,
            AssociateCompletionPortInformation = 7,
            BasicAndIoAccountingInformation = 8,
            ExtendedLimitInformation = 9,
            JobSetInformation = 10,
            GroupInformation = 11,
        }

        [Flags]
        private enum JOB_OBJECT_LIMIT_FLAGS : uint
        {
            JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE = 0x00002000,
        }
    }
}