namespace NewSpecialEvent.DataGrids
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.Entity;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using NewSpecialEvent.Dao;
    using NewSpecialEvent.Dao.ResultCtx;

    public partial class Courses : Form
    {
        private ResultContext context;

        public Courses()
        {
            this.InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.context.Dispose();
        }

        private void CourseBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.context.SaveChanges();
            this.courseDataGridView.Refresh();
        }

        private void Courses_Load(object sender, EventArgs e)
        {
            this.context = new ResultContext();
            this.context.Courses.Load();
            this.courseBindingSource.DataSource = this.context.Courses.Local.ToBindingList();
        } 
    }
}
