namespace NewSpecialEvent.TextBoxes
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using NewSpecialEvent.Models;
    using System.Collections.Generic;
    /// <summary>
    /// Show an input box when course is not found
    /// </summary>
    public partial class InputCourseName : Form
    {
        private IList<Course> courses;

        public InputCourseName(IList<Course> courses)
        {
            this.InitializeComponent();
            this.courses = courses;
        }

        /// <summary>
        /// Gets or sets the selected courseId
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Called when the course is saved
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The EventArgs</param>
        private void SaveCourse_Click(object sender, EventArgs e)
        {
            if (this.courseSelectionComboBox.SelectedValue != null)
            {
                this.CourseId = (int)this.courseSelectionComboBox.SelectedValue;
            }
            else
            {
                this.CourseId = -1;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Called when the form loads
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The EventArgs</param>
        private void InputCourseName_Load(object sender, EventArgs e)
        {
            this.courseBindingSource.DataSource = this.courses;
        }
    }
}
