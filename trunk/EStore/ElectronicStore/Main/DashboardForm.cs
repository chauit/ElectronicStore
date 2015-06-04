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

            timer.Tick += new EventHandler(timer_Tick); // Every time timer ticks, timer_Tick will be called
            timer.Interval = (10) * (1000);             // Timer will tick every 10 seconds
            timer.Enabled = true;                       // Enable the timer
            timer.Start();       
            
        }
        Timer timer = new Timer();
        void timer_Tick(object sender, EventArgs e)
        {
            var biz = new OrderBiz();
            dataGridView.DataSource = biz.LoadItemsToDashBoard();
            dataGridView.Refresh();
        }
    }
}
