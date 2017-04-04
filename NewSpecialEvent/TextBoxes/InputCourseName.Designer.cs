using System.Globalization;
using System.Reflection;
using System.Resources;

namespace NewSpecialEvent.TextBoxes
{
    partial class InputCourseName
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.courseSelectionComboBox = new System.Windows.Forms.ComboBox();
            this.courseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.saveCourse = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.courseBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(18, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(357, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please enter the name of this course!";
            // 
            // courseSelectionComboBox
            // 
            this.courseSelectionComboBox.DataSource = this.courseBindingSource;
            this.courseSelectionComboBox.DisplayMember = "Name";
            this.courseSelectionComboBox.FormattingEnabled = true;
            this.courseSelectionComboBox.Location = new System.Drawing.Point(91, 36);
            this.courseSelectionComboBox.Name = "courseSelectionComboBox";
            this.courseSelectionComboBox.Size = new System.Drawing.Size(199, 21);
            this.courseSelectionComboBox.TabIndex = 1;
            this.courseSelectionComboBox.ValueMember = "Id";
            // 
            // courseBindingSource
            // 
            this.courseBindingSource.DataSource = typeof(NewSpecialEvent.Models.Course);
            // 
            // saveCourse
            // 
            this.saveCourse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.saveCourse.Location = new System.Drawing.Point(140, 63);
            this.saveCourse.Name = "saveCourse";
            this.saveCourse.Size = new System.Drawing.Size(75, 29);
            this.saveCourse.TabIndex = 2;
            this.saveCourse.Text = global::NewSpecialEvent.Resources.Default.OK;
            this.saveCourse.UseVisualStyleBackColor = true;
            this.saveCourse.Click += new System.EventHandler(this.SaveCourse_Click);
            // 
            // InputCourseName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 101);
            this.Controls.Add(this.saveCourse);
            this.Controls.Add(this.courseSelectionComboBox);
            this.Controls.Add(this.label1);
            this.Name = "InputCourseName";
            this.Text = "Course is not found!";
            this.Load += new System.EventHandler(this.InputCourseName_Load);
            ((System.ComponentModel.ISupportInitialize)(this.courseBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox courseSelectionComboBox;
        private System.Windows.Forms.Button saveCourse;
        private System.Windows.Forms.BindingSource courseBindingSource;
    }
}