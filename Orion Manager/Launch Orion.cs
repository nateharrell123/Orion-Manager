using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Orion_Manager
{
    public partial class MyOrion : Form
    {
        public MyOrion()
        {
            Thread thread = new Thread(new ThreadStart(StartScreen));
            thread.Start();
            Thread.Sleep(1500);
            InitializeComponent();
            thread.Abort();

            // this.FormClosed += new FormClosedEventHandler(formClose);
        }
        /// <summary>
        /// Splash Screen
        /// </summary>
        public void StartScreen()
        {
            Application.Run(new Startup_Screen());
        }

        /// <summary>
        /// Cleaning up when form is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void formClose(object sender, FormClosedEventArgs e)
        {
            // TODO: close driver when form close
        }


        /// <summary>
        /// For Table Binding Navigator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.orion_DatabaseDataSet);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // This line of code loads data into the 'orion_DatabaseDataSet.Table' table.
            this.tableTableAdapter.Fill(this.orion_DatabaseDataSet.Table);
        }

        /// <summary>
        /// Launches Orion based on IP.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxLaunchOrion_Click(object sender, EventArgs e)
        {
            ChromeOptions options = new ChromeOptions
            {
                AcceptInsecureCertificates = true // For untrusted certificates
            };

            if (uxDataTable.CurrentCell == null) return; 

            var selectedCell = uxDataTable.CurrentCell.Value.ToString();

            if (Regex.IsMatch(selectedCell, @"^\d")) // if it starts with num (IP)
            {
                var driver = new ChromeDriver(options);
                driver.Url = $"https://{selectedCell}";
            }
            else MessageBox.Show("You can launch an Orion by selecting the IP address, then click 'Launch Orion.'", ":(");
        }
    }
}
