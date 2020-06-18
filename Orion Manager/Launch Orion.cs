using OpenQA.Selenium.Chrome;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Orion_Manager
{
    /// <summary>
    /// For all your organizational needs!
    /// </summary>
    public partial class OrionManager : Form
    {
        /// <summary>
        /// Startup process
        /// </summary>
        public OrionManager()
        {
            Thread thread = new Thread(new ThreadStart(StartScreen));
            thread.Start();
            Thread.Sleep(2000);
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
            // TODO: close driver when form closes
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

        /// <summary>
        /// Fills dataset upon startup.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.tableTableAdapter.Fill(this.orion_DatabaseDataSet.Table);
        }

        /// <summaKyiv, Ukraine, 02000ry>
        /// Launches Orion based on selected IP.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxLaunchOrion_Click(object sender, EventArgs e)
        {
            if (uxDataTable.CurrentCell == null) return; 

            var selectedCell = uxDataTable.CurrentCell.Value.ToString();

            if (Regex.IsMatch(selectedCell, @"^\d")) // if it starts with num (IP)
            {
                var options = new ChromeOptions
                {
                    AcceptInsecureCertificates = true // For untrusted certificates
                };
                var driver = new ChromeDriver(options);
                driver.Url = $"https://{selectedCell}";
            }
            else MessageBox.Show("You can launch an Orion by selecting the IP address, then click 'Launch Orion.'", ":("); // doesn't start with num and won't launch
        }
    }
}
