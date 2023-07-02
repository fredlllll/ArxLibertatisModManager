using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ArxLibertatisModManager.Classes
{
    public static class Util
    {
        public static void OpenURL(string url)
        {
            try
            {
                if (System.IO.File.Exists(url))
                {
                    throw new ArgumentException("url cant be a path to a local file for security reasons");
                }
            }
            catch { }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                throw new NotSupportedException("only windows, linux and osx are supported for opening urls atm");
            }
        }
    }
}