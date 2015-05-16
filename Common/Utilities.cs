using System;
using System.Collections.Generic;
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
    }
}
