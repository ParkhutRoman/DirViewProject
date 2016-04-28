using System.Collections.Generic;

namespace DirViewProject.Models
{
    public class Directory
    {
        public string Name { get; set; }
        public string Path { get; set; } = "";

        public int SmallFilesCount { get; set; }
        public int MediumFilesCount { get; set; }
        public int LargeFilesCount { get; set; }

        public List<Item> Files { get; set; } = new List<Item>();
        public List<Item> Directories { get; set; } = new List<Item>();
    }
}