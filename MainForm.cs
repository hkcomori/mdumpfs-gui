using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

using Misuzilla.Tools.mdumpfs;
using Misuzilla.Tools.mdumpfs.Gui.Native;

namespace Misuzilla.Tools.mdumpfs.Gui
{
	/// <summary>
	/// Form1 �̊T�v�̐����ł��B
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ColumnHeader status;
        private System.Windows.Forms.ColumnHeader file;
        private System.Windows.Forms.ColumnHeader dest;
		private System.Windows.Forms.Button buttonBackupDir;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.ComboBox comboBackupDir;
		private System.Windows.Forms.Button buttonDestDir;
		private System.Windows.Forms.ComboBox comboDestDir;
		private System.Windows.Forms.Button buttonCheck;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.Button buttonGo;
		private System.Windows.Forms.ListView listViewLog;
		private System.Windows.Forms.GroupBox groupSetting;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox checkRegex;
		private System.Windows.Forms.NumericUpDown numericLimitDay;
		private System.Windows.Forms.CheckBox checkOnErrorStop;
		private System.Windows.Forms.Label labelPrevDir;
		private System.Windows.Forms.TextBox textExclude;
		private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.LinkLabel linkTaskSchedule;
        /// <summary>
        /// �K�v�ȃf�U�C�i�ϐ��ł��B
        /// </summary>
        private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent �Ăяo���̌�ɁA�R���X�g���N�^ �R�[�h��ǉ����Ă��������B
			//
		}

