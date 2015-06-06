using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;

namespace ElectronicStore.Main
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;

            LoadDataSource();

            timer.Tick += new EventHandler(timer_Tick);
            // Timer will tick every 3 minute
            timer.Interval = (3) * (60) * (1000);
            // Enable the timer
            timer.Enabled = true;                       
            timer.Start();   
        }


        Timer timer = new Timer();
        void timer_Tick(object sender, EventArgs e)
        {
            LoadDataSource();
        }

        private void LoadDataSource()
        {
            var biz = new OrderBiz();
            dataGridView.DataSource = biz.LoadItemsToDashBoard();
            dataGridView.Refresh();
        }
    }
}
