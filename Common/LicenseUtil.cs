using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace ElectronicStore.Common
{
    public class LicenseUtil
    {
        private const string keyCpu = "Win32_Processor";
        private const string keyHardDisk = "Win32_DiskDrive";
        private long size = 0;
        private string raw1 = string.Empty;
        private string raw2 = string.Empty;
        private string random = "Chauit12bct45833VietNamhp663835";
        private string key = "NM@#891AbC&*";
        private readonly MD5 _md5Hash = null;

        public LicenseUtil()
        {
            _md5Hash = MD5.Create();
            raw1 = this.GetInfoMain();
            raw2 = this.GetString(raw1);
        }

        private string GetInfoMain()
        {
            string result = "";
            string serialCpu = "";
            string serialHdd = "";
            try
            {
                ManagementObjectSearcher cpu = new ManagementObjectSearcher("select * from " + keyCpu);
                ManagementObjectSearcher hdd = new ManagementObjectSearcher("select * from " + keyHardDisk);

                ManagementObjectCollection mocCpu = cpu.Get();
                ManagementObjectCollection mocHdd = hdd.Get();

                foreach (var mo in mocCpu)
                {
                    serialCpu = mo["ProcessorId"].ToString();
                    break;
                }

                foreach (var mo in mocHdd)
                {
                    if (long.Parse(mo["Size"].ToString()) > size)
                    {
                        size = long.Parse(mo["Size"].ToString());
                        serialHdd = mo["SerialNumber"].ToString();
                    }
                }

                result = serialCpu + serialHdd;
            }
            catch (Exception)
            {
            }

            if (result.Length == 0)
            {
                result = random;
            }
            else if (result.Length < 10)
            {
                result = result + random;
            }

            return result;
        }

        private string GetString(string str)
        {
            string temp = string.Empty;
            foreach (char c in str)
            {
                if (char.IsLetterOrDigit(c))
                {
                    temp += c;
                }
            }

            return temp;
        }

        private string Convert(string str)
        {
            long temp1 = 0;
            long temp2 = 1;
            string output = string.Empty;
            int intTemp = 0;

            foreach (char c in str)
            {
                if (char.IsLetter(c))
                {
                    intTemp = c - '0';
                    temp1 += intTemp;
                }
                else if (char.IsNumber(c))
                {
                    if (c - '0' != 0)
                    {
                        intTemp = ((int)(c - '0'));
                        temp2 *= intTemp;
                    }
                }
            }

            if ((temp1 + temp2).ToString().Count() > 10)
            {
                output = (temp1 + temp2).ToString().Substring(0, 10).Insert(5, "-");
            }
            else if ((temp1 + temp2).ToString().Count() > 5)
            {
                output = (temp1 + temp2).ToString().Insert(5, "-");
            }
            else
            {
                output = (temp1 + temp2).ToString();
            }

            return output;
        }

        private string Encrypt(string str)
        {
            return GetMd5Hash(_md5Hash, str + this.key).Substring(0, 25).Insert(5, "-").Insert(11, "-").Insert(17, "-").Insert(23, "-").ToUpper();
        }

        private string GetMd5Hash(MD5 md5Hash, string input)
        {

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            string hashOfInput = GetMd5Hash(md5Hash, input + this.key).Substring(0, 25).Insert(5, "-").Insert(11, "-").Insert(17, "-").Insert(23, "-").ToUpper();
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            return false;
        }

        public string ProductKey
        {
            get { return this.Convert(this.raw2); }
        }

        public bool WriteFile(string license)
        {
            bool result = false;
            try
            {
                File.WriteAllText("license.dat", license);
                result = true;
            }
            catch (Exception)
            {
                MessageBox.Show(@"Không lưu được thông tin kích hoạt phần mềm. Xin vui lòng thử lại.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        public string ReadFile()
        {
            string result = string.Empty;
            if (File.Exists("license.dat"))
            {
                try
                {
                    result = File.ReadAllLines("license.dat")[0];
                }
                catch (Exception)
                {
                    MessageBox.Show(@"Không đọc được thông tin kích hoạt phần mềm. Xin vui lòng thử lại.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return result;
        }

        public bool VerifyLicense()
        {
            string hash = this.ReadFile();
            return VerifyMd5Hash(_md5Hash, this.ProductKey, hash);
        }
    }
    }
}
