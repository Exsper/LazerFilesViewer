using LazerFilesViewer.Localisation;
using Microsoft.VisualBasic.FileIO;
using Microsoft.VisualBasic.Logging;
using osu.Game;
using Realms;
using Realms.Logging;
using System.Buffers;
using System.Collections;
using System.Configuration;
using System.Diagnostics;

using System.Security.Policy;
using System.Text;

namespace LazerFilesViewer
{
    public partial class MainForm : Form
    {
        private ulong newest_schema_version = 43;
        private ulong schema_version = 43;
        private bool shownVersionWarning = false;

        private string TempFolder = AppDomain.CurrentDomain.BaseDirectory + "tmp\\";
        private string BackupFolder = AppDomain.CurrentDomain.BaseDirectory + "Backups\\";

        private string LazerPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\osu\";
        private string LazerFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\osu\files\";
        private string DataBasePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\osu\client.realm";

        private FakeDirectory Songs = new FakeDirectory("Songs", "\\");
        private FakeDirectory Skins = new FakeDirectory("Skins", "\\");

        SelectedItemsList SelectedItemsList;

        ListViewItemComparer listViewItemComparer = new ListViewItemComparer();

        HistoryControl historyControl = new HistoryControl();

        string DeleteWarning = "1";
        string CleanTemp = "1";
        string HideDeleted = "-1";
        string Lang = "";

        private RealmConfiguration GetConfiguration(bool isReadOnly = true)
        {
            return new RealmConfiguration(DataBasePath)
            {
                SchemaVersion = schema_version,
                IsReadOnly = isReadOnly,
                //MigrationCallback = onMigration,
                //FallbackPipePath = tempPathLocation,
            };
        }

