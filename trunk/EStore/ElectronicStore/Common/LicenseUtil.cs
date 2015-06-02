using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ElectronicStore.Common
{
    public class LicenseUtil
    {
        private string Key = "SOFTWARE\\{0}\\{1}";
        public string Read(string KeyName)
        {
            // Opening the registry key
            RegistryKey rk = Registry.CurrentUser;
            // Open a subKey as read-only
            string manufacturer = Business.Utility.GetSetting("manufacturer", string.Empty);
            string productname = Business.Utility.GetSetting("productname", string.Empty);
            string subKey = string.Format(Key, manufacturer, productname);

            RegistryKey sk1 = rk.OpenSubKey(subKey);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return null;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    return (string)sk1.GetValue(KeyName.ToUpper());
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    return null;
                }
            }
        }
    
    }
}
