using ElectronicStore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronicStore
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LicenseUtil utilities = new LicenseUtil();
            var license = utilities.Read("License");
            if (StringComparer.OrdinalIgnoreCase.Equals(license, "ElectronicStore"))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MDI());    
            }
            else
            {
                MessageBox.Show("Bạn không có quyền sử dụng chương trình này.");
                Application.Exit();                        
            }
        }
    }
}