		/// <summary>
		/// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h 
		/// <summary>
		/// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
		/// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.buttonBackupDir = new System.Windows.Forms.Button();
            this.comboBackupDir = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDestDir = new System.Windows.Forms.Button();
            this.comboDestDir = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupSetting = new System.Windows.Forms.GroupBox();
            this.linkTaskSchedule = new System.Windows.Forms.LinkLabel();
            this.checkRegex = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericLimitDay = new System.Windows.Forms.NumericUpDown();
            this.textExclude = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkOnErrorStop = new System.Windows.Forms.CheckBox();
            this.listViewLog = new System.Windows.Forms.ListView();
            this.status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.file = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonGo = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.labelPrevDir = new System.Windows.Forms.Label();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.dest = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericLimitDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonBackupDir
            // 
            this.buttonBackupDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBackupDir.Location = new System.Drawing.Point(575, 8);
            this.buttonBackupDir.Name = "buttonBackupDir";
            this.buttonBackupDir.Size = new System.Drawing.Size(22, 18);
            this.buttonBackupDir.TabIndex = 5;
            this.buttonBackupDir.Text = "...";
            this.buttonBackupDir.Click += new System.EventHandler(this.buttonBackupDir_Click);
            // 
            // comboBackupDir
            // 
            this.comboBackupDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBackupDir.Location = new System.Drawing.Point(97, 7);
            this.comboBackupDir.Name = "comboBackupDir";
            this.comboBackupDir.Size = new System.Drawing.Size(460, 20);
            this.comboBackupDir.TabIndex = 4;
            this.comboBackupDir.TextChanged += new System.EventHandler(this.comboBackupDir_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "�o�b�N�A�b�v��(&S):";
            // 
            // buttonDestDir
            // 
            this.buttonDestDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDestDir.Location = new System.Drawing.Point(575, 36);
            this.buttonDestDir.Name = "buttonDestDir";
            this.buttonDestDir.Size = new System.Drawing.Size(22, 18);
            this.buttonDestDir.TabIndex = 8;
            this.buttonDestDir.Text = "...";
            this.buttonDestDir.Click += new System.EventHandler(this.buttonDestDir_Click);
            // 
            // comboDestDir
            // 
            this.comboDestDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboDestDir.Location = new System.Drawing.Point(97, 35);
            this.comboDestDir.Name = "comboDestDir";
            this.comboDestDir.Size = new System.Drawing.Size(460, 20);
            this.comboDestDir.TabIndex = 7;
            this.comboDestDir.TextChanged += new System.EventHandler(this.comboDestDir_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "�ۑ���(&D):";
            // 
            // groupSetting
            // 
            this.groupSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupSetting.Controls.Add(this.linkTaskSchedule);
            this.groupSetting.Controls.Add(this.checkRegex);
            this.groupSetting.Controls.Add(this.label4);
            this.groupSetting.Controls.Add(this.numericLimitDay);
            this.groupSetting.Controls.Add(this.textExclude);
            this.groupSetting.Controls.Add(this.label3);
            this.groupSetting.Controls.Add(this.checkOnErrorStop);
            this.groupSetting.Location = new System.Drawing.Point(1, 285);
            this.groupSetting.Name = "groupSetting";
            this.groupSetting.Size = new System.Drawing.Size(598, 107);
            this.groupSetting.TabIndex = 10;
            this.groupSetting.TabStop = false;
            this.groupSetting.Text = "�ڍאݒ�";
            // 
            // linkTaskSchedule
            // 
            this.linkTaskSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkTaskSchedule.Location = new System.Drawing.Point(445, 76);
            this.linkTaskSchedule.Name = "linkTaskSchedule";
            this.linkTaskSchedule.Size = new System.Drawing.Size(143, 19);
            this.linkTaskSchedule.TabIndex = 6;
            this.linkTaskSchedule.TabStop = true;
            this.linkTaskSchedule.Text = "�^�X�N�X�P�W���[���Ƃ��Ēǉ�";
            this.linkTaskSchedule.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTaskSchedule_LinkClicked);
            // 
            // checkRegex
            // 
            this.checkRegex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkRegex.Checked = true;
            this.checkRegex.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkRegex.Enabled = false;
            this.checkRegex.Location = new System.Drawing.Point(507, 20);
            this.checkRegex.Name = "checkRegex";
            this.checkRegex.Size = new System.Drawing.Size(81, 19);
            this.checkRegex.TabIndex = 5;
            this.checkRegex.Text = "���K�\��";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(55, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(202, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "���܂Ńo�b�N�A�b�v�������̂ڂ��Č����B";
            // 
            // numericLimitDay
            // 
            this.numericLimitDay.Location = new System.Drawing.Point(15, 73);
            this.numericLimitDay.Name = "numericLimitDay";
            this.numericLimitDay.Size = new System.Drawing.Size(36, 19);
            this.numericLimitDay.TabIndex = 3;
            this.numericLimitDay.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            // 
            // textExclude
            // 
            this.textExclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textExclude.Location = new System.Drawing.Point(101, 16);
            this.textExclude.Name = "textExclude";
            this.textExclude.Size = new System.Drawing.Size(399, 19);
            this.textExclude.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 19);
            this.label3.TabIndex = 1;
            this.label3.Text = "��������t�@�C��:";
            // 
            // checkOnErrorStop
            // 
            this.checkOnErrorStop.Location = new System.Drawing.Point(16, 45);
            this.checkOnErrorStop.Name = "checkOnErrorStop";
            this.checkOnErrorStop.Size = new System.Drawing.Size(216, 20);
            this.checkOnErrorStop.TabIndex = 0;
            this.checkOnErrorStop.Text = "�G���[�������ɏ����𒆒f����(&E)";
            // 
            // listViewLog
            // 
            this.listViewLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.status,
            this.file,
            this.dest});
            this.listViewLog.FullRowSelect = true;
            this.listViewLog.GridLines = true;
            this.listViewLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewLog.Location = new System.Drawing.Point(1, 99);
            this.listViewLog.MultiSelect = false;
            this.listViewLog.Name = "listViewLog";
            this.listViewLog.Size = new System.Drawing.Size(598, 142);
            this.listViewLog.TabIndex = 11;
            this.listViewLog.UseCompatibleStateImageBehavior = false;
            this.listViewLog.View = System.Windows.Forms.View.Details;
            // 
            // status
            // 
            this.status.Text = "���";
            this.status.Width = 63;
            // 
            // file
            // 
            this.file.Text = "�t�@�C��";
            this.file.Width = 256;
            // 
            // buttonGo
            // 
            this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGo.Enabled = false;
            this.buttonGo.Location = new System.Drawing.Point(488, 252);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(108, 23);
            this.buttonGo.TabIndex = 12;
            this.buttonGo.Text = "���s(&G)";
            this.buttonGo.Click += new System.EventHandler(this.buttonCheckOrGo_Click);
            // 
            // buttonCheck
            // 
            this.buttonCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCheck.Enabled = false;
            this.buttonCheck.Location = new System.Drawing.Point(374, 252);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(108, 23);
            this.buttonCheck.TabIndex = 13;
            this.buttonCheck.Text = "�`�F�b�N(&C)";
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheckOrGo_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // labelPrevDir
            // 
            this.labelPrevDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPrevDir.Location = new System.Drawing.Point(7, 65);
            this.labelPrevDir.Name = "labelPrevDir";
            this.labelPrevDir.Size = new System.Drawing.Size(584, 13);
            this.labelPrevDir.TabIndex = 14;
            this.labelPrevDir.Text = "�O��o�b�N�A�b�v: ";
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 398);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(600, 21);
            this.statusBar.TabIndex = 15;
            // 
            // dest
            // 
            this.dest.Text = "�o�b�N�A�b�v��";
            this.dest.Width = 256;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(600, 419);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.labelPrevDir);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.buttonGo);
            this.Controls.Add(this.listViewLog);
            this.Controls.Add(this.groupSetting);
            this.Controls.Add(this.buttonDestDir);
            this.Controls.Add(this.comboDestDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonBackupDir);
            this.Controls.Add(this.comboBackupDir);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(580, 276);
            this.Name = "MainForm";
            this.Text = "mdumpfs";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupSetting.ResumeLayout(false);
            this.groupSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericLimitDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// �A�v���P�[�V�����̃��C�� �G���g�� �|�C���g�ł��B
		/// </summary>
		[STAThread]
		static void Main(String[] args) 
		{
			// GUI �����ŃR�}���h���C���p�Ɠ����悤�ɓ��삷��B
			// �A���E�B���h�E���o�Ȃ��B
			foreach (String arg in args) 
			{
				if (arg == "-nogui" || arg == "/nogui") 
				{
					Misuzilla.Tools.mdumpfs.ComandLineInterface.Main(args);
					return;
				}
			}

			// GUI
			Application.Run(new MainForm());
		}

		private void buttonBackupDir_Click(object sender, System.EventArgs e)
		{
			folderBrowserDialog.Description = "�o�b�N�A�b�v���̃t�H���_���w�肵�Ă�������";
			folderBrowserDialog.SelectedPath = comboBackupDir.Text;
			if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK) 
			{
				comboBackupDir.Text = folderBrowserDialog.SelectedPath;
				comboBackupDir.Items.Add(comboBackupDir.Text);
			}
		}

		private void buttonDestDir_Click(object sender, System.EventArgs e)
		{
			folderBrowserDialog.Description = "�o�b�N�A�b�v�ۑ���̃t�H���_���w�肵�Ă��������B\r\n�t�H���_��NTFS�{�����[����̕K�v������܂��B";
			folderBrowserDialog.SelectedPath = comboDestDir.Text;
			if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK) 
			{
				comboDestDir.Text = folderBrowserDialog.SelectedPath;
				comboDestDir.Items.Add(comboDestDir.Text);
			}
		}

		private void buttonCheckOrGo_Click(object sender, System.EventArgs e)
		{
			StartupInfo startup = new StartupInfo();
			startup.DestinationRootDirectory = comboDestDir.Text + Path.DirectorySeparatorChar;
			startup.SourceDirectory = comboBackupDir.Text + Path.DirectorySeparatorChar;
			startup.PreviousDateLimit = (Int32)numericLimitDay.Value;
			startup.IgnoreError = checkOnErrorStop.Checked;
			startup.DumpProgress = new DumpProgressHandler(DumpProgressHandled);
			startup.Error = new ErrorHandler(ErrorHandled);
			startup.CheckMode = (sender == buttonCheck);

			if (textExclude.Text.Trim() != "") 
			{
				startup.Exclude = new Regex(textExclude.Text.Trim(), RegexOptions.IgnoreCase);
			}

			listViewLog.Items.Clear();
			listViewLog.BeginUpdate();
			Dumper.Copy(startup);
			listViewLog.EndUpdate();
			statusBar.Text = "�t�@�C���̃o�b�N�A�b�v���������܂���";
		}

		private void ErrorHandled(String reason, String fileName, String srcBaseDir, String destBaseDir, String prevBaseDir)
		{
			if (checkOnErrorStop.Checked) 
			{
				MessageBox.Show(this, 
					"���s���ɃG���[���������܂����B\r\n"+
					"�o�b�N�A�b�v�����͒��f����܂��B\r\n\r\n"+
					"����: \r\n"+
					reason,
					"�o�b�N�A�b�v���̃G���[ - mdumpfs", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			ListViewItem item = new ListViewItem();
			item.Text = "�G���[";
			item.SubItems.Add(srcBaseDir+fileName);
			listViewLog.Items.Add(item);
		}

		private void DumpProgressHandled(Int32 action, String fileName, String srcBaseDir, String destBaseDir, String prevBaseDir)
		{
			ListViewItem item = new ListViewItem();
			switch (action) 
			{
				case 0:
					statusBar.Text = "�f�B���N�g�� \""+srcBaseDir+"\" ��������...";
					break;
				case 1:
					item.Text = "�n�[�h�����N";
					item.SubItems.Add(srcBaseDir+fileName);
					item.SubItems.Add(destBaseDir+fileName);
					statusBar.Text = "�n�[�h�����N \""+fileName+"\" ���쐬��...";
					break;
				case 2:
					item.Text = "�R�s�[";
					item.SubItems.Add(srcBaseDir+fileName);
					item.SubItems.Add(destBaseDir+fileName);
					statusBar.Text = "�R�s�[ \""+fileName+"\" ���쐬��...";
					break;
			}
			if (action != 0) listViewLog.Items.Add(item);
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// �ۑ�
			System.Collections.Specialized.StringDictionary sd = new System.Collections.Specialized.StringDictionary();
			sd.Add("directory.destination", comboDestDir.Text);
			sd.Add("directory.source", comboBackupDir.Text);

			
			// �����B
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			foreach (String s in comboBackupDir.Items) 
				sb.Append(s).Append("\t");
			sd.Add("directory.source.history", sb.ToString());
			sb.Length = 0;

			foreach (String s in comboDestDir.Items) 
				sb.Append(s).Append("\t");
			sd.Add("directory.destination.history", sb.ToString());

			try 
			{
				Misuzilla.Utilities.Setting.SaveSettings("mdumpfs.xml", sd);		
			} 
			catch (IOException ie) 
			{
				MessageBox.Show(ie.ToString(), "mdumpfs", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			// �ǂݍ���
			System.Collections.Specialized.StringDictionary sd = null;
			try 
			{
				sd = Misuzilla.Utilities.Setting.GetSettings("mdumpfs.xml");
			} 
			catch (IOException)
			{
			}

			if (sd == null) return;


			if (sd["directory.source"] != null)
				comboBackupDir.Text = sd["directory.source"];
			if (sd["directory.destination"] != null)
				comboDestDir.Text = sd["directory.destination"];

			if (sd["directory.destination.history"] != null) 
			{
				foreach (String s in sd["directory.destination.history"].Split(new Char[] { '\t' })) 
				{
					if (s.Length != 0) 
					{
						comboDestDir.Items.Add(s);
					}
				}
			}

			if (sd["directory.source.history"] != null) 
			{
				foreach (String s in sd["directory.source.history"].Split(new Char[] { '\t' })) 
				{
					if (s.Length != 0) 
					{
						comboBackupDir.Items.Add(s);
					}
				}
			}
		}

		private void comboDestDir_TextChanged(object sender, System.EventArgs e)
		{
			buttonCheck.Enabled = buttonGo.Enabled = false;

			// check
			if (!System.IO.Directory.Exists(comboDestDir.Text)) 
			{
				// directory not found
				errorProvider.SetError(comboDestDir, "�ۑ���t�H���_��������܂���B");
			}
			else if (!FileUtility.IsNTFS(comboDestDir.Text)) 
			{
				// not ntfs
				errorProvider.SetError(comboDestDir, "�o�b�N�A�b�v�ۑ��悪NTFS�{�����[���ł͂���܂���B");
			} 
			else 
			{
				// ok
				errorProvider.SetError(comboDestDir, "");
				labelPrevDir.Text = "�O��o�b�N�A�b�v: "+Dumper.GetPreviousSnap(
					comboDestDir.Text, DateTime.Now, (Int32)numericLimitDay.Value);
				if (errorProvider.GetError(comboBackupDir) == "")
				{
					buttonCheck.Enabled = buttonGo.Enabled = true;
				}
			}		
		}

		private void comboBackupDir_TextChanged(object sender, System.EventArgs e)
		{
			buttonCheck.Enabled = buttonGo.Enabled = false;
		
			// check
			if (!System.IO.Directory.Exists(comboBackupDir.Text)) 
			{
				// directory not found
				errorProvider.SetError(comboBackupDir, "�o�b�N�A�b�v���t�H���_��������܂���B");
			}
			else 
			{
				// ok
				errorProvider.SetError(comboBackupDir, "");
				buttonCheck.Enabled = buttonGo.Enabled = (errorProvider.GetError(comboDestDir) == "");
			}		
		}

		private void linkTaskSchedule_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			ITask task = TaskSchedulerFactory.CreateTask();
			ITaskScheduler tasksched = TaskSchedulerFactory.CreateTaskScheduler();
			String taskName = "mdumpfs - Backup Task";
			Int32 i = 0;

			// param
			System.Text.StringBuilder sbParam = new System.Text.StringBuilder();
			if (textExclude.Text.Trim().Length > 0)
				sbParam.Append(" /re \""+textExclude.Text.Trim()+"\"");
			sbParam.Append(" /nogui /l "+numericLimitDay.Value);

			// �^�X�N�X�P�W���[���A�C�e�� �ݒ�
			task.SetApplicationName(Application.ExecutablePath);
			task.SetComment("�o�b�N�A�b�v��: "+comboBackupDir.Text+"\r\n"+"�ۑ���: "+comboDestDir.Text);
			task.SetParameters(
				String.Format("\"{0}\" \"{1}\" {2}",
					comboBackupDir.Text.TrimEnd(new Char[] {Path.DirectorySeparatorChar}),
					comboDestDir.Text.TrimEnd(new Char[] {Path.DirectorySeparatorChar}), sbParam.ToString()));

			// �^�X�N�X�P�W���[���A�C�e�� �ۑ�
			while (true) 
			{
				try 
				{
					tasksched.AddWorkItem(taskName, task);
					break;
				} 
				catch (System.Runtime.InteropServices.COMException ex)
				{
					if (ex.ErrorCode == -2147024816) 
					{
						// �t�@�C��������B
						taskName = "mdumpfs - Backup Task ("+ (i++) +")";
					} 
					else 
					{
						// �s���ȃG���[
						return;
					}
				}
			}

			// �^�X�N�X�P�W���[���A�C�e�� �_�C�A���O�\��
			if (task != null) 
			{
				task.EditWorkItem(this.Handle, 0);
			}
			//tasksched.NewWorkItem("Test Task", Guids.CTask, Guids.ITask, out taskItem);
		}

	}
}
