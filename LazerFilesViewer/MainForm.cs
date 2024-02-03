using Microsoft.VisualBasic.FileIO;
using osu.Game.Beatmaps;
using osu.Game.Skinning;
using Realms;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LazerFilesViewer
{
    public partial class MainForm : Form
    {
        private const int schema_version = 40;

        private string TempFolder = AppDomain.CurrentDomain.BaseDirectory + "tmp\\";

        private string LazerPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\osu\";
        private string LazerFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\osu\files\";
        private string DataBasePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\osu\client.realm";

        private FakeDirectory Songs = new FakeDirectory("Songs", "\\");
        private FakeDirectory Skins = new FakeDirectory("Skins", "\\");

        SelectedItemsList SelectedItemsList;

        ListViewItemComparer listViewItemComparer = new ListViewItemComparer();

        HistoryControl historyControl = new HistoryControl();

        private RealmConfiguration GetConfiguration()
        {
            return new RealmConfiguration(DataBasePath)
            {
                SchemaVersion = schema_version,
                IsReadOnly = true,
                //MigrationCallback = onMigration,
                //FallbackPipePath = tempPathLocation,
            };
        }

        private void BuildDirectories()
        {
            Realm r = Realm.GetInstance(GetConfiguration());
            var allBeatmaps = r.All<BeatmapSetInfo>();
            foreach (var item in allBeatmaps)
            {
                string title = (item.Beatmaps.Count > 0) ? item.OnlineID + " " + item.Beatmaps.First().Metadata.ArtistUnicode + " - " + item.Beatmaps.First().Metadata.TitleUnicode : item.Hash;
                FakeDirectory d = Songs.AddDirectory(title);
                foreach (var file in item.Files)
                {
                    d.AddFile(file.Filename, file.File.Hash);
                }
            }
            var allSkins = r.All<SkinInfo>();
            foreach (var item in allSkins)
            {
                string title = item.ToString();
                FakeDirectory d = Skins.AddDirectory(title);
                foreach (var file in item.Files)
                {
                    d.AddFile(file.Filename, file.File.Hash);
                }
            }
            r.Dispose();
        }

        private enum FileListIcons
        {
            Folder = 0,
            File = 1,
            Sound = 2,
            Image = 3,
            video = 4,
        }

        private FileListIcons GetIconIndex(string fileType)
        {
            fileType = fileType.ToLower();
            switch (fileType)
            {
                case "jpg":
                case "jpeg":
                case "png":
                case "bmp": return FileListIcons.Image;
                case "mp3":
                case "ogg":
                case "wav": return FileListIcons.Sound;
                case "mp4":
                case "avi":
                case "mkv":
                case "mov": return FileListIcons.video;
                default: return FileListIcons.File;
            }
        }


        private void ShowCurrentDirectory(FakeDirectory d, bool isHistory = false)
        {
            FileListView.Items.Clear();
            foreach (FakeDirectory subDirectory in d.ChildDirectories)
            {
                ListViewItem item = FileListView.Items.Add(subDirectory.Name, (int)FileListIcons.Folder);
                item.Tag = subDirectory;
                item.SubItems.Add("文件夹");
                item.SubItems.Add("");
            }
            foreach (FakeFile f in d.ChildFiles)
            {
                ListViewItem item = FileListView.Items.Add(f.Name, (int)GetIconIndex(f.GetFileType()));
                item.Tag = f;
                item.SubItems.Add(f.GetFileType());
                item.SubItems.Add(f.Hash);
            }
            AddressToolStripComboBox.Text = d.FullName;
            if (!isHistory) historyControl.AddHistory(CurrentPage.Directory, d.FullName);
            CheckButtonEnable();
        }

        private void ShowRootDirectory(bool isHistory = false)
        {
            FileListView.Items.Clear();
            ListViewItem item = FileListView.Items.Add("Songs", (int)FileListIcons.Folder);
            item.Tag = Songs;
            item.SubItems.Add("文件夹");
            item.SubItems.Add("");

            item = FileListView.Items.Add("Skins", (int)FileListIcons.Folder);
            item.Tag = Skins;
            item.SubItems.Add("文件夹");
            item.SubItems.Add("");
            AddressToolStripComboBox.Text = "\\";
            if (!isHistory) historyControl.AddHistory(CurrentPage.Directory, "\\");
            CheckButtonEnable();
        }

        private bool OpenPath(string path, bool isHistory = false)
        {
            if (path == null || path == "")
            {
                ShowRootDirectory(isHistory);
                return true;
            }
            while (path.StartsWith("\\"))
            {
                path = path.Substring(1);
            }
            while (path.EndsWith("\\"))
            {
                path = path.Substring(0, path.Length - 1);
            }
            if (path == "")
            {
                ShowRootDirectory(isHistory);
                return true;
            }
            if (path == "Songs")
            {
                ShowCurrentDirectory(Songs, isHistory);
                return true;
            }
            if (path == "Skins")
            {
                ShowCurrentDirectory(Skins, isHistory);
                return true;
            }
            FakeDirectory d = null;
            if (path.StartsWith("Songs\\"))
            {
                d = Songs.GetDirectory(path.Substring(6));
            }
            if (path.StartsWith("Skins\\"))
            {
                d = Skins.GetDirectory(path.Substring(6));
            }
            if (d != null)
            {
                ShowCurrentDirectory(d, isHistory);
                return true;
            }
            else return false;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!File.Exists(DataBasePath))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Realm数据库 (*.realm)|*.realm";
                openFileDialog.Title = "找不到Lazer数据库文件，请手动选择该文件";
                openFileDialog.Multiselect = false;
                openFileDialog.CheckFileExists = true;
                openFileDialog.CheckPathExists = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    DataBasePath = openFileDialog.FileName;
                    LazerPath = DataBasePath.Substring(0, DataBasePath.LastIndexOf("\\" + 1));
                    LazerFilePath = LazerPath + @"files\";
                }
            }

            try
            {
                BuildDirectories();
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show("读取数据库错误！\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    Close();
                }
            }

            OpenPath("");
        }

        private void FileListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            FileListView.ListViewItemSorter = listViewItemComparer;
            if (e.Column == listViewItemComparer.Col)
            {
                if (listViewItemComparer.Order == SortOrder.Ascending)
                {
                    listViewItemComparer.Order = SortOrder.Descending;
                }
                else
                {
                    listViewItemComparer.Order = SortOrder.Ascending;
                }
            }
            else
            {
                listViewItemComparer.Col = e.Column;
                listViewItemComparer.Order = SortOrder.Ascending;
            }
            FileListView.Sort();
        }

        private void AddressToolStripComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenPath(AddressToolStripComboBox.Text);
            }
        }

        private void DefaultOpen()
        {
            if (FileListView.SelectedItems.Count > 0)
            {
                var tag = FileListView.SelectedItems[0].Tag;

                if (tag != null)
                {
                    if (tag is FakeDirectory)
                    {
                        ShowCurrentDirectory((FakeDirectory)tag);
                    }
                    else if (tag is FakeFile)
                    {
                        string sourcePath = LazerFilePath + ((FakeFile)tag).GetFilePath();
                        string destinationPath = TempFolder + ((FakeFile)tag).Name;
                        try
                        {
                            if (!Directory.Exists(TempFolder))
                            {
                                Directory.CreateDirectory(TempFolder);
                            }
                            File.Copy(sourcePath, destinationPath, true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("创建文件时发生错误\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        try
                        {
                            Process process = new Process();
                            process.StartInfo = new ProcessStartInfo(destinationPath);
                            process.StartInfo.UseShellExecute = true;
                            process.Start();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("系统可能无法打开此类型文件。\r\n请选择“调用系统右键菜单”后自主选择打开方式。\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void FileListView_ItemActivate(object sender, EventArgs e)
        {
            DefaultOpen();
        }

        private void TSMI_Empty()
        {
            TSMI_Empty_Reload.Visible = true;
            TSMI_File_Temp_Open.Visible = false;
            TSMI_File_Open_Txt.Visible = false;
            TSMI_File_Temp_Shell.Visible = false;
            TSMI_File_EnableMulti_Copy.Visible = false;
            TSMI_File_Shell.Visible = false;
            TSMI_File_OpenFolder.Visible = false;
            TSMI_File_EnableMulti_Delete.Visible = false;
            TSMI_Folder_Open.Visible = false;
            TSMI_Folder_EnableMulti_Copy.Visible = false;
            TSMI_Mix_EnableMulti_Copy.Visible = false;
        }

        private void TSMI_File_Single()
        {
            TSMI_Empty_Reload.Visible = false;
            TSMI_File_Temp_Open.Visible = true;
            TSMI_File_Open_Txt.Visible = true;
            TSMI_File_Temp_Shell.Visible = true;
            TSMI_File_EnableMulti_Copy.Visible = true;
            TSMI_File_Shell.Visible = true;
            TSMI_File_OpenFolder.Visible = true;
            TSMI_File_EnableMulti_Delete.Visible = true;
            TSMI_Folder_Open.Visible = false;
            TSMI_Folder_EnableMulti_Copy.Visible = false;
            TSMI_Mix_EnableMulti_Copy.Visible = false;
        }

        private void TSMI_File_Multi()
        {
            TSMI_Empty_Reload.Visible = false;
            TSMI_File_Temp_Open.Visible = false;
            TSMI_File_Open_Txt.Visible = false;
            TSMI_File_Temp_Shell.Visible = false;
            TSMI_File_EnableMulti_Copy.Visible = true;
            TSMI_File_Shell.Visible = false;
            TSMI_File_OpenFolder.Visible = false;
            TSMI_File_EnableMulti_Delete.Visible = true;
            TSMI_Folder_Open.Visible = false;
            TSMI_Folder_EnableMulti_Copy.Visible = false;
            TSMI_Mix_EnableMulti_Copy.Visible = false;
        }

        private void TSMI_Folder_Single()
        {
            TSMI_Empty_Reload.Visible = false;
            TSMI_File_Temp_Open.Visible = false;
            TSMI_File_Open_Txt.Visible = false;
            TSMI_File_Temp_Shell.Visible = false;
            TSMI_File_EnableMulti_Copy.Visible = false;
            TSMI_File_Shell.Visible = false;
            TSMI_File_OpenFolder.Visible = false;
            TSMI_File_EnableMulti_Delete.Visible = false;
            TSMI_Folder_Open.Visible = true;
            TSMI_Folder_EnableMulti_Copy.Visible = true;
            TSMI_Mix_EnableMulti_Copy.Visible = false;
        }

        private void TSMI_Folder_Multi()
        {
            TSMI_Empty_Reload.Visible = false;
            TSMI_File_Temp_Open.Visible = false;
            TSMI_File_Open_Txt.Visible = false;
            TSMI_File_Temp_Shell.Visible = false;
            TSMI_File_EnableMulti_Copy.Visible = false;
            TSMI_File_Shell.Visible = false;
            TSMI_File_OpenFolder.Visible = false;
            TSMI_File_EnableMulti_Delete.Visible = false;
            TSMI_Folder_Open.Visible = false;
            TSMI_Folder_EnableMulti_Copy.Visible = true;
            TSMI_Mix_EnableMulti_Copy.Visible = false;
        }

        private void TSMI_Mix()
        {
            TSMI_Empty_Reload.Visible = false;
            TSMI_File_Temp_Open.Visible = false;
            TSMI_File_Open_Txt.Visible = false;
            TSMI_File_Temp_Shell.Visible = false;
            TSMI_File_EnableMulti_Copy.Visible = false;
            TSMI_File_Shell.Visible = false;
            TSMI_File_OpenFolder.Visible = false;
            TSMI_File_EnableMulti_Delete.Visible = false;
            TSMI_Folder_Open.Visible = false;
            TSMI_Folder_EnableMulti_Copy.Visible = false;
            TSMI_Mix_EnableMulti_Copy.Visible = true;
        }



        private void ViewerContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (FileListView.SelectedItems.Count <= 0)
            {
                TSMI_Empty();
            }
            else
            {
                List<FakeDirectory> fakeDirectories = new List<FakeDirectory>();
                List<FakeFile> fakeFiles = new List<FakeFile>();
                foreach (ListViewItem item in FileListView.SelectedItems)
                {
                    if (item.Tag != null)
                    {
                        if (item.Tag is FakeDirectory)
                        {
                            fakeDirectories.Add(item.Tag as FakeDirectory);
                        }
                        else if (item.Tag is FakeFile)
                        {
                            fakeFiles.Add(item.Tag as FakeFile);
                        }
                    }
                }
                SelectedItemsList = new SelectedItemsList(fakeDirectories, fakeFiles);
                int fdc = fakeDirectories.Count;
                int ffc = fakeFiles.Count;
                if (fdc > 0)
                {
                    if (ffc > 0)
                    {
                        TSMI_Mix();
                    }
                    else if (fdc > 1)
                    {
                        TSMI_Folder_Multi();
                    }
                    else
                    {
                        TSMI_Folder_Single();
                    }
                }
                else if (ffc > 0)
                {
                    if (ffc > 1)
                    {
                        TSMI_File_Multi();
                    }
                    else
                    {
                        TSMI_File_Single();
                    }
                }
                else
                {
                    TSMI_Empty();
                }
            }
        }


        private void Reload()
        {
            string nowpath = AddressToolStripComboBox.Text;
            try
            {
                BuildDirectories();
            }
            catch (Exception ex)
            {
                DialogResult result = MessageBox.Show("读取数据库错误！\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    Close();
                }
            }
            OpenPath(nowpath);
        }

        private void TSMI_File_Temp_Open_Click(object sender, EventArgs e)
        {
            DefaultOpen();
        }

        private void TSMI_File_Open_Txt_Click(object sender, EventArgs e)
        {
            if (FileListView.SelectedItems.Count > 0)
            {
                var tag = FileListView.SelectedItems[0].Tag;

                if (tag != null)
                {
                    if (tag is FakeFile)
                    {
                        string sourcePath = LazerFilePath + ((FakeFile)tag).GetFilePath();
                        Process.Start("notepad.exe", sourcePath);
                    }
                }
            }
        }

        private void TSMI_File_Temp_Shell_Click(object sender, EventArgs e)
        {
            if (FileListView.SelectedItems.Count > 0)
            {
                var tag = FileListView.SelectedItems[0].Tag;

                if (tag != null)
                {
                    if (tag is FakeFile)
                    {
                        string sourcePath = LazerFilePath + ((FakeFile)tag).GetFilePath();
                        string destinationPath = TempFolder + ((FakeFile)tag).Name;
                        try
                        {
                            if (!Directory.Exists(TempFolder))
                            {
                                Directory.CreateDirectory(TempFolder);
                            }
                            File.Copy(sourcePath, destinationPath, true);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("创建文件时发生错误\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        try
                        {
                            ShellContextMenu scm = new ShellContextMenu();
                            FileInfo[] files = new FileInfo[1];
                            files[0] = new FileInfo(destinationPath);
                            scm.ShowContextMenu(files, Cursor.Position);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("打开右键菜单失败\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void TSMI_File_EnableMulti_Copy_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "将文件导出到哪个文件夹？";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string folder = dialog.SelectedPath;
                    if (folder != null && Directory.Exists(folder))
                    {
                        try
                        {
                            List<string> filePaths = new List<string> { };
                            foreach (FakeFile ff in SelectedItemsList.FakeFiles)
                            {
                                string sourcePath = LazerFilePath + ff.GetFilePath();
                                string destinationPath = folder + "\\" + ff.Name;
                                filePaths.Add(destinationPath);
                                File.Copy(sourcePath, destinationPath);
                            }
                            OpenFolderAndSelectItems.OpenFolderAndSelectFiles(folder, filePaths.ToArray());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("导出文件失败\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void TSMI_File_Shell_Click(object sender, EventArgs e)
        {
            if (FileListView.SelectedItems.Count > 0)
            {
                var tag = FileListView.SelectedItems[0].Tag;

                if (tag != null)
                {
                    if (tag is FakeFile)
                    {
                        string sourcePath = LazerFilePath + ((FakeFile)tag).GetFilePath();
                        ShellContextMenu scm = new ShellContextMenu();
                        FileInfo[] files = new FileInfo[1];
                        files[0] = new FileInfo(sourcePath);
                        scm.ShowContextMenu(files, Cursor.Position);
                    }
                }
            }
        }

        private void TSMI_File_OpenFolder_Click(object sender, EventArgs e)
        {
            if (FileListView.SelectedItems.Count > 0)
            {
                var tag = FileListView.SelectedItems[0].Tag;

                if (tag != null)
                {
                    if (tag is FakeFile)
                    {
                        string sourcePath = LazerFilePath + ((FakeFile)tag).GetFilePath();
                        OpenFolderAndSelectItems.OpenFolderAndSelectFiles(sourcePath.Substring(0, sourcePath.LastIndexOf("\\")), sourcePath);
                    }
                }
            }
        }

        private void TSMI_File_EnableMulti_Delete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("删除存储文件可能会造成数据库损坏或Lazer程序异常，请小心使用！\r\n该操作会影响到所有使用该文件的谱面、皮肤等！", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                foreach (FakeFile ff in SelectedItemsList.FakeFiles)
                {
                    string sourcePath = LazerFilePath + ff.GetFilePath();
                    FileSystem.DeleteFile(sourcePath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                }
                Reload();
                Process p = new Process();
                p.StartInfo.FileName = "explorer.exe";
                p.StartInfo.Arguments = "shell:RecycleBinFolder";
                p.Start();
            }
        }

        private void TSMI_Folder_Open_Click(object sender, EventArgs e)
        {
            DefaultOpen();
        }

        private string CopyDirectory(FakeDirectory fd, string destinationRoot)
        {
            string folderPath = destinationRoot + "\\" + fd.Name;
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            foreach (FakeFile sff in fd.ChildFiles)
            {
                string sourcePath = LazerFilePath + sff.GetFilePath();
                string destinationPath = folderPath + "\\" + sff.Name;
                File.Copy(sourcePath, destinationPath);
            }
            foreach (FakeDirectory sfd in fd.ChildDirectories)
            {
                CopyDirectory(sfd, folderPath);
            }
            return folderPath;
        }

        private void TSMI_Folder_EnableMulti_Copy_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "将文件导出到哪个文件夹？";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string folder = dialog.SelectedPath;
                    if (folder != null && Directory.Exists(folder))
                    {
                        try
                        {
                            List<string> folderPaths = new List<string> { };
                            foreach (FakeDirectory fd in SelectedItemsList.FakeDirectories)
                            {
                                folderPaths.Add(CopyDirectory(fd, folder));
                            }
                            OpenFolderAndSelectItems.OpenFolderAndSelectFiles(folder, folderPaths.ToArray());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("导出文件失败\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void ReloadToolStripButton_Click(object sender, EventArgs e)
        {
            Reload();
        }

        private void TSMI_Mix_EnableMulti_Copy_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "将文件导出到哪个文件夹？";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string folder = dialog.SelectedPath;
                    if (folder != null && Directory.Exists(folder))
                    {
                        try
                        {
                            List<string> filePaths = new List<string> { };
                            foreach (FakeDirectory fd in SelectedItemsList.FakeDirectories)
                            {
                                filePaths.Add(CopyDirectory(fd, folder));
                            }
                            foreach (FakeFile ff in SelectedItemsList.FakeFiles)
                            {
                                string sourcePath = LazerFilePath + ff.GetFilePath();
                                string destinationPath = folder + "\\" + ff.Name;
                                filePaths.Add(destinationPath);
                                File.Copy(sourcePath, destinationPath);
                            }
                            OpenFolderAndSelectItems.OpenFolderAndSelectFiles(folder, filePaths.ToArray());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("导出文件失败\r\n" + ex, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void UpToolStripButton_Click(object sender, EventArgs e)
        {
            string nowPath = AddressToolStripComboBox.Text;
            while (nowPath.EndsWith("\\"))
            {
                nowPath = nowPath.Substring(0, nowPath.Length - 1);
            }
            int parent = nowPath.LastIndexOf("\\");
            if (parent <= 0)
            {
                OpenPath("");
            }
            else
            {
                OpenPath(nowPath.Substring(0, parent));
            }
        }

        private void CheckButtonEnable()
        {
            BackToolStripButton.Enabled = historyControl.HasBack();
            AdvanceToolStripButton.Enabled = historyControl.HasFront();
        }

        private void BackToolStripButton_Click(object sender, EventArgs e)
        {
            HistoryPoint historyPoint = historyControl.Move(-1);
            if (historyPoint.CurrentPage == CurrentPage.Directory)
            {
                OpenPath(historyPoint.Content, true);
            }
        }

        private void AdvanceToolStripButton_Click(object sender, EventArgs e)
        {
            HistoryPoint historyPoint = historyControl.Move(1);
            if (historyPoint.CurrentPage == CurrentPage.Directory)
            {
                OpenPath(historyPoint.Content, true);
            }
        }
    }

    public class SelectedItemsList
    {
        public List<FakeDirectory> FakeDirectories { get; set; }
        public List<FakeFile> FakeFiles { get; set; }

        public SelectedItemsList(List<FakeDirectory> fakeDirectories, List<FakeFile> fakeFiles)
        {
            FakeDirectories = fakeDirectories;
            FakeFiles = fakeFiles;
        }

    }

    // Implements the manual sorting of items by columns.
    class ListViewItemComparer : IComparer
    {
        public int Col { get; set; }
        public SortOrder Order { get; set; }
        public ListViewItemComparer()
        {
            Col = 0;
            Order = SortOrder.None;
        }
        public int Compare(object x, object y)
        {
            int result = String.Compare(((ListViewItem)x).SubItems[Col].Text, ((ListViewItem)y).SubItems[Col].Text);
            if (Order == SortOrder.Ascending)
            {
                return result;
            }
            else if (Order == SortOrder.Descending)
            {
                return (-result);
            }
            else
            {
                return 0;
            }
        }
    }

    public enum CurrentPage
    {
        Directory = 0,
        Search = 1,
    }

    public class HistoryPoint
    {
        public CurrentPage CurrentPage { get; set; }
        public string Content { get; set; }
        public HistoryPoint(CurrentPage currentPage, string content)
        {
            CurrentPage = currentPage;
            Content = content;
        }
    }

    public class HistoryControl
    {
        public List<HistoryPoint> HistoryPoints { get; set; }
        public int CurrentIndex = -1;
        public HistoryControl()
        {
            HistoryPoints = new List<HistoryPoint>();
        }
        private void RemoveAfter()
        {
            if (CurrentIndex < 0) return;
            HistoryPoints = HistoryPoints.GetRange(0, CurrentIndex + 1);
        }
        public void AddHistory(CurrentPage currentPage, string content)
        {
            if (CurrentIndex >= 0)
            {
                if (currentPage == HistoryPoints[CurrentIndex].CurrentPage && content == HistoryPoints[CurrentIndex].Content)
                {
                    return;
                }
            }
            HistoryPoint historyPoint = new HistoryPoint(currentPage, content);
            RemoveAfter();
            HistoryPoints.Add(historyPoint);
            CurrentIndex += 1;
        }
        public HistoryPoint Move(int step)
        {
            CurrentIndex += step;
            if (CurrentIndex < 0) CurrentIndex = 0;
            if (CurrentIndex >= HistoryPoints.Count) CurrentIndex = HistoryPoints.Count - 1;
            return HistoryPoints[CurrentIndex];
        }
        public bool HasBack()
        {
            return (CurrentIndex > 0);
        }
        public bool HasFront()
        {
            return (CurrentIndex < HistoryPoints.Count - 1);
        }
    }
}