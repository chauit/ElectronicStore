using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace ElectronicStore.Common
{
    public class Utilities
    {
        public static bool OpenForm(Form mdi, string currentFormName)
        {
            if (mdi.ActiveControl != null)
            {
                if (!string.Equals(mdi.ActiveControl.Name, currentFormName, StringComparison.InvariantCulture))
                {
                    if (mdi.ActiveControl is Form)
                    {
                        ((Form)mdi.ActiveControl).Close();
                    }
                    return true;
                }
                return false;
            }
            return true;
        }

        public static string EncodePassword(string password)
        {
            byte[] toEncodeAsBytes = Encoding.ASCII.GetBytes(password);
            string encoded = Convert.ToBase64String(toEncodeAsBytes);

            return encoded;
        }

        public static string DecodedPassword(string encodedPassword)
        {
            byte[] encodedDataAsBytes = Convert.FromBase64String(encodedPassword);
            string password = Encoding.ASCII.GetString(encodedDataAsBytes);

            return password;
        }

        public static List<string> GetCities()
        {
            var cities = new List<string>();
            cities.Add("Hà Nội");
            cities.Add("Thành phố Hồ Chí Mính");

            return cities;
        }

        /// <summary>
        /// Get setting from AppSettings node
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetSetting<T>(string key)
        {
            return GetSetting(key, default(T));
        }

        /// <summary>
        /// Get setting from AppSettings node with default value
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="key">Setting key</param>
        /// <param name="defaultValue">Default setting value</param>
        /// <returns></returns>
        public static T GetSetting<T>(string key, T defaultValue)
        {
            try
            {
                string appSetting = ConfigurationManager.AppSettings[key];

                if (string.IsNullOrEmpty(appSetting)) return defaultValue;

                return (T)Convert.ChangeType(appSetting, typeof(T), CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
