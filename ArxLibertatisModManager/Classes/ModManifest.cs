using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArxLibertatisModManager.Classes
{
    public class ModManifest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = null;
        public string? Author { get; set; } = null;
        public string? Version { get; set; } = null;
        public string? Url { get; set; } = null;
    }
}
