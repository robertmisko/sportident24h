using System.Globalization;

namespace NewSpecialEvent
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem entriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem teamsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printerSettingsToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox courseFileTextBox;
        private System.Windows.Forms.Button courseBrowseBtn;
        private System.Windows.Forms.Button courseReaderBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox zeroTextBox;
        private System.Windows.Forms.Button zeroBtn;
        private System.Windows.Forms.RichTextBox infoTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox teamTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox timeTextBox;
        private System.Windows.Forms.TextBox finishtimeTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox courseTextBox;
        private System.Windows.Forms.TextBox categoryTextBox;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.ToolStripMenuItem coursesToolStripMenuItem;
        private System.Windows.Forms.Button printResultBtn;
        private System.Windows.Forms.Label printLabel;
        private System.Windows.Forms.OpenFileDialog openCourseDialog;
        private System.Windows.Forms.ToolStripMenuItem resultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resultsByCourseToolStripMenuItem;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.entriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.teamsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coursesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printerSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resultsByCourseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.courseFileTextBox = new System.Windows.Forms.TextBox();
            this.courseBrowseBtn = new System.Windows.Forms.Button();
            this.courseReaderBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.zeroTextBox = new System.Windows.Forms.TextBox();
            this.zeroBtn = new System.Windows.Forms.Button();
            this.infoTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.categoryTextBox = new System.Windows.Forms.TextBox();
            this.courseTextBox = new System.Windows.Forms.TextBox();
            this.timeTextBox = new System.Windows.Forms.TextBox();
            this.finishtimeTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.teamTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printResultBtn = new System.Windows.Forms.Button();
            this.printLabel = new System.Windows.Forms.Label();
            this.openCourseDialog = new System.Windows.Forms.OpenFileDialog();
            this.grpInputDevice = new System.Windows.Forms.GroupBox();
            this.DeviceListRefresh = new System.Windows.Forms.Button();
            this.cmbSerialPort = new System.Windows.Forms.ComboBox();
            this.rdoInputSerialPort = new System.Windows.Forms.RadioButton();
            this.btnOpen = new System.Windows.Forms.Button();
            this.printEnabled = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpInputDevice.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.entriesToolStripMenuItem,
            this.teamsToolStripMenuItem,
            this.coursesToolStripMenuItem,
            this.printerSettingsToolStripMenuItem,
            this.resultsToolStripMenuItem,
            this.resultsByCourseToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1185, 24);
            this.menuStrip1.TabIndex = 0;
            // 
            // entriesToolStripMenuItem
            // 
            this.entriesToolStripMenuItem.Name = "entriesToolStripMenuItem";
            this.entriesToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.entriesToolStripMenuItem.Text = global::NewSpecialEvent.Resources.Default.Entries;
            this.entriesToolStripMenuItem.Click += new System.EventHandler(this.EntriesToolStripMenuItem_Click);
            // 
            // teamsToolStripMenuItem
            // 
            this.teamsToolStripMenuItem.Name = "teamsToolStripMenuItem";
            this.teamsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.teamsToolStripMenuItem.Text = global::NewSpecialEvent.Resources.Default.Teams;
            this.teamsToolStripMenuItem.Click += new System.EventHandler(this.TeamsToolStripMenuItem_Click);
            // 
            // coursesToolStripMenuItem
            // 
            this.coursesToolStripMenuItem.Name = "coursesToolStripMenuItem";
            this.coursesToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.coursesToolStripMenuItem.Text = global::NewSpecialEvent.Resources.Default.Courses;
            this.coursesToolStripMenuItem.Click += new System.EventHandler(this.CoursesToolStripMenuItem_Click);
            // 
            // printerSettingsToolStripMenuItem
            // 
            this.printerSettingsToolStripMenuItem.Name = "printerSettingsToolStripMenuItem";
            this.printerSettingsToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
            this.printerSettingsToolStripMenuItem.Text = global::NewSpecialEvent.Resources.Default.PrinterSettings;
            this.printerSettingsToolStripMenuItem.Click += new System.EventHandler(this.PrinterSettingsToolStripMenuItem_Click);
            // 
            // resultsToolStripMenuItem
            // 
            this.resultsToolStripMenuItem.Name = "resultsToolStripMenuItem";
            this.resultsToolStripMenuItem.Size = new System.Drawing.Size(123, 20);
            this.resultsToolStripMenuItem.Text = global::NewSpecialEvent.Resources.Default.ResultsByCategory;
            this.resultsToolStripMenuItem.Click += new System.EventHandler(this.ResultsToolStripMenuItem_Click);
            // 
            // resultsByCourseToolStripMenuItem
            // 
            this.resultsByCourseToolStripMenuItem.Name = "resultsByCourseToolStripMenuItem";
            this.resultsByCourseToolStripMenuItem.Size = new System.Drawing.Size(112, 20);
            this.resultsByCourseToolStripMenuItem.Text = global::NewSpecialEvent.Resources.Default.ResultsByCourse;
            this.resultsByCourseToolStripMenuItem.Click += new System.EventHandler(this.ResultsByCourseToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Course File";
            // 
            // courseFileTextBox
            // 
            this.courseFileTextBox.Location = new System.Drawing.Point(96, 36);
            this.courseFileTextBox.Name = "courseFileTextBox";
            this.courseFileTextBox.Size = new System.Drawing.Size(347, 20);
            this.courseFileTextBox.TabIndex = 2;
            // 
            // courseBrowseBtn
            // 
            this.courseBrowseBtn.Location = new System.Drawing.Point(458, 33);
            this.courseBrowseBtn.Name = "courseBrowseBtn";
            this.courseBrowseBtn.Size = new System.Drawing.Size(88, 23);
            this.courseBrowseBtn.TabIndex = 3;
            this.courseBrowseBtn.Text = global::NewSpecialEvent.Resources.Default.Browse;
            this.courseBrowseBtn.UseVisualStyleBackColor = true;
            this.courseBrowseBtn.Click += new System.EventHandler(this.CourseBrowseBtn_Click);
            // 
            // courseReaderBtn
            // 
            this.courseReaderBtn.Location = new System.Drawing.Point(96, 62);
            this.courseReaderBtn.Name = "courseReaderBtn";
            this.courseReaderBtn.Size = new System.Drawing.Size(347, 23);
            this.courseReaderBtn.TabIndex = 4;
            this.courseReaderBtn.Text = global::NewSpecialEvent.Resources.Default.ReadCourses;
            this.courseReaderBtn.UseVisualStyleBackColor = true;
            this.courseReaderBtn.Click += new System.EventHandler(this.CourseReaderBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(596, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Zero Time";
            // 
            // zeroTextBox
            // 
            this.zeroTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.zeroTextBox.Location = new System.Drawing.Point(699, 28);
            this.zeroTextBox.Name = "zeroTextBox";
            this.zeroTextBox.Size = new System.Drawing.Size(208, 29);
            this.zeroTextBox.TabIndex = 6;
            this.zeroTextBox.Text = "2016/07/02 10:00:00";
            // 
            // zeroBtn
            // 
            this.zeroBtn.Location = new System.Drawing.Point(699, 63);
            this.zeroBtn.Name = "zeroBtn";
            this.zeroBtn.Size = new System.Drawing.Size(208, 23);
            this.zeroBtn.TabIndex = 7;
            this.zeroBtn.Text = global::NewSpecialEvent.Resources.Default.SetZeroTime;
            this.zeroBtn.UseVisualStyleBackColor = true;
            this.zeroBtn.Click += new System.EventHandler(this.ZeroBtn_Click);
            // 
            // infoTextBox
            // 
            this.infoTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.infoTextBox.Location = new System.Drawing.Point(12, 178);
            this.infoTextBox.Name = "infoTextBox";
            this.infoTextBox.Size = new System.Drawing.Size(1158, 399);
            this.infoTextBox.TabIndex = 8;
            this.infoTextBox.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.categoryTextBox);
            this.groupBox1.Controls.Add(this.courseTextBox);
            this.groupBox1.Controls.Add(this.timeTextBox);
            this.groupBox1.Controls.Add(this.finishtimeTextBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.teamTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.nameTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 583);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1161, 106);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Info";
            // 
            // categoryTextBox
            // 
            this.categoryTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.categoryTextBox.Location = new System.Drawing.Point(562, 65);
            this.categoryTextBox.Name = "categoryTextBox";
            this.categoryTextBox.Size = new System.Drawing.Size(147, 26);
            this.categoryTextBox.TabIndex = 11;
            // 
            // courseTextBox
            // 
            this.courseTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.courseTextBox.Location = new System.Drawing.Point(562, 22);
            this.courseTextBox.Name = "courseTextBox";
            this.courseTextBox.Size = new System.Drawing.Size(147, 26);
            this.courseTextBox.TabIndex = 10;
            // 
            // timeTextBox
            // 
            this.timeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.timeTextBox.Location = new System.Drawing.Point(835, 65);
            this.timeTextBox.Name = "timeTextBox";
            this.timeTextBox.Size = new System.Drawing.Size(193, 26);
            this.timeTextBox.TabIndex = 11;
            // 
            // finishtimeTextBox
            // 
            this.finishtimeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.finishtimeTextBox.Location = new System.Drawing.Point(835, 25);
            this.finishtimeTextBox.Name = "finishtimeTextBox";
            this.finishtimeTextBox.Size = new System.Drawing.Size(193, 26);
            this.finishtimeTextBox.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label8.Location = new System.Drawing.Point(736, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 20);
            this.label8.TabIndex = 11;
            this.label8.Text = "Time";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(736, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "Finish Time";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(468, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "Category";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(468, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "Course";
            // 
            // teamTextBox
            // 
            this.teamTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.teamTextBox.Location = new System.Drawing.Point(83, 71);
            this.teamTextBox.Name = "teamTextBox";
            this.teamTextBox.Size = new System.Drawing.Size(329, 26);
            this.teamTextBox.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(6, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Team";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Name";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.nameTextBox.Location = new System.Drawing.Point(83, 22);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(329, 26);
            this.nameTextBox.TabIndex = 0;
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // printResultBtn
            // 
            this.printResultBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.printResultBtn.Location = new System.Drawing.Point(15, 97);
            this.printResultBtn.Name = "printResultBtn";
            this.printResultBtn.Size = new System.Drawing.Size(75, 49);
            this.printResultBtn.TabIndex = 12;
            this.printResultBtn.Text = global::NewSpecialEvent.Resources.Default.PrintResults;
            this.printResultBtn.UseVisualStyleBackColor = true;
            this.printResultBtn.Click += new System.EventHandler(this.PrintResultBtn_Click);
            // 
            // printLabel
            // 
            this.printLabel.AutoSize = true;
            this.printLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.printLabel.Location = new System.Drawing.Point(96, 104);
            this.printLabel.Name = "printLabel";
            this.printLabel.Size = new System.Drawing.Size(26, 29);
            this.printLabel.TabIndex = 13;
            this.printLabel.Text = "0";
            // 
            // openCourseDialog
            // 
            this.openCourseDialog.FileName = "openFileDialog1";
            // 
            // grpInputDevice
            // 
            this.grpInputDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpInputDevice.Controls.Add(this.DeviceListRefresh);
            this.grpInputDevice.Controls.Add(this.cmbSerialPort);
            this.grpInputDevice.Controls.Add(this.rdoInputSerialPort);
            this.grpInputDevice.Location = new System.Drawing.Point(565, 91);
            this.grpInputDevice.Name = "grpInputDevice";
            this.grpInputDevice.Size = new System.Drawing.Size(569, 81);
            this.grpInputDevice.TabIndex = 96;
            this.grpInputDevice.TabStop = false;
            this.grpInputDevice.Text = "Input device";
            // 
            // DeviceListRefresh
            // 
            this.DeviceListRefresh.Location = new System.Drawing.Point(172, 51);
            this.DeviceListRefresh.Name = "DeviceListRefresh";
            this.DeviceListRefresh.Size = new System.Drawing.Size(75, 23);
            this.DeviceListRefresh.TabIndex = 100;
            this.DeviceListRefresh.Text = "Refresh";
            this.DeviceListRefresh.UseVisualStyleBackColor = true;
            this.DeviceListRefresh.Click += new System.EventHandler(this.DeviceListRefresh_Click);
            // 
            // cmbSerialPort
            // 
            this.cmbSerialPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSerialPort.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbSerialPort.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cmbSerialPort.FormattingEnabled = true;
            this.cmbSerialPort.Location = new System.Drawing.Point(172, 24);
            this.cmbSerialPort.Name = "cmbSerialPort";
            this.cmbSerialPort.Size = new System.Drawing.Size(391, 21);
            this.cmbSerialPort.TabIndex = 96;
            // 
            // rdoInputSerialPort
            // 
            this.rdoInputSerialPort.BackColor = System.Drawing.SystemColors.Window;
            this.rdoInputSerialPort.Checked = true;
            this.rdoInputSerialPort.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rdoInputSerialPort.Location = new System.Drawing.Point(15, 25);
            this.rdoInputSerialPort.Name = "rdoInputSerialPort";
            this.rdoInputSerialPort.Size = new System.Drawing.Size(151, 18);
            this.rdoInputSerialPort.TabIndex = 92;
            this.rdoInputSerialPort.TabStop = true;
            this.rdoInputSerialPort.Text = "SPORTident device";
            this.rdoInputSerialPort.UseVisualStyleBackColor = false;
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnOpen.Location = new System.Drawing.Point(950, 39);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(152, 46);
            this.btnOpen.TabIndex = 100;
            this.btnOpen.Text = "Open/Close";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // printEnabled
            // 
            this.printEnabled.AutoSize = true;
            this.printEnabled.Location = new System.Drawing.Point(15, 152);
            this.printEnabled.Name = "printEnabled";
            this.printEnabled.Size = new System.Drawing.Size(89, 17);
            this.printEnabled.TabIndex = 101;
            this.printEnabled.Text = "Print Enabled";
            this.printEnabled.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1185, 701);
            this.Controls.Add(this.printEnabled);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.grpInputDevice);
            this.Controls.Add(this.printLabel);
            this.Controls.Add(this.printResultBtn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.infoTextBox);
            this.Controls.Add(this.zeroBtn);
            this.Controls.Add(this.zeroTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.courseReaderBtn);
            this.Controls.Add(this.courseBrowseBtn);
            this.Controls.Add(this.courseFileTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Hungarian 24H O-Competition";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpInputDevice.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpInputDevice;
        private System.Windows.Forms.Button DeviceListRefresh;
        internal System.Windows.Forms.ComboBox cmbSerialPort;
        private System.Windows.Forms.RadioButton rdoInputSerialPort;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.CheckBox printEnabled;
    }
}