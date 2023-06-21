using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArxLibertatisModManager.Classes
{
    public static class AppStatus
    {
#pragma warning disable IDE0052 // Remove unread private members
        private static Mutex? mutex = null;
#pragma warning restore IDE0052 // Remove unread private members
        private static FileStream? lockFile = null;
        private static bool? isAlreadyRunning = null;
        public static bool IsAlreadyRunning
        {
            get
            {
                if (isAlreadyRunning == null)
                {
                    if (OperatingSystem.IsWindows())
                    {
                        isAlreadyRunning = CheckMutex();
                    }
                    else if (OperatingSystem.IsLinux())
                    {
                        isAlreadyRunning = CheckLockFile();
                    }
                    else
                    {
                        isAlreadyRunning = false; //assume it isnt running already on other platforms, macos should handle it automatically anyway
                    }
                }
                return (bool)isAlreadyRunning;
            }
        }

        [SupportedOSPlatform("windows")]
        private static bool CheckMutex()
        {
            mutex = new Mutex(false, "TetherSync", out bool createdNew);
            return !createdNew;
        }

        [SupportedOSPlatform("windows")]
        [SupportedOSPlatform("linux")]
        private static bool CheckLockFile()
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Barker Technologies", "TetherSync");
            Directory.CreateDirectory(dir);
            try
            {
                lockFile = File.Open(Path.Combine(dir, ".lock"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                lockFile.Lock(0, 0);
                return false;
            }
            catch
            {
                return true;
            }
        }

        private const string pipeName = "TetherSync";

        public static void OpenOtherInstance()
        {
            using var pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.Out);
            pipeClient.Connect();
            BinaryWriter writer = new(pipeClient);
            writer.Write("open");
            writer.Flush();
            pipeClient.Close();
        }

        public static event Action? OpenRequested;
        private static Thread? pipeServerThread;

        public static void CreatePipeListener()
        {
            if (pipeServerThread != null)
            {
                throw new Exception("can only create one pipe listener");
            }
            pipeServerThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        using NamedPipeServerStream pipeServer = new(pipeName, PipeDirection.In);
                        pipeServer.WaitForConnection();
                        var reader = new BinaryReader(pipeServer);

                        var command = reader.ReadString();
                        if (command == "open")
                        {
                            OpenRequested?.Invoke();
                        }

                        pipeServer.Close();
                    }
                    catch
                    {
                        //ignore errors
                    }
                }
            })
            {
                IsBackground = true
            };
            pipeServerThread.Start();
        }
    }
}
