using System.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArxLibertatisModManager.ViewModels;
using Avalonia.Threading;
using System.IO.Compression;
using ArxLibertatisModManager.UserControls;

namespace ArxLibertatisModManager.Classes
{
    public class GameProcess
    {
        private readonly string executablePath;
        private readonly IEnumerable<ModViewModel> mods;

        public event Action? ProcessExit;
        public event Action? ProcessStart;

        private Process? gameProcess;

        public GameProcess(string executablePath, IEnumerable<ModViewModel> mods)
        {
            this.executablePath = executablePath;
            this.mods = mods;
        }

        async Task PrepareMods()
        {
            Directory.CreateDirectory(ConfigurationPage.Instance.ViewModel.ModsCacheFolder);
            HashSet<string> directories = new();
            foreach (var mod in mods)
            {
                directories.Add(mod.ModCachePath);
                if (!Directory.Exists(mod.ModCachePath))
                {
                    Directory.CreateDirectory(mod.ModCachePath);
                    ZipFile.ExtractToDirectory(mod.ZipPath, mod.ModCachePath);
                }
            }
            foreach (var dir in Directory.EnumerateDirectories(ConfigurationPage.Instance.ViewModel.ModsCacheFolder))
            {
                if (!directories.Contains(dir))
                {
                    Directory.Delete(dir, true);
                }
            }
        }

        void StartGameWithMods()
        {
            List<string> argList = new();
            foreach (var mod in mods)
            {
                argList.Add("-d");
                argList.Add(mod.ModCachePath);
            }
            StartGameWithArguments(argList);
            //-d <directory>
        }

        void StartGameWithArguments(IEnumerable<string>? args = null)
        {
            args ??= Enumerable.Empty<string>();

            gameProcess = new Process();
            var startInfo = gameProcess.StartInfo = new ProcessStartInfo()
            {
                FileName = executablePath,
                WorkingDirectory = Path.GetDirectoryName(executablePath),
            };
            foreach (var arg in args)
            {
                startInfo.ArgumentList.Add(arg);
            }
            gameProcess.EnableRaisingEvents = true;
            gameProcess.Exited += GameProcess_Exited;
            gameProcess.Start();
        }

        private void OnGameExited()
        {
            if (gameProcess != null)
            {
                gameProcess.Exited -= GameProcess_Exited;
                gameProcess = null;
            }
            Dispatcher.UIThread.Post(() =>
            {
                ProcessExit?.Invoke();
            });
        }

        private void OnGameStarted()
        {
            Dispatcher.UIThread.Post(() =>
            {
                ProcessStart?.Invoke();
            });
        }

        private void GameProcess_Exited(object? sender, EventArgs e)
        {
            OnGameExited();
        }

        public async Task StartWithMods()
        {
            try
            {
                OnGameStarted();
                await PrepareMods();
                StartGameWithMods();
            }
            catch
            {
                OnGameExited();
                throw;
            }
        }

        public void StartWithoutMods()
        {
            try
            {
                OnGameStarted();
                StartGameWithArguments();
            }
            catch
            {
                OnGameExited();
                throw;
            }
        }
    }
}