        private void BuildDirectories()
        {
            Realm r = Realm.GetInstance(GetConfiguration());

            if (!shownVersionWarning && schema_version < newest_schema_version)
            {
                DialogResult result = MessageBox.Show(Language.GetString("String_Database_Version_Warning"), Language.GetString("String_Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                shownVersionWarning = true;
            }

            var allSkins = r.All<SkinInfo>();
            var allBeatmapSets = r.All<BeatmapSetInfo>();
            /*
            var allFiles = r.All<RealmFile>();
            var allScores = r.All<ScoreInfo>();
            var allRulesetSettings = r.All<RealmRulesetSetting>();
            var allRulesets = r.All<RulesetInfo>();
            var allModPresets = r.All<ModPreset>();
            var allKeyBindings = r.All<RealmKeyBinding>();
            var allBeatmapMetadatas = r.All<BeatmapMetadata>();
            var allBeatmapCollections = r.All<BeatmapCollection>();
            var allBeatmaps = r.All<BeatmapInfo>();
            var allUsers = r.All<RealmUser>();
            var allFileUsages = r.All<RealmNamedFileUsage>();
            var allBeatmapUserSettings = r.All<BeatmapUserSettings>();
            var allBeatmapDifficulty = r.All<BeatmapDifficulty>();
            */


            foreach (var item in allBeatmapSets)
            {
                string title;
                FakeDirectory d;
                if (item.Beatmaps.Count > 0)
                {
                    BeatmapMetadata bm = item.Beatmaps.First().Metadata;
                    title = item.OnlineID + " " + bm.ArtistUnicode + " - " + bm.TitleUnicode;
                    d = Songs.AddDirectory(title, bm.Artist + " " + bm.Title + " " + bm.Author.Username + " " + bm.Tags);
                    d.ID = item.ID;
                }
                else
                {
                    title = item.Hash;
                    d = Songs.AddDirectory(title);
                }
                foreach (var file in item.Files)
                {
                    d.AddFile(file.Filename, file.File.Hash);
                }
            }
            foreach (var item in allSkins)
            {
                string title = item.Name;
                FakeDirectory d = Skins.AddDirectory(title);
                d.ID = item.ID;
                foreach (var file in item.Files)
                {
                    d.AddFile(file.Filename, file.File.Hash);
                }
            }

            r.Dispose();

            AddUpdateAppSettings("LazerPath", LazerPath);
            AddUpdateAppSettings("LazerFilePath", LazerFilePath);
            AddUpdateAppSettings("DataBasePath", DataBasePath);
        }

        private void ReadRealm()
        {
            try
            {
                BuildDirectories();
            }
            catch (Exception ex)
            {
                if (ex.Message.StartsWith("Provided schema version") && schema_version == newest_schema_version)
                {
                    string correctVersion = ex.Message.Split(" ").Last();
                    correctVersion = correctVersion.Substring(0, correctVersion.Length - 1);
                    schema_version = ulong.Parse(correctVersion);
                    if (schema_version != newest_schema_version) { ReadRealm(); return; }
                }
                DialogResult result = MessageBox.Show(Language.GetString("String_Load_Database_Error") + "\r\n" + ex, Language.GetString("String_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (result == DialogResult.OK)
                {
                    Close();
                }
            }
        }

        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        private enum FileListIcons
        {
            Folder = 0,
            File = 1,
            Sound = 2,
            Image = 3,
            video = 4,
        }

        private static FileListIcons GetIconIndex(string fileType)
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
                item.SubItems.Add(Language.GetString("String_Folder"));
                item.SubItems.Add("");
                item.SubItems.Add(subDirectory.FullName);
                item.SubItems.Add("");
            }
            foreach (FakeFile f in d.ChildFiles)
            {
                bool isExists = File.Exists(LazerFilePath + f.GetFilePath());
                if (HideDeleted != "1" || isExists)
                {
                    ListViewItem item = FileListView.Items.Add(f.Name, (int)GetIconIndex(f.GetFileType()));
                    item.Tag = f;
                    item.SubItems.Add(f.GetFileType());
                    item.SubItems.Add(isExists ? Language.GetString("String_Yes") : Language.GetString("String_No"));
                    item.SubItems.Add(f.FullName);
                    item.SubItems.Add(f.GetFilePath());
                }
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
            item.SubItems.Add(Language.GetString("String_Folder"));
            item.SubItems.Add("");
            item.SubItems.Add("");
            item.SubItems.Add("");

            item = FileListView.Items.Add("Skins", (int)FileListIcons.Folder);
            item.Tag = Skins;
            item.SubItems.Add(Language.GetString("String_Folder"));
            item.SubItems.Add("");
            item.SubItems.Add("");
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
            FakeDirectory? d = null;
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


        static void DeleteFolder(string path)
        {
            foreach (string file in Directory.GetFiles(path))
            {
                File.Delete(file);
            }

            foreach (string dir in Directory.GetDirectories(path))
            {
                DeleteFolder(dir);
            }

            Directory.Delete(path);
        }

        #region Language

        private void SetLangText(string? lang)
        {
            Language.SetLocalClutrue(lang);
            this.Text = Language.GetString("Form_Name");
            FileToolStripMenuItem.Text = Language.GetString("ToolStrip_File");
            SetDatabasePathToolStripMenuItem.Text = Language.GetString("ToolStrip_File_SetDatabasePath");
            OpenDatabaseFolderToolStripMenuItem.Text = Language.GetString("ToolStrip_File_OpenDatabaseFolder");
            BackupToolStripMenuItem.Text = Language.GetString("ToolStrip_File_Backup");
            OpenBackupFolderToolStripMenuItem.Text = Language.GetString("ToolStrip_File_OpenBackupFolder");
            ExitToolStripMenuItem.Text = Language.GetString("ToolStrip_File_Exit");
            OptionsStripMenuItem.Text = Language.GetString("ToolStrip_Options");
            LangStripMenuItem.Text = Language.GetString("ToolStrip_Options_Lang");
            DeleteWarningStripMenuItem.Text = Language.GetString("ToolStrip_Options_DeleteWarning");
            HideDeletedStripMenuItem.Text = Language.GetString("ToolStrip_Options_HideDeleted");
            CleanTempStripMenuItem.Text = Language.GetString("ToolStrip_Options_CleanTemp");
            AboutToolStripMenuItem.Text = Language.GetString("ToolStrip_About");
            VisitRepoToolStripMenuItem.Text = Language.GetString("ToolStrip_About_VisitRepo");
            BackToolStripButton.Text = Language.GetString("Button_Back");
            AdvanceToolStripButton.Text = Language.GetString("Button_Forward");
            UpToolStripButton.Text = Language.GetString("Button_Up");
            ReloadToolStripButton.Text = Language.GetString("Button_Reload");
            SearchToolStripComboBox.Text = Language.GetString("Search_Hint");
            NameColumnHeader.Text = Language.GetString("ColumnHeader_Name");
            TypeColumnHeader.Text = Language.GetString("ColumnHeader_Type");
            FileExistColumnHeader.Text = Language.GetString("ColumnHeader_FileExist");
            GamePathColumnHeader.Text = Language.GetString("ColumnHeader_GamePath");
            FilePathColumnHeader.Text = Language.GetString("ColumnHeader_FilePath");
            TSMI_File_Temp_Open.Text = Language.GetString("File_Temp_Open");
            TSMI_File_Temp_Open.ToolTipText = Language.GetString("File_Temp_Open_ToolTipText");
            TSMI_File_GoToFolder.Text = Language.GetString("File_GoToFolder");
            TSMI_File_Open_Txt.Text = Language.GetString("File_Open_Txt");
            TSMI_File_Open_Txt.ToolTipText = Language.GetString("File_Open_Txt_ToolTipText");
            TSMI_File_Temp_Shell.Text = Language.GetString("File_Temp_Shell");
            TSMI_File_Temp_Shell.ToolTipText = Language.GetString("File_Temp_Shell_ToolTipText");
            TSMI_File_EnableMulti_Copy.Text = Language.GetString("File_Export");
            TSMI_File_EnableMulti_Copy.ToolTipText = Language.GetString("File_Export_ToolTipText");
            TSMI_File_Shell.Text = Language.GetString("File_Shell");
            TSMI_File_Shell.ToolTipText = Language.GetString("File_Shell_ToolTipText");
            TSMI_File_OpenFolder.Text = Language.GetString("File_OpenFolder");
            TSMI_File_ReplaceWith.Text = Language.GetString("File_ReplaceWith");
            TSMI_File_EnableMulti_Delete.Text = Language.GetString("File_Delete");
            TSMI_File_EnableMulti_Delete.ToolTipText = Language.GetString("File_Delete_ToolTipText");
            TSMI_Folder_Open.Text = Language.GetString("Folder_Open");
            TSMI_Folder_Open.ToolTipText = Language.GetString("Folder_Open_ToolTipText");
            TSMI_Folder_EnableMulti_Copy.Text = Language.GetString("Folder_Export");
            TSMI_Folder_EnableMulti_Copy.ToolTipText = Language.GetString("Folder_Export_ToolTipText");
            TSMI_Mix_EnableMulti_Copy.Text = Language.GetString("FileFolder_Export");
            TSMI_Mix_EnableMulti_Copy.ToolTipText = Language.GetString("FileFolder_Export_ToolTipText");
            TSMI_Empty_Reload.Text = Language.GetString("Refresh");
            TSMI_Empty_Reload.ToolTipText = Language.GetString("Refresh_ToolTipText");
        }

        private void enUS_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLangText("en-US");
            AddUpdateAppSettings("Lang", "en-US");
        }

        private void zhCN_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLangText("zh-CN");
            AddUpdateAppSettings("Lang", "zh-CN");
        }

        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            Lang = ConfigurationManager.AppSettings["Lang"] ?? "";
            SetLangText(Lang);

            if (File.Exists(LazerPath + "storage.ini"))
            {
                using (var stream = File.Open(LazerPath + "storage.ini", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    if (stream == null)
                        return;

                    using (var reader = new StreamReader(stream))
                    {
                        string line;

                        while ((line = reader.ReadLine()) != null)
                        {
                            int equalsIndex = line.IndexOf('=');

                            if (line.Length == 0 || line[0] == '#' || equalsIndex < 0) continue;

                            string key = line.AsSpan(0, equalsIndex).Trim().ToString();
                            string val = line.AsSpan(equalsIndex + 1).Trim().ToString();

                            if (key == "FullPath" && val != "")
                            {
                                LazerPath = val + @"\";
                                LazerFilePath = val + @"\files\";
                                DataBasePath = val + @"\client.realm";
                                break;
                            }
                        }
                    }
                }
            }

            DeleteWarning = ConfigurationManager.AppSettings["DeleteWarning"] ?? DeleteWarning;
            DeleteWarningStripMenuItem.Checked = (DeleteWarning == "1") ? true : false;
            CleanTemp = ConfigurationManager.AppSettings["CleanTemp"] ?? CleanTemp;
            CleanTempStripMenuItem.Checked = (CleanTemp == "1") ? true : false;
            HideDeleted = ConfigurationManager.AppSettings["HideDeleted"] ?? HideDeleted;
            HideDeletedStripMenuItem.Checked = (HideDeleted == "1") ? true : false;
            if (CleanTemp == "1")
            {
                try
                {
                    if (Directory.Exists(TempFolder))
                    {
                        DeleteFolder(TempFolder);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Language.GetString("String_Clean_Temp_Failed") + "\r\n" + ex, Language.GetString("String_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }



            DataBasePath = ConfigurationManager.AppSettings["DataBasePath"] ?? DataBasePath;
            LazerPath = ConfigurationManager.AppSettings["LazerPath"] ?? LazerPath;
            LazerFilePath = ConfigurationManager.AppSettings["LazerFilePath"] ?? LazerFilePath;
            if (!File.Exists(DataBasePath))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = Language.GetString("String_Open_Database_Filter");
                openFileDialog.Title = Language.GetString("String_Open_Database_Dialog");
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

            ReadRealm();

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
                    if (tag is FakeDirectory fakedirectory)
                    {
                        ShowCurrentDirectory(fakedirectory);
                    }
                    else if (tag is FakeFile fakefile)
                    {
                        string sourcePath = LazerFilePath + fakefile.GetFilePath();
                        string destinationPath = TempFolder + fakefile.Name;
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
                            MessageBox.Show(Language.GetString("String_Create_File_Error") + "\r\n" + ex, Language.GetString("String_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show(Language.GetString("String_Open_File_Error") + "\r\n" + ex, Language.GetString("String_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            TSMI_File_ReplaceWith.Visible = false;
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
            TSMI_File_ReplaceWith.Visible = true;
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
            TSMI_File_ReplaceWith.Visible = false;
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
            TSMI_File_ReplaceWith.Visible = false;
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
            TSMI_File_ReplaceWith.Visible = false;
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
            TSMI_File_ReplaceWith.Visible = false;
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
                TSMI_File_GoToFolder.Visible = false;
            }
            else
            {
                if (FileListView.SelectedItems.Count == 1 && historyControl.GetCurrentPageType() == CurrentPage.Search)
                {
                    TSMI_File_GoToFolder.Visible = true;
                }
                else
                {
                    TSMI_File_GoToFolder.Visible = false;
                }
                List<FakeDirectory> fakeDirectories = new List<FakeDirectory>();
                List<FakeFile> fakeFiles = new List<FakeFile>();
                foreach (ListViewItem item in FileListView.SelectedItems)
                {
                    if (item.Tag != null)
                    {
                        if (item.Tag is FakeDirectory fakedirectory)
                        {
                            fakeDirectories.Add(fakedirectory);
                        }
                        else if (item.Tag is FakeFile fakefile)
                        {
                            fakeFiles.Add(fakefile);
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


        private void Reload(bool reloadDataBase = true)
        {
            HistoryPoint? hp = historyControl.GetCurrentHistoryPoint();
            if (reloadDataBase)
            {
                ReadRealm();
            }
            if (hp == null)
            {
                OpenPath("");
                return;
            }
            if (hp.CurrentPage == CurrentPage.Search)
            {
                Search(hp.Content, true);
            }
            else if (hp.CurrentPage == CurrentPage.Directory)
            {
                OpenPath(hp.Content, true);
            }
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
                    if (tag is FakeFile fakefile)
                    {
                        string sourcePath = LazerFilePath + fakefile.GetFilePath();
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
                    if (tag is FakeFile fakefile)
                    {
                        string sourcePath = LazerFilePath + fakefile.GetFilePath();
                        string destinationPath = TempFolder + fakefile.Name;
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
                            MessageBox.Show(Language.GetString("String_Create_File_Error") + "\r\n" + ex, Language.GetString("String_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            MessageBox.Show(Language.GetString("String_Open_Right_Click_Menu_Error") + "\r\n" + ex, Language.GetString("String_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void TSMI_File_EnableMulti_Copy_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = Language.GetString("String_Export_To_Where");
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
                            MessageBox.Show(Language.GetString("String_Export_Error") + "\r\n" + ex, Language.GetString("String_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (tag is FakeFile fakefile)
                    {
                        string sourcePath = LazerFilePath + fakefile.GetFilePath();
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
                    if (tag is FakeFile fakefile)
                    {
                        string sourcePath = LazerFilePath + fakefile.GetFilePath();
                        OpenFolderAndSelectItems.OpenFolderAndSelectFiles(sourcePath.Substring(0, sourcePath.LastIndexOf("\\")), sourcePath);
                    }
                }
            }
        }

        private void DeleteSelected()
        {
            try
            {
                foreach (FakeFile ff in SelectedItemsList.FakeFiles)
                {
                    string sourcePath = LazerFilePath + ff.GetFilePath();
                    FileSystem.DeleteFile(sourcePath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                }
                Reload(false);
                Process p = new Process();
                p.StartInfo.FileName = "explorer.exe";
                p.StartInfo.Arguments = "shell:RecycleBinFolder";
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.GetString("String_Delete_Error") + "\r\n" + ex, Language.GetString("String_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TSMI_File_EnableMulti_Delete_Click(object sender, EventArgs e)
        {
            if (DeleteWarning == "1")
            {
                DialogResult result = MessageBox.Show(Language.GetString("String_Delete_Warning"), Language.GetString("String_Warning"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    DeleteSelected();
                }
            }
            else
            {
                DeleteSelected();
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
                dialog.Description = Language.GetString("String_Export_To_Where");
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
                            MessageBox.Show(Language.GetString("String_Export_Error") + "\r\n" + ex, Language.GetString("String_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void ReloadToolStripButton_Click(object sender, EventArgs e)
        {
            Reload(true);
        }

        private void TSMI_Mix_EnableMulti_Copy_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = Language.GetString("String_Export_To_Where");
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
                            MessageBox.Show(Language.GetString("String_Export_Error") + "\r\n" + ex, Language.GetString("String_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            else if (historyPoint.CurrentPage == CurrentPage.Search)
            {
                Search(historyPoint.Content, true);
            }
        }

        private void AdvanceToolStripButton_Click(object sender, EventArgs e)
        {
            HistoryPoint historyPoint = historyControl.Move(1);
            if (historyPoint.CurrentPage == CurrentPage.Directory)
            {
                OpenPath(historyPoint.Content, true);
            }
            else if (historyPoint.CurrentPage == CurrentPage.Search)
            {
                Search(historyPoint.Content, true);
            }
        }

        private void Search(string searchText, bool isHistory = false)
        {
            List<FakeDirectory> dirs = Songs.SearchDirectories(searchText);
            dirs.AddRange(Skins.SearchDirectories(searchText));
            List<FakeFile> files = Songs.SearchFiles(searchText);
            files.AddRange(Skins.SearchFiles(searchText));

            FileListView.Items.Clear();
            foreach (FakeDirectory dir in dirs)
            {
                ListViewItem item = FileListView.Items.Add(dir.Name, (int)FileListIcons.Folder);
                item.Tag = dir;
                item.SubItems.Add(Language.GetString("String_Folder"));
                item.SubItems.Add("");
                item.SubItems.Add(dir.FullName);
                item.SubItems.Add("");
            }
            foreach (FakeFile f in files)
            {
                bool isExists = File.Exists(LazerFilePath + f.GetFilePath());
                if (HideDeleted != "1" || isExists)
                {
                    ListViewItem item = FileListView.Items.Add(f.Name, (int)GetIconIndex(f.GetFileType()));
                    item.Tag = f;
                    item.SubItems.Add(f.GetFileType());
                    item.SubItems.Add(isExists ? Language.GetString("String_Yes") : Language.GetString("String_No"));
                    item.SubItems.Add(f.FullName);
                    item.SubItems.Add(f.GetFilePath());
                }
            }
            AddressToolStripComboBox.Text = Language.GetString("String_Search_Result");
            if (!isHistory) historyControl.AddHistory(CurrentPage.Search, searchText);
            CheckButtonEnable();
        }

        private void SearchToolStripComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (SearchToolStripComboBox.Text != "") Search(SearchToolStripComboBox.Text);
            }
        }

        private void SearchToolStripComboBox_Enter(object sender, EventArgs e)
        {
            SearchToolStripComboBox.Text = "";
        }

        private void SearchToolStripComboBox_Leave(object sender, EventArgs e)
        {
            SearchToolStripComboBox.Text = Language.GetString("Search_Hint");
        }

        private void TSMI_File_GoToFolder_Click(object sender, EventArgs e)
        {
            string fullName = FileListView.SelectedItems[0].SubItems[3].Text;
            int index = fullName.LastIndexOf("\\");
            if (index > 0)
            {
                string folderPath = fullName.Substring(0, index);
                string fileName = fullName.Substring(index + 1);
                OpenPath(folderPath);
                foreach (ListViewItem item in FileListView.Items)
                {
                    if (item.SubItems[0].Text == fileName)
                    {
                        item.Selected = true;
                        item.EnsureVisible();
                        break;
                    }
                }
            }
            else
            {
                OpenPath("");
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetDatabasePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = Language.GetString("String_Open_Database_Filter");
            openFileDialog.Title = Language.GetString("String_Select_Database_File");
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DataBasePath = openFileDialog.FileName;
                LazerPath = DataBasePath.Substring(0, DataBasePath.LastIndexOf("\\" + 1));
                LazerFilePath = LazerPath + @"files\";
            }

            ReadRealm();

            OpenPath("");
        }

        private void OpenDatabaseFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sourcePath = DataBasePath;
            OpenFolderAndSelectItems.OpenFolderAndSelectFiles(sourcePath.Substring(0, sourcePath.LastIndexOf("\\")), sourcePath);
        }

        private void BackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sourcePath = DataBasePath;
            string destinationPath = BackupFolder + "client.realm." + DateTime.Now.ToFileTime();
            try
            {
                if (!Directory.Exists(BackupFolder))
                {
                    Directory.CreateDirectory(BackupFolder);
                }
                File.Copy(sourcePath, destinationPath, true);
                OpenFolderAndSelectItems.OpenFolderAndSelectFiles(destinationPath.Substring(0, destinationPath.LastIndexOf("\\")), destinationPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Language.GetString("String_Create_Backup_Error") + "\r\n" + ex, Language.GetString("String_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void OpenBackupFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sourcePath = BackupFolder;
            if (!Directory.Exists(sourcePath))
            {
                Directory.CreateDirectory(sourcePath);
            }
            OpenFolderAndSelectItems.OpenFolderAndSelectFiles(sourcePath);

        }

        private void TSMI_Empty_Reload_Click(object sender, EventArgs e)
        {
            Reload(true);
        }

        private void DeleteWarningStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DeleteWarning == "1") DeleteWarning = "-1";
            else DeleteWarning = "1";
            AddUpdateAppSettings("DeleteWarning", DeleteWarning);
        }

        private void CleanTempStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CleanTemp == "1") CleanTemp = "-1";
            else CleanTemp = "1";
            AddUpdateAppSettings("CleanTemp", CleanTemp);
        }

        private void HideDeletedStripMenuItem_Click(object sender, EventArgs e)
        {
            if (HideDeleted == "1") HideDeleted = "-1";
            else HideDeleted = "1";
            AddUpdateAppSettings("HideDeleted", HideDeleted);
            Reload(false);
        }

        private void VisitRepoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo(@"https://github.com/exsper/LazerFilesViewer") { UseShellExecute = true });
        }



        private async void TSMI_File_ReplaceWith_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = Language.GetString("String_Select_Any_File_Filter");
            openFileDialog.Title = Language.GetString("String_Select_Replace_File");
            openFileDialog.Multiselect = false;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string aimfile = openFileDialog.FileName;
                try
                {
                    string hash = String.Empty;
                    try
                    {
                        hash = Storage.AddToStorage(aimfile, LazerFilePath);
                    }
                    catch(Exception ex)
                    {
                        throw new Exception(Language.GetString("String_Create_File_Error") + "\r\n" + ex);
                    }

                    // TODO: warning

                    Realm r = Realm.GetInstance(GetConfiguration(false));
                    var existing = r.Find<RealmFile>(hash);
                    var rf = existing ?? new RealmFile { Hash = hash };
                    await Database.AddtoRealm(r, rf);

                    var oldFF = SelectedItemsList.FakeFiles[0];
                    if (oldFF == null) throw new Exception("'SelectedItemsList' has error.");
                    if (oldFF.FullName.StartsWith(@"\\Songs"))
                    {
                        var allBeatmapSets = r.All<BeatmapSetInfo>();
                        string beatmapName = oldFF.FullName.Substring(7).Split("\\", StringSplitOptions.RemoveEmptyEntries)[0];
                        var d = Songs.GetDirectory(beatmapName);
                        if (d != null && d.ID != Guid.Empty)
                        {
                            var bsi = allBeatmapSets.ToList().Find((bsi)=> bsi.ID == d.ID);
                            if (bsi != null)
                            {
                                var rnfus = bsi.Files;
                                var rnfu = rnfus.ToList().Find((r) => r.File.Hash == oldFF.Hash);

                                if (rnfu != null) {
                                    await Database.ChangeFile(r, rnfu, rf);
                                }
                                else
                                {
                                    throw new Exception("Couldn't find file hash.");
                                }
                            }
                            else
                            {
                                throw new Exception("Couldn't find beatmap by ID.");
                            }
                        }
                        else
                        {
                            throw new Exception("Couldn't find beatmap by name.");
                        }
                    }
                    else if (oldFF.FullName.StartsWith(@"\\Skins"))
                    {
                        var allSkins = r.All<SkinInfo>();
                        string skinName = oldFF.FullName.Substring(7).Split("\\", StringSplitOptions.RemoveEmptyEntries)[0];
                        var d = Skins.GetDirectory(skinName);
                        if (d != null && d.ID != Guid.Empty)
                        {
                            var si = allSkins.ToList().Find((si) => si.ID == d.ID);
                            if (si != null)
                            {
                                var rnfus = si.Files;
                                var rnfu = rnfus.ToList().Find((r) => r.File.Hash == oldFF.Hash);

                                if (rnfu != null)
                                {
                                    await Database.ChangeFile(r, rnfu, rf);
                                }
                                else
                                {
                                    throw new Exception("Couldn't find file hash.");
                                }
                            }
                            else
                            {
                                throw new Exception("Couldn't find skin by ID.");
                            }
                        }
                        else
                        {
                            throw new Exception("Couldn't find skin by name.");
                        }
                    }
                    r.Refresh();
                    r.Dispose();

                    //TODO: Reload issue - Must restart the program to load new contents.
                    MessageBox.Show(Language.GetString("String_Replace_Complete"), Language.GetString("String_Message"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Reload();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Language.GetString("String_Replace_Error") + "\r\n" + ex, Language.GetString("String_Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
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
        public int Compare(object? x, object? y)
        {
            if (x == null || y == null)
            {
                return 0;
            }
            try
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
            catch
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
        public CurrentPage? GetCurrentPageType()
        {
            if (CurrentIndex >= 0)
            {
                return HistoryPoints[CurrentIndex].CurrentPage;
            }
            else
            {
                return null;
            }
        }
        public HistoryPoint? GetCurrentHistoryPoint()
        {
            if (CurrentIndex >= 0)
            {
                return HistoryPoints[CurrentIndex];
            }
            else
            {
                return null;
            }
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