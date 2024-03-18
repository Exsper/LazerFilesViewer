namespace LazerFilesViewer
{
    public class FakeDirectory
    {
        public string Name { get; set; }

        public string FullName { get; set; }

        public string KeyWords { get; set; }

        public Guid ID { get; set; } = Guid.Empty;

        public List<FakeDirectory> ChildDirectories { get; set; }
        public List<FakeFile> ChildFiles { get; set; }
        public FakeDirectory(string name, string preName, string keyWords = "")
        {
            Name = name;
            FullName = preName + "\\" + name;
            ChildDirectories = new List<FakeDirectory>();
            ChildFiles = new List<FakeFile>();
            KeyWords = keyWords;
        }

        public FakeDirectory? GetDirectory(string name)
        {
            while (name.StartsWith("\\"))
            {
                name = name.Substring(1);
            }
            while (name.EndsWith("\\"))
            {
                name = name.Substring(0, name.Length - 1);
            }
            int index = name.IndexOf("\\");
            if (index > 0)
            {
                string folderName = name.Substring(0, index);
                string leftName = name.Substring(index + 1);
                FakeDirectory d = ChildDirectories.Find(x => x.Name == folderName);
                if (d != null) { return d.GetDirectory(leftName); }
                else return this;
            }
            return ChildDirectories.Find(x => x.Name == name);
        }

        public FakeFile? GetFile(string name)
        {
            while (name.StartsWith("\\"))
            {
                name = name.Substring(1);
            }
            while (name.EndsWith("\\"))
            {
                name = name.Substring(0, name.Length - 1);
            }
            return ChildFiles.Find(x => x.Name == name);
        }

        public FakeDirectory AddDirectory(string name, string keyWords = "")
        {
            FakeDirectory? d = GetDirectory(name);
            if (d != null) return d;
            d = new FakeDirectory(name, FullName, keyWords);
            ChildDirectories.Add(d);
            return d;
        }

        public bool DeleteDirectory(string name)
        {
            FakeDirectory? d = GetDirectory(name);
            if (d == null) return false;
            ChildDirectories.Remove(d);
            return true;
        }

        public FakeFile AddFile(string name, string hash)
        {
            while (name.StartsWith("/"))
            {
                name = name.Substring(1);
            }
            while (name.EndsWith("/"))
            {
                name = name.Substring(0, name.Length - 1);
            }
            int index = name.IndexOf("/");
            if (index > 0)
            {
                string folderName = name.Substring(0, index);
                string fileName = name.Substring(index + 1);
                return AddDirectory(folderName).AddFile(fileName, hash);
            }
            else
            {
                FakeFile? f = GetFile(name);
                if (f != null) return f;
                f = new FakeFile(name, hash, FullName);
                ChildFiles.Add(f);
                return f;
            }
        }

        public bool DeleteFile(string name)
        {
            FakeFile? f = GetFile(name);
            if (f == null) return false;
            ChildFiles.Remove(f);
            return true;
        }

        public List<FakeDirectory> SearchDirectories(string keyWord)
        {
            if (ChildDirectories.Count <= 0) return new List<FakeDirectory>();
            List<FakeDirectory> dirs = ChildDirectories.FindAll(dir => (dir.Name.Contains(keyWord, StringComparison.OrdinalIgnoreCase)) || (dir.KeyWords.Contains(keyWord, StringComparison.OrdinalIgnoreCase)));
            foreach (FakeDirectory dir in ChildDirectories)
            {
                List<FakeDirectory> subdirs = dir.SearchDirectories(keyWord);
                if (subdirs != null && subdirs.Count > 0)
                {
                    dirs.AddRange(subdirs);
                }
            }
            return dirs;
        }

        public List<FakeFile> SearchFiles(string keyWord)
        {
            List<FakeFile> files = new List<FakeFile>();
            if (ChildFiles.Count > 0) files = ChildFiles.FindAll(file => (file.Name.Contains(keyWord, StringComparison.OrdinalIgnoreCase)));
            if (ChildDirectories.Count > 0)
            {
                foreach (FakeDirectory dir in ChildDirectories)
                {
                    List<FakeFile> subfiles = dir.SearchFiles(keyWord);
                    if (subfiles != null && subfiles.Count > 0)
                    {
                        files.AddRange(subfiles);
                    }
                }
            }
            return files;
        }
    }
}
