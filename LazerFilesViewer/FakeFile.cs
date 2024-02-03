namespace LazerFilesViewer
{
    public class FakeFile
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Hash { get; set; }
        public FakeFile(string name, string hash, string preName)
        {
            Name = name;
            Hash = hash;
            FullName = preName + "\\" + name;
        }

        public string GetFileType()
        {
            string suffix = Name.Substring(Name.LastIndexOf('.') + 1);
            return suffix;
        }

        public string GetFilePath()
        {
            return Hash.Substring(0, 1) + "\\" + Hash.Substring(0, 2) + "\\" + Hash;
        }
    }
}
