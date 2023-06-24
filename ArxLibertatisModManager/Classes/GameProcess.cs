using System.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArxLibertatisModManager.ViewModels;

namespace ArxLibertatisModManager.Classes
{
    public class GameProcess
    {
        private readonly string executablePath;
        private readonly IEnumerable<ModViewModel> mods;

        private Process gameProcess;

        public GameProcess(string executablePath, IEnumerable<ModViewModel> mods)
        {
            this.executablePath = executablePath;
            this.mods = mods;
        }

        async Task PrepareMods()
        {
            //iterate over mod zips and unpack them into the mods cache
        }

        void StartGameWithMods()
        {
            //-d <directory>
        }

        void StartGameWithArguments(IEnumerable<string>? args = null)
        {
            args ??= Enumerable.Empty<string>();

            gameProcess = new Process();
            var startInfo = gameProcess.StartInfo = new ProcessStartInfo() {
                FileName=executablePath,
                WorkingDirectory = Path.GetDirectoryName(executablePath),
            };
            foreach (var arg in args)
            {
                startInfo.ArgumentList.Add(arg);
            }
        }

        public async Task StartWithMods()
        {
            await PrepareMods();
            StartGameWithMods();
        }

        public void StartWithoutMods()
        {
            StartGameWithArguments();
        }
    }
}
