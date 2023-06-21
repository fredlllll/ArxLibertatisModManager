using ArxLibertatisModManager.Classes;
using Avalonia;
using System;

namespace ArxLibertatisModManager
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            if (AppStatus.IsAlreadyRunning)
            {
                AppStatus.OpenOtherInstance();
                return;
            }
            AppStatus.CreatePipeListener();

            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
    }
}