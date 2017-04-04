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
    using NewSpecialEvent.Dao.ResultCtx;
    using Common;
    using Models;
    public partial class Entries : Form
    {
        private ResultContext context;
        private SortableBindingList<Runner> bindingList;

        public Entries()
        {
            this.InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.context.Dispose();
        }

        private void RunnerBindingSource_CurrentChanged(object sender, EventArgs e)
        {
        }

        private void BindingNavigator1_RefreshItems(object sender, EventArgs e)
        {
        }

        private void Entries_Load(object sender, EventArgs e)
        {
            this.context = new ResultContext();
            this.context.Runners.Load();
            this.bindingList = new SortableBindingList<Runner>(this.context.Runners.Local.ToList());
            this.runnerBindingSource.DataSource = this.bindingList;
            this.bindingNavigator1.BindingSource = this.runnerBindingSource;
            this.context.Teams.Load();
            this.teamBindingSource.DataSource = this.context.Teams.Local.ToBindingList().OrderBy(t => t.Name);
        }

        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            this.Validate();
            foreach (var runner in this.bindingList)
            {
                if (runner.Team == null)
                {
                    this.context.Runners.Remove(runner);
                }

                if (runner.Id == 0)
                {
                    if (this.context.Runners.FirstOrDefault(r => r.Name == runner.Name && r.Team.Id == runner.Team.Id) == null)
                    {
                        this.context.Runners.Add(runner);
                    }
                }
            }

            this.context.SaveChanges();
            this.runnerDataGridView.Refresh();

            List<Runner> toRemove = new List<Runner>();
            foreach (var runner in this.context.Runners)
            {
                if (this.bindingList.FirstOrDefault(r => r.Id == runner.Id) == null)
                {
                    toRemove.Add(runner);
                }
            }

            this.context.Runners.RemoveRange(toRemove);
            this.context.SaveChanges();
            this.runnerDataGridView.Refresh();
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
        }
    }
}
