namespace NewSpecialEvent
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

    public partial class Teams : Form
    {
        private ResultContext context;

        public Teams()
        {
            this.InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.context.Dispose();
        }

        private void TeamBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.context.SaveChanges();
            this.teamDataGridView.Refresh();
        }

        private void Teams_Load(object sender, EventArgs e)
        {
            this.context = new ResultContext();
            this.context.Teams.Load();
            this.teamBindingSource.DataSource = this.context.Teams.Local.ToBindingList();
        }
    }
}
