using Business;
using ElectronicStore.Common;
using Model;
using System;
using System.Windows.Forms;

namespace ElectronicStore.Administration
{
    public partial class UserForm : Form
    {
        private int itemId;
        private string password;

        private void InitForm()
        {
            buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public UserForm()
        {
            InitializeComponent();

            textFirstName.Focus();
            itemId = 0;

            InitForm();

            this.Text = "Thêm nhân viên";
        }

        public UserForm(int id)
        {
            InitializeComponent();

            textFirstName.Focus();
            itemId = id;

            var biz = new UserBiz();
            var item = biz.LoadItem(id);

            textFirstName.Text = item.FirstName;
            textLastName.Text = item.LastName;
            textUsername.Text = item.Username;
            drlType.SelectedItem = item.Type;
            textMobile.Text = item.Mobile;
            textOtherInformation.Text = item.AdditionalInformation;
            password = item.Password;

            InitForm();

            this.Text = "Sửa nhân viên";
        }

        private void SaveItem(object sender, EventArgs e)
        {
            if (CustomValidation())
            {
                var item = new User();
                item.FirstName = textFirstName.Text;
                item.LastName = textLastName.Text;
                item.Username = textUsername.Text;
                item.Type = Convert.ToString(drlType.SelectedItem);
                item.Mobile = textMobile.Text;
                item.AdditionalInformation = textOtherInformation.Text;
                item.FullName = string.Concat(item.FirstName, " ", item.LastName);

                if (itemId > 0)
                {
                    item.Id = itemId;
                    item.Password = password;
                    var biz = new UserBiz();
                    biz.UpdateItem(item);
                }
                else
                {
                    item.Password = Utilities.EncodePassword(Constants.DefaultPassword);
                    var biz = new UserBiz();
                    biz.SaveItem(item);
                }

                this.Close();
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }

        private void CancelItem(object sender, EventArgs e)
        {            
            this.Close();
        }

        private bool CustomValidation()
        {
            bool hasError = true;            
            errorProvider.Clear();

            if (string.IsNullOrEmpty(textFirstName.Text))
            {
                errorProvider.SetError(textFirstName, Constants.Messages.RequireMessage);
                hasError = false;

                textFirstName.Focus();
            }

            if (string.IsNullOrEmpty(textLastName.Text))
            {
                errorProvider.SetError(textLastName, Constants.Messages.RequireMessage);
                hasError = false;

                textLastName.Focus();
            }

            if (string.IsNullOrEmpty(textUsername.Text))
            {
                errorProvider.SetError(textUsername, Constants.Messages.RequireMessage);
                hasError = false;

                textUsername.Focus();
            }

            if (string.IsNullOrEmpty(Convert.ToString(drlType.SelectedItem)))
            {
                errorProvider.SetError(drlType, Constants.Messages.RequireMessage);
                hasError = false;

                drlType.Focus();
            }

            if (string.IsNullOrEmpty(textMobile.Text))
            {
                errorProvider.SetError(textMobile, Constants.Messages.RequireMessage);
                hasError = false;

                textMobile.Focus();
            }            
            

            return hasError;
        }
    }
}
