namespace NewSpecialEvent
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Microsoft.Reporting.WinForms;
    using NewSpecialEvent.ReportDataSets;
    using NewSpecialEvent.ReportDataSets.ResultsDataSourceTableAdapters;

    public partial class ResultsViewer : Form
    {
        public ResultsViewer()
        {
            this.InitializeComponent();
        }

        private void ResultsViewer_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
            if (this.reportViewer1.LocalReport.DataSources.Count > 0)
            {
                this.reportViewer1.LocalReport.DataSources.Clear();
            }
            ResultsDataSource ds = new ResultsDataSource();
            var dataTable = ds.DataTable1;
            var tableAdapter = new DataTable1TableAdapter();
            tableAdapter.Fill(dataTable);
            ReportDataSource rd = new ReportDataSource("results", dataTable as DataTable);
            this.reportViewer1.LocalReport.DataSources.Add(rd);
            this.reportViewer1.RefreshReport();
        }
    }
}
