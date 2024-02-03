using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazerFilesViewer
{
    public class FakeFile
    {
        public string Name { get; set; }
        public string Hash { get; set; }
        public FakeFile(string name, string hash) {
            Name = name;
            Hash = hash;
        }

        public string GetFileType()
        {
            string suffix = Name.Substring(Name.LastIndexOf('.') + 1);
            return suffix;
        }

        public string GetFilePath()
        {
            return Hash.Substring(0,1) + "\\" + Hash.Substring(0,2) + "\\" + Hash;
        }
    }
}
