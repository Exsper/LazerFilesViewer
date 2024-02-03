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
            MainMenuStrip = new MenuStrip();
            FileToolStripMenuItem = new ToolStripMenuItem();
            SetDatabasePathToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            ExitToolStripMenuItem = new ToolStripMenuItem();
            OptionsStripMenuItem = new ToolStripMenuItem();
            DeleteWarningStripMenuItem = new ToolStripMenuItem();
            CleanTempStripMenuItem = new ToolStripMenuItem();
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
            toolStripSeparator4 = new ToolStripSeparator();
            HideDeletedStripMenuItem = new ToolStripMenuItem();
            MainMenuStrip.SuspendLayout();
            ViewerContextMenuStrip.SuspendLayout();
            MainToolStrip.SuspendLayout();
            SuspendLayout();
            // 
            // MainMenuStrip
            // 
            MainMenuStrip.Items.AddRange(new ToolStripItem[] { FileToolStripMenuItem, OptionsStripMenuItem });
            MainMenuStrip.Location = new Point(0, 0);
            MainMenuStrip.Name = "MainMenuStrip";
            MainMenuStrip.Size = new Size(800, 25);
            MainMenuStrip.TabIndex = 0;
            MainMenuStrip.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            FileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { SetDatabasePathToolStripMenuItem, toolStripSeparator3, ExitToolStripMenuItem });
            FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            FileToolStripMenuItem.Size = new Size(58, 21);
            FileToolStripMenuItem.Text = "文件(&F)";
            // 
            // SetDatabasePathToolStripMenuItem
            // 
            SetDatabasePathToolStripMenuItem.Name = "SetDatabasePathToolStripMenuItem";
            SetDatabasePathToolStripMenuItem.Size = new Size(175, 22);
            SetDatabasePathToolStripMenuItem.Text = "重新选择数据库(&S)";
            SetDatabasePathToolStripMenuItem.Click += SetDatabasePathToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(172, 6);
            // 
            // ExitToolStripMenuItem
            // 
            ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            ExitToolStripMenuItem.Size = new Size(175, 22);
            ExitToolStripMenuItem.Text = "退出(&X)";
            ExitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // OptionsStripMenuItem
            // 
            OptionsStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { DeleteWarningStripMenuItem, HideDeletedStripMenuItem, toolStripSeparator4, CleanTempStripMenuItem });
            OptionsStripMenuItem.Name = "OptionsStripMenuItem";
            OptionsStripMenuItem.Size = new Size(62, 21);
            OptionsStripMenuItem.Text = "选项(&O)";
            // 
            // DeleteWarningStripMenuItem
            // 
            DeleteWarningStripMenuItem.Checked = true;
            DeleteWarningStripMenuItem.CheckOnClick = true;
            DeleteWarningStripMenuItem.CheckState = CheckState.Checked;
            DeleteWarningStripMenuItem.Name = "DeleteWarningStripMenuItem";
            DeleteWarningStripMenuItem.Size = new Size(225, 22);
            DeleteWarningStripMenuItem.Text = "删除时提醒(&D)";
            DeleteWarningStripMenuItem.Click += DeleteWarningStripMenuItem_Click;
            // 
            // CleanTempStripMenuItem
            // 
            CleanTempStripMenuItem.Checked = true;
            CleanTempStripMenuItem.CheckOnClick = true;
            CleanTempStripMenuItem.CheckState = CheckState.Checked;
            CleanTempStripMenuItem.Name = "CleanTempStripMenuItem";
            CleanTempStripMenuItem.Size = new Size(225, 22);
            CleanTempStripMenuItem.Text = "启动时清空临时文件夹(&C)";
            CleanTempStripMenuItem.Click += CleanTempStripMenuItem_Click;
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
            NameColumnHeader.Text = "名称";
            NameColumnHeader.Width = 160;
            // 
            // TypeColumnHeader
            // 
            TypeColumnHeader.Text = "类型";
            // 
            // FileExistColumnHeader
            // 
            FileExistColumnHeader.Text = "文件存在";
            // 
            // GamePathColumnHeader
            // 
            GamePathColumnHeader.Text = "文件路径";
            GamePathColumnHeader.Width = 260;
            // 
            // FilePathColumnHeader
            // 
            FilePathColumnHeader.Text = "存储路径";
            FilePathColumnHeader.Width = 260;
            // 
            // ViewerContextMenuStrip
            // 
            ViewerContextMenuStrip.Items.AddRange(new ToolStripItem[] { TSMI_File_Temp_Open, TSMI_File_GoToFolder, TSMI_File_Open_Txt, TSMI_File_Temp_Shell, TSMI_File_EnableMulti_Copy, TSMI_File_Shell, TSMI_File_OpenFolder, TSMI_File_EnableMulti_Delete, TSMI_Folder_Open, TSMI_Folder_EnableMulti_Copy, TSMI_Mix_EnableMulti_Copy, TSMI_Empty_Reload });
            ViewerContextMenuStrip.Name = "ViewerContextMenuStrip";
            ViewerContextMenuStrip.Size = new Size(221, 268);
            ViewerContextMenuStrip.Opening += ViewerContextMenuStrip_Opening;
            // 
            // TSMI_File_Temp_Open
            // 
            TSMI_File_Temp_Open.Name = "TSMI_File_Temp_Open";
            TSMI_File_Temp_Open.Size = new Size(220, 22);
            TSMI_File_Temp_Open.Text = "打开";
            TSMI_File_Temp_Open.ToolTipText = "将存储文件以真正文件名复制到临时文件夹并使用默认程序打开";
            TSMI_File_Temp_Open.Click += TSMI_File_Temp_Open_Click;
            // 
            // TSMI_File_GoToFolder
            // 
            TSMI_File_GoToFolder.Name = "TSMI_File_GoToFolder";
            TSMI_File_GoToFolder.Size = new Size(220, 22);
            TSMI_File_GoToFolder.Text = "转到文件所在位置";
            TSMI_File_GoToFolder.Click += TSMI_File_GoToFolder_Click;
            // 
            // TSMI_File_Open_Txt
            // 
            TSMI_File_Open_Txt.Name = "TSMI_File_Open_Txt";
            TSMI_File_Open_Txt.Size = new Size(220, 22);
            TSMI_File_Open_Txt.Text = "用 记事本 打开";
            TSMI_File_Open_Txt.ToolTipText = "使用记事本程序打开存储文件";
            TSMI_File_Open_Txt.Click += TSMI_File_Open_Txt_Click;
            // 
            // TSMI_File_Temp_Shell
            // 
            TSMI_File_Temp_Shell.Name = "TSMI_File_Temp_Shell";
            TSMI_File_Temp_Shell.Size = new Size(220, 22);
            TSMI_File_Temp_Shell.Text = "调用系统右键菜单";
            TSMI_File_Temp_Shell.ToolTipText = "将存储文件以真正文件名复制到临时文件夹并调用系统右键菜单";
            TSMI_File_Temp_Shell.Click += TSMI_File_Temp_Shell_Click;
            // 
            // TSMI_File_EnableMulti_Copy
            // 
            TSMI_File_EnableMulti_Copy.Name = "TSMI_File_EnableMulti_Copy";
            TSMI_File_EnableMulti_Copy.Size = new Size(220, 22);
            TSMI_File_EnableMulti_Copy.Text = "导出";
            TSMI_File_EnableMulti_Copy.ToolTipText = "将选中的存储文件以真正文件名复制到目标文件夹";
            TSMI_File_EnableMulti_Copy.Click += TSMI_File_EnableMulti_Copy_Click;
            // 
            // TSMI_File_Shell
            // 
            TSMI_File_Shell.Name = "TSMI_File_Shell";
            TSMI_File_Shell.Size = new Size(220, 22);
            TSMI_File_Shell.Text = "调用存储文件系统右键菜单";
            TSMI_File_Shell.ToolTipText = "调用存储文件的系统右键菜单";
            TSMI_File_Shell.Click += TSMI_File_Shell_Click;
            // 
            // TSMI_File_OpenFolder
            // 
            TSMI_File_OpenFolder.Name = "TSMI_File_OpenFolder";
            TSMI_File_OpenFolder.Size = new Size(220, 22);
            TSMI_File_OpenFolder.Text = "打开存储文件所在文件夹";
            TSMI_File_OpenFolder.ToolTipText = "打开存储文件所在文件夹";
            TSMI_File_OpenFolder.Click += TSMI_File_OpenFolder_Click;
            // 
            // TSMI_File_EnableMulti_Delete
            // 
            TSMI_File_EnableMulti_Delete.Name = "TSMI_File_EnableMulti_Delete";
            TSMI_File_EnableMulti_Delete.Size = new Size(220, 22);
            TSMI_File_EnableMulti_Delete.Text = "删除存储文件";
            TSMI_File_EnableMulti_Delete.ToolTipText = "将选中的存储文件放入回收站\r\n\r\n可能会造成数据库损坏或Lazer程序异常，请小心使用！";
            TSMI_File_EnableMulti_Delete.Click += TSMI_File_EnableMulti_Delete_Click;
            // 
            // TSMI_Folder_Open
            // 
            TSMI_Folder_Open.Name = "TSMI_Folder_Open";
            TSMI_Folder_Open.Size = new Size(220, 22);
            TSMI_Folder_Open.Text = "打开";
            TSMI_Folder_Open.ToolTipText = "打开文件夹";
            TSMI_Folder_Open.Click += TSMI_Folder_Open_Click;
            // 
            // TSMI_Folder_EnableMulti_Copy
            // 
            TSMI_Folder_EnableMulti_Copy.Name = "TSMI_Folder_EnableMulti_Copy";
            TSMI_Folder_EnableMulti_Copy.Size = new Size(220, 22);
            TSMI_Folder_EnableMulti_Copy.Text = "导出";
            TSMI_Folder_EnableMulti_Copy.ToolTipText = "将选中的文件夹及其内部所有存储文件以真正文件名复制到目标文件夹";
            TSMI_Folder_EnableMulti_Copy.Click += TSMI_Folder_EnableMulti_Copy_Click;
            // 
            // TSMI_Mix_EnableMulti_Copy
            // 
            TSMI_Mix_EnableMulti_Copy.Name = "TSMI_Mix_EnableMulti_Copy";
            TSMI_Mix_EnableMulti_Copy.Size = new Size(220, 22);
            TSMI_Mix_EnableMulti_Copy.Text = "导出";
            TSMI_Mix_EnableMulti_Copy.ToolTipText = "将选中的所有存储文件和文件夹以真正文件名复制到目标文件夹";
            TSMI_Mix_EnableMulti_Copy.Click += TSMI_Mix_EnableMulti_Copy_Click;
            // 
            // TSMI_Empty_Reload
            // 
            TSMI_Empty_Reload.Name = "TSMI_Empty_Reload";
            TSMI_Empty_Reload.Size = new Size(220, 22);
            TSMI_Empty_Reload.Text = "刷新";
            TSMI_Empty_Reload.ToolTipText = "重新加载数据库文件并打开当前文件夹";
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
            BackToolStripButton.Text = "返回";
            BackToolStripButton.Click += BackToolStripButton_Click;
            // 
            // AdvanceToolStripButton
            // 
            AdvanceToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            AdvanceToolStripButton.Image = (Image)resources.GetObject("AdvanceToolStripButton.Image");
            AdvanceToolStripButton.ImageTransparentColor = Color.Magenta;
            AdvanceToolStripButton.Name = "AdvanceToolStripButton";
            AdvanceToolStripButton.Size = new Size(23, 22);
            AdvanceToolStripButton.Text = "前进";
            AdvanceToolStripButton.Click += AdvanceToolStripButton_Click;
            // 
            // UpToolStripButton
            // 
            UpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            UpToolStripButton.Image = (Image)resources.GetObject("UpToolStripButton.Image");
            UpToolStripButton.ImageTransparentColor = Color.Magenta;
            UpToolStripButton.Name = "UpToolStripButton";
            UpToolStripButton.Size = new Size(23, 22);
            UpToolStripButton.Text = "上移";
            UpToolStripButton.Click += UpToolStripButton_Click;
            // 
            // ReloadToolStripButton
            // 
            ReloadToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            ReloadToolStripButton.Image = (Image)resources.GetObject("ReloadToolStripButton.Image");
            ReloadToolStripButton.ImageTransparentColor = Color.Magenta;
            ReloadToolStripButton.Name = "ReloadToolStripButton";
            ReloadToolStripButton.Size = new Size(23, 22);
            ReloadToolStripButton.Text = "刷新";
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
            SearchToolStripComboBox.Size = new Size(160, 25);
            SearchToolStripComboBox.Text = "搜索谱面、皮肤和后缀名";
            SearchToolStripComboBox.Enter += SearchToolStripComboBox_Enter;
            SearchToolStripComboBox.Leave += SearchToolStripComboBox_Leave;
            SearchToolStripComboBox.KeyDown += SearchToolStripComboBox_KeyDown;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(222, 6);
            // 
            // HideDeletedStripMenuItem
            // 
            HideDeletedStripMenuItem.CheckOnClick = true;
            HideDeletedStripMenuItem.Name = "HideDeletedStripMenuItem";
            HideDeletedStripMenuItem.Size = new Size(213, 22);
            HideDeletedStripMenuItem.Text = "隐藏不存在的存储文件(&H)";
            HideDeletedStripMenuItem.Click += HideDeletedStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(MainToolStrip);
            Controls.Add(FileListView);
            Controls.Add(MainMenuStrip);
            Name = "MainForm";
            Text = "Lazer文件浏览器";
            Load += MainForm_Load;
            MainMenuStrip.ResumeLayout(false);
            MainMenuStrip.PerformLayout();
            ViewerContextMenuStrip.ResumeLayout(false);
            MainToolStrip.ResumeLayout(false);
            MainToolStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MainMenuStrip;
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
        private ToolStripMenuItem FileToolStripMenuItem;
        private ToolStripMenuItem ExitToolStripMenuItem;
        private ToolStripMenuItem SetDatabasePathToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem OptionsStripMenuItem;
        private ToolStripMenuItem DeleteWarningStripMenuItem;
        private ToolStripMenuItem CleanTempStripMenuItem;
        private ColumnHeader FileExistColumnHeader;
        private ToolStripMenuItem HideDeletedStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
    }
}