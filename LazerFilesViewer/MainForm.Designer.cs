namespace LazerFilesViewer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            FileListView = new ListView();
            NameColumnHeader = new ColumnHeader();
            TypeColumnHeader = new ColumnHeader();
            FileExistColumnHeader = new ColumnHeader();
            GamePathColumnHeader = new ColumnHeader();
            FilePathColumnHeader = new ColumnHeader();
            ViewerContextMenuStrip = new ContextMenuStrip(components);
            TSMI_File_Temp_Open = new ToolStripMenuItem();
            TSMI_File_GoToFolder = new ToolStripMenuItem();
            TSMI_File_Open_Txt = new ToolStripMenuItem();
            TSMI_File_Temp_Shell = new ToolStripMenuItem();
            TSMI_File_EnableMulti_Copy = new ToolStripMenuItem();
            TSMI_File_Shell = new ToolStripMenuItem();
            TSMI_File_OpenFolder = new ToolStripMenuItem();
            TSMI_File_EnableMulti_Delete = new ToolStripMenuItem();
            TSMI_Folder_Open = new ToolStripMenuItem();
            TSMI_Folder_EnableMulti_Copy = new ToolStripMenuItem();
            TSMI_Mix_EnableMulti_Copy = new ToolStripMenuItem();
            TSMI_Empty_Reload = new ToolStripMenuItem();
            IconImageList = new ImageList(components);
            MainToolStrip = new ToolStrip();
            BackToolStripButton = new ToolStripButton();
            AdvanceToolStripButton = new ToolStripButton();
            UpToolStripButton = new ToolStripButton();
            ReloadToolStripButton = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            AddressToolStripComboBox = new ToolStripComboBox();
            toolStripSeparator2 = new ToolStripSeparator();
            SearchToolStripComboBox = new ToolStripComboBox();
            FileToolStripMenuItem = new ToolStripMenuItem();
            SetDatabasePathToolStripMenuItem = new ToolStripMenuItem();
            OpenDatabaseFolderToolStripMenuItem = new ToolStripMenuItem();
            BackupToolStripMenuItem = new ToolStripMenuItem();
            OpenBackupFolderToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            ExitToolStripMenuItem = new ToolStripMenuItem();
            OptionsStripMenuItem = new ToolStripMenuItem();
            LangStripMenuItem = new ToolStripMenuItem();
            enUS_ToolStripMenuItem = new ToolStripMenuItem();
            zhCN_ToolStripMenuItem = new ToolStripMenuItem();
            DeleteWarningStripMenuItem = new ToolStripMenuItem();
            HideDeletedStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            CleanTempStripMenuItem = new ToolStripMenuItem();
            MainFormMenuStrip = new MenuStrip();
            AboutToolStripMenuItem = new ToolStripMenuItem();
            VisitRepoToolStripMenuItem = new ToolStripMenuItem();
            ViewerContextMenuStrip.SuspendLayout();
            MainToolStrip.SuspendLayout();
            MainFormMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // FileListView
            // 
            FileListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            FileListView.Columns.AddRange(new ColumnHeader[] { NameColumnHeader, TypeColumnHeader, FileExistColumnHeader, GamePathColumnHeader, FilePathColumnHeader });
            FileListView.ContextMenuStrip = ViewerContextMenuStrip;
            FileListView.Location = new Point(0, 52);
            FileListView.Name = "FileListView";
            FileListView.Size = new Size(800, 394);
            FileListView.SmallImageList = IconImageList;
            FileListView.TabIndex = 2;
            FileListView.UseCompatibleStateImageBehavior = false;
            FileListView.View = View.Details;
            FileListView.ColumnClick += FileListView_ColumnClick;
            FileListView.ItemActivate += FileListView_ItemActivate;
            // 
            // NameColumnHeader
            // 
            NameColumnHeader.Text = "ColumnHeader_Name";
            NameColumnHeader.Width = 160;
            // 
            // TypeColumnHeader
            // 
            TypeColumnHeader.Text = "ColumnHeader_Type";
            // 
            // FileExistColumnHeader
            // 
            FileExistColumnHeader.Text = "ColumnHeader_FileExist";
            // 
            // GamePathColumnHeader
            // 
            GamePathColumnHeader.Text = "ColumnHeader_GamePath";
            GamePathColumnHeader.Width = 260;
            // 
            // FilePathColumnHeader
            // 
            FilePathColumnHeader.Text = "ColumnHeader_FilePath";
            FilePathColumnHeader.Width = 260;
            // 
            // ViewerContextMenuStrip
            // 
            ViewerContextMenuStrip.Items.AddRange(new ToolStripItem[] { TSMI_File_Temp_Open, TSMI_File_GoToFolder, TSMI_File_Open_Txt, TSMI_File_Temp_Shell, TSMI_File_EnableMulti_Copy, TSMI_File_Shell, TSMI_File_OpenFolder, TSMI_File_EnableMulti_Delete, TSMI_Folder_Open, TSMI_Folder_EnableMulti_Copy, TSMI_Mix_EnableMulti_Copy, TSMI_Empty_Reload });
            ViewerContextMenuStrip.Name = "ViewerContextMenuStrip";
            ViewerContextMenuStrip.Size = new Size(176, 268);
            ViewerContextMenuStrip.Opening += ViewerContextMenuStrip_Opening;
            // 
            // TSMI_File_Temp_Open
            // 
            TSMI_File_Temp_Open.Name = "TSMI_File_Temp_Open";
            TSMI_File_Temp_Open.Size = new Size(175, 22);
            TSMI_File_Temp_Open.Text = "File_Temp_Open";
            TSMI_File_Temp_Open.ToolTipText = "File_Temp_Open_ToolTipText";
            TSMI_File_Temp_Open.Click += TSMI_File_Temp_Open_Click;
            // 
            // TSMI_File_GoToFolder
            // 
            TSMI_File_GoToFolder.Name = "TSMI_File_GoToFolder";
            TSMI_File_GoToFolder.Size = new Size(175, 22);
            TSMI_File_GoToFolder.Text = "File_GoToFolder";
            TSMI_File_GoToFolder.Click += TSMI_File_GoToFolder_Click;
            // 
            // TSMI_File_Open_Txt
            // 
            TSMI_File_Open_Txt.Name = "TSMI_File_Open_Txt";
            TSMI_File_Open_Txt.Size = new Size(175, 22);
            TSMI_File_Open_Txt.Text = "File_Open_Txt";
            TSMI_File_Open_Txt.ToolTipText = "File_Open_Txt_ToolTipText";
            TSMI_File_Open_Txt.Click += TSMI_File_Open_Txt_Click;
            // 
            // TSMI_File_Temp_Shell
            // 
            TSMI_File_Temp_Shell.Name = "TSMI_File_Temp_Shell";
            TSMI_File_Temp_Shell.Size = new Size(175, 22);
            TSMI_File_Temp_Shell.Text = "File_Temp_Shell";
            TSMI_File_Temp_Shell.ToolTipText = "将存储文件以真正文件名复制到临时文件夹并调用系统右键菜单";
            TSMI_File_Temp_Shell.Click += TSMI_File_Temp_Shell_Click;
            // 
            // TSMI_File_EnableMulti_Copy
            // 
            TSMI_File_EnableMulti_Copy.Name = "TSMI_File_EnableMulti_Copy";
            TSMI_File_EnableMulti_Copy.Size = new Size(175, 22);
            TSMI_File_EnableMulti_Copy.Text = "File_Export";
            TSMI_File_EnableMulti_Copy.ToolTipText = "File_Export_ToolTipText";
            TSMI_File_EnableMulti_Copy.Click += TSMI_File_EnableMulti_Copy_Click;
            // 
            // TSMI_File_Shell
            // 
            TSMI_File_Shell.Name = "TSMI_File_Shell";
            TSMI_File_Shell.Size = new Size(175, 22);
            TSMI_File_Shell.Text = "File_Shell";
            TSMI_File_Shell.ToolTipText = "File_Shell_ToolTipText";
            TSMI_File_Shell.Click += TSMI_File_Shell_Click;
            // 
            // TSMI_File_OpenFolder
            // 
            TSMI_File_OpenFolder.Name = "TSMI_File_OpenFolder";
            TSMI_File_OpenFolder.Size = new Size(175, 22);
            TSMI_File_OpenFolder.Text = "File_OpenFolder";
            TSMI_File_OpenFolder.Click += TSMI_File_OpenFolder_Click;
            // 
            // TSMI_File_EnableMulti_Delete
            // 
            TSMI_File_EnableMulti_Delete.ForeColor = Color.Red;
            TSMI_File_EnableMulti_Delete.Name = "TSMI_File_EnableMulti_Delete";
            TSMI_File_EnableMulti_Delete.Size = new Size(175, 22);
            TSMI_File_EnableMulti_Delete.Text = "File_Delete";
            TSMI_File_EnableMulti_Delete.ToolTipText = "File_Delete_ToolTipText";
            TSMI_File_EnableMulti_Delete.Click += TSMI_File_EnableMulti_Delete_Click;
            // 
            // TSMI_Folder_Open
            // 
            TSMI_Folder_Open.Name = "TSMI_Folder_Open";
            TSMI_Folder_Open.Size = new Size(175, 22);
            TSMI_Folder_Open.Text = "Folder_Open";
            TSMI_Folder_Open.ToolTipText = "Folder_Open_ToolTipText";
            TSMI_Folder_Open.Click += TSMI_Folder_Open_Click;
            // 
            // TSMI_Folder_EnableMulti_Copy
            // 
            TSMI_Folder_EnableMulti_Copy.Name = "TSMI_Folder_EnableMulti_Copy";
            TSMI_Folder_EnableMulti_Copy.Size = new Size(175, 22);
            TSMI_Folder_EnableMulti_Copy.Text = "Folder_Export";
            TSMI_Folder_EnableMulti_Copy.ToolTipText = "Folder_Export_ToolTipText";
            TSMI_Folder_EnableMulti_Copy.Click += TSMI_Folder_EnableMulti_Copy_Click;
            // 
            // TSMI_Mix_EnableMulti_Copy
            // 
            TSMI_Mix_EnableMulti_Copy.Name = "TSMI_Mix_EnableMulti_Copy";
            TSMI_Mix_EnableMulti_Copy.Size = new Size(175, 22);
            TSMI_Mix_EnableMulti_Copy.Text = "FileFolder_Export";
            TSMI_Mix_EnableMulti_Copy.ToolTipText = "FileFolder_Export_ToolTipText";
            TSMI_Mix_EnableMulti_Copy.Click += TSMI_Mix_EnableMulti_Copy_Click;
            // 
            // TSMI_Empty_Reload
            // 
            TSMI_Empty_Reload.Name = "TSMI_Empty_Reload";
            TSMI_Empty_Reload.Size = new Size(175, 22);
            TSMI_Empty_Reload.Text = "Refresh";
            TSMI_Empty_Reload.ToolTipText = "Refresh_ToolTipText";
            TSMI_Empty_Reload.Click += TSMI_Empty_Reload_Click;
            // 
            // IconImageList
            // 
            IconImageList.ColorDepth = ColorDepth.Depth8Bit;
            IconImageList.ImageStream = (ImageListStreamer)resources.GetObject("IconImageList.ImageStream");
            IconImageList.TransparentColor = Color.Transparent;
            IconImageList.Images.SetKeyName(0, "folder");
            IconImageList.Images.SetKeyName(1, "file");
            IconImageList.Images.SetKeyName(2, "sound");
            IconImageList.Images.SetKeyName(3, "image");
            IconImageList.Images.SetKeyName(4, "video");
            // 
            // MainToolStrip
            // 
            MainToolStrip.Items.AddRange(new ToolStripItem[] { BackToolStripButton, AdvanceToolStripButton, UpToolStripButton, ReloadToolStripButton, toolStripSeparator1, AddressToolStripComboBox, toolStripSeparator2, SearchToolStripComboBox });
            MainToolStrip.Location = new Point(0, 25);
            MainToolStrip.Name = "MainToolStrip";
            MainToolStrip.Size = new Size(800, 25);
            MainToolStrip.TabIndex = 3;
            MainToolStrip.Text = "toolStrip1";
            // 
            // BackToolStripButton
            // 
            BackToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            BackToolStripButton.Image = (Image)resources.GetObject("BackToolStripButton.Image");
            BackToolStripButton.ImageTransparentColor = Color.Magenta;
            BackToolStripButton.Name = "BackToolStripButton";
            BackToolStripButton.Size = new Size(23, 22);
            BackToolStripButton.Text = "Button_Back";
            BackToolStripButton.Click += BackToolStripButton_Click;
            // 
            // AdvanceToolStripButton
            // 
            AdvanceToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            AdvanceToolStripButton.Image = (Image)resources.GetObject("AdvanceToolStripButton.Image");
            AdvanceToolStripButton.ImageTransparentColor = Color.Magenta;
            AdvanceToolStripButton.Name = "AdvanceToolStripButton";
            AdvanceToolStripButton.Size = new Size(23, 22);
            AdvanceToolStripButton.Text = "Button_Forward";
            AdvanceToolStripButton.Click += AdvanceToolStripButton_Click;
            // 
            // UpToolStripButton
            // 
            UpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            UpToolStripButton.Image = (Image)resources.GetObject("UpToolStripButton.Image");
            UpToolStripButton.ImageTransparentColor = Color.Magenta;
            UpToolStripButton.Name = "UpToolStripButton";
            UpToolStripButton.Size = new Size(23, 22);
            UpToolStripButton.Text = "Button_Up";
            UpToolStripButton.Click += UpToolStripButton_Click;
            // 
            // ReloadToolStripButton
            // 
            ReloadToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ReloadToolStripButton.Image = (Image)resources.GetObject("ReloadToolStripButton.Image");
            ReloadToolStripButton.ImageTransparentColor = Color.Magenta;
            ReloadToolStripButton.Name = "ReloadToolStripButton";
            ReloadToolStripButton.Size = new Size(23, 22);
            ReloadToolStripButton.Text = "Button_Reload";
            ReloadToolStripButton.Click += ReloadToolStripButton_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // AddressToolStripComboBox
            // 
            AddressToolStripComboBox.Name = "AddressToolStripComboBox";
            AddressToolStripComboBox.Size = new Size(450, 25);
            AddressToolStripComboBox.KeyDown += AddressToolStripComboBox_KeyDown;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // SearchToolStripComboBox
            // 
            SearchToolStripComboBox.Name = "SearchToolStripComboBox";
            SearchToolStripComboBox.Size = new Size(220, 25);
            SearchToolStripComboBox.Text = "Search_Hint";
            SearchToolStripComboBox.Enter += SearchToolStripComboBox_Enter;
            SearchToolStripComboBox.Leave += SearchToolStripComboBox_Leave;
            SearchToolStripComboBox.KeyDown += SearchToolStripComboBox_KeyDown;
            // 
            // FileToolStripMenuItem
            // 
            FileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { SetDatabasePathToolStripMenuItem, OpenDatabaseFolderToolStripMenuItem, BackupToolStripMenuItem, OpenBackupFolderToolStripMenuItem, toolStripSeparator3, ExitToolStripMenuItem });
            FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            FileToolStripMenuItem.Size = new Size(97, 21);
            FileToolStripMenuItem.Text = "ToolStrip_File";
            // 
            // SetDatabasePathToolStripMenuItem
            // 
            SetDatabasePathToolStripMenuItem.Name = "SetDatabasePathToolStripMenuItem";
            SetDatabasePathToolStripMenuItem.Size = new Size(282, 22);
            SetDatabasePathToolStripMenuItem.Text = "ToolStrip_File_SetDatabasePath";
            SetDatabasePathToolStripMenuItem.Click += SetDatabasePathToolStripMenuItem_Click;
            // 
            // OpenDatabaseFolderToolStripMenuItem
            // 
            OpenDatabaseFolderToolStripMenuItem.Name = "OpenDatabaseFolderToolStripMenuItem";
            OpenDatabaseFolderToolStripMenuItem.Size = new Size(282, 22);
            OpenDatabaseFolderToolStripMenuItem.Text = "ToolStrip_File_OpenDatabaseFolder";
            OpenDatabaseFolderToolStripMenuItem.Click += OpenDatabaseFolderToolStripMenuItem_Click;
            // 
            // BackupToolStripMenuItem
            // 
            BackupToolStripMenuItem.Name = "BackupToolStripMenuItem";
            BackupToolStripMenuItem.Size = new Size(282, 22);
            BackupToolStripMenuItem.Text = "ToolStrip_File_Backup";
            BackupToolStripMenuItem.Click += BackupToolStripMenuItem_Click;
            // 
            // OpenBackupFolderToolStripMenuItem
            // 
            OpenBackupFolderToolStripMenuItem.Name = "OpenBackupFolderToolStripMenuItem";
            OpenBackupFolderToolStripMenuItem.Size = new Size(282, 22);
            OpenBackupFolderToolStripMenuItem.Text = "ToolStrip_File_OpenBackupFolder";
            OpenBackupFolderToolStripMenuItem.Click += OpenBackupFolderToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(279, 6);
            // 
            // ExitToolStripMenuItem
            // 
            ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            ExitToolStripMenuItem.Size = new Size(282, 22);
            ExitToolStripMenuItem.Text = "退出(&X)";
            ExitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // OptionsStripMenuItem
            // 
            OptionsStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { LangStripMenuItem, DeleteWarningStripMenuItem, HideDeletedStripMenuItem, toolStripSeparator4, CleanTempStripMenuItem });
            OptionsStripMenuItem.Name = "OptionsStripMenuItem";
            OptionsStripMenuItem.Size = new Size(124, 21);
            OptionsStripMenuItem.Text = "ToolStrip_Options";
            // 
            // LangStripMenuItem
            // 
            LangStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { enUS_ToolStripMenuItem, zhCN_ToolStripMenuItem });
            LangStripMenuItem.Name = "LangStripMenuItem";
            LangStripMenuItem.Size = new Size(271, 22);
            LangStripMenuItem.Text = "ToolStrip_Options_Lang";
            // 
            // enUS_ToolStripMenuItem
            // 
            enUS_ToolStripMenuItem.Name = "enUS_ToolStripMenuItem";
            enUS_ToolStripMenuItem.Size = new Size(148, 22);
            enUS_ToolStripMenuItem.Text = "English";
            enUS_ToolStripMenuItem.Click += enUS_ToolStripMenuItem_Click;
            // 
            // zhCN_ToolStripMenuItem
            // 
            zhCN_ToolStripMenuItem.Name = "zhCN_ToolStripMenuItem";
            zhCN_ToolStripMenuItem.Size = new Size(148, 22);
            zhCN_ToolStripMenuItem.Text = "中文（简体）";
            zhCN_ToolStripMenuItem.Click += zhCN_ToolStripMenuItem_Click;
            // 
            // DeleteWarningStripMenuItem
            // 
            DeleteWarningStripMenuItem.Checked = true;
            DeleteWarningStripMenuItem.CheckOnClick = true;
            DeleteWarningStripMenuItem.CheckState = CheckState.Checked;
            DeleteWarningStripMenuItem.Name = "DeleteWarningStripMenuItem";
            DeleteWarningStripMenuItem.Size = new Size(271, 22);
            DeleteWarningStripMenuItem.Text = "ToolStrip_Options_DeleteWarning";
            DeleteWarningStripMenuItem.Click += DeleteWarningStripMenuItem_Click;
            // 
            // HideDeletedStripMenuItem
            // 
            HideDeletedStripMenuItem.CheckOnClick = true;
            HideDeletedStripMenuItem.Name = "HideDeletedStripMenuItem";
            HideDeletedStripMenuItem.Size = new Size(271, 22);
            HideDeletedStripMenuItem.Text = "ToolStrip_Options_HideDeleted";
            HideDeletedStripMenuItem.Click += HideDeletedStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(268, 6);
            // 
            // CleanTempStripMenuItem
            // 
            CleanTempStripMenuItem.Checked = true;
            CleanTempStripMenuItem.CheckOnClick = true;
            CleanTempStripMenuItem.CheckState = CheckState.Checked;
            CleanTempStripMenuItem.Name = "CleanTempStripMenuItem";
            CleanTempStripMenuItem.Size = new Size(271, 22);
            CleanTempStripMenuItem.Text = "ToolStrip_Options_CleanTemp";
            CleanTempStripMenuItem.Click += CleanTempStripMenuItem_Click;
            // 
            // MainFormMenuStrip
            // 
            MainFormMenuStrip.Items.AddRange(new ToolStripItem[] { FileToolStripMenuItem, OptionsStripMenuItem, AboutToolStripMenuItem });
            MainFormMenuStrip.Location = new Point(0, 0);
            MainFormMenuStrip.Name = "MainFormMenuStrip";
            MainFormMenuStrip.Size = new Size(800, 25);
            MainFormMenuStrip.TabIndex = 0;
            MainFormMenuStrip.Text = "menuStrip1";
            // 
            // AboutToolStripMenuItem
            // 
            AboutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { VisitRepoToolStripMenuItem });
            AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            AboutToolStripMenuItem.Size = new Size(113, 21);
            AboutToolStripMenuItem.Text = "ToolStrip_About";
            // 
            // VisitRepoToolStripMenuItem
            // 
            VisitRepoToolStripMenuItem.Name = "VisitRepoToolStripMenuItem";
            VisitRepoToolStripMenuItem.Size = new Size(229, 22);
            VisitRepoToolStripMenuItem.Text = "ToolStrip_About_VisitRepo";
            VisitRepoToolStripMenuItem.Click += VisitRepoToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(MainToolStrip);
            Controls.Add(FileListView);
            Controls.Add(MainFormMenuStrip);
            Name = "MainForm";
            Text = "Form_Name";
            Load += MainForm_Load;
            ViewerContextMenuStrip.ResumeLayout(false);
            MainToolStrip.ResumeLayout(false);
            MainToolStrip.PerformLayout();
            MainFormMenuStrip.ResumeLayout(false);
            MainFormMenuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListView FileListView;
        private ColumnHeader NameColumnHeader;
        private ColumnHeader TypeColumnHeader;
        private ToolStrip MainToolStrip;
        private ToolStripButton BackToolStripButton;
        private ToolStripButton AdvanceToolStripButton;
        private ToolStripButton UpToolStripButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripComboBox AddressToolStripComboBox;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripComboBox SearchToolStripComboBox;
        private ContextMenuStrip ViewerContextMenuStrip;
        private ToolStripMenuItem TSMI_File_Temp_Open;
        private ToolStripMenuItem TSMI_File_Open_Txt;
        private ToolStripMenuItem TSMI_File_Temp_Shell;
        private ToolStripMenuItem TSMI_File_EnableMulti_Copy;
        private ToolStripMenuItem TSMI_File_Shell;
        private ToolStripMenuItem TSMI_File_EnableMulti_Delete;
        private ToolStripMenuItem TSMI_Folder_Open;
        private ToolStripMenuItem TSMI_Folder_EnableMulti_Copy;
        private ToolStripMenuItem TSMI_Mix_EnableMulti_Copy;
        private ToolStripButton ReloadToolStripButton;
        private ToolStripMenuItem TSMI_Empty_Reload;
        private ToolStripMenuItem TSMI_File_OpenFolder;
        private ImageList IconImageList;
        private ColumnHeader GamePathColumnHeader;
        private ColumnHeader FilePathColumnHeader;
        private ToolStripMenuItem TSMI_File_GoToFolder;
        private ColumnHeader FileExistColumnHeader;
        private ToolStripMenuItem FileToolStripMenuItem;
        private ToolStripMenuItem SetDatabasePathToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem ExitToolStripMenuItem;
        private ToolStripMenuItem OptionsStripMenuItem;
        private ToolStripMenuItem LangStripMenuItem;
        private ToolStripMenuItem enUS_ToolStripMenuItem;
        private ToolStripMenuItem zhCN_ToolStripMenuItem;
        private ToolStripMenuItem DeleteWarningStripMenuItem;
        private ToolStripMenuItem HideDeletedStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem CleanTempStripMenuItem;
        private MenuStrip MainFormMenuStrip;
        private ToolStripMenuItem AboutToolStripMenuItem;
        private ToolStripMenuItem VisitRepoToolStripMenuItem;
        private ToolStripMenuItem OpenDatabaseFolderToolStripMenuItem;
        private ToolStripMenuItem BackupToolStripMenuItem;
        private ToolStripMenuItem OpenBackupFolderToolStripMenuItem;
    }
}