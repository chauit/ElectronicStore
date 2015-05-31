using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ElectronicStore.Administration
{
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm()
        {
            InitializeComponent();

            textUsername.Focus();
        }

        public ChangePasswordForm(string username)
        {
            InitializeComponent();

            textUsername.Focus();
            textUsername.Text = username;
        }

        private void ChangePassword(object sender, EventArgs e)
        {
            if (CustomValidate())
            {
                var biz = new UserBiz();
                var currentUser = biz.Login(textUsername.Text, textPasswordOld.Text);

                if (currentUser == null)
                {
                    labelMessage.Text = ElectronicStore.Common.Constants.Messages.CannotChangePassword;
                }
                else
                {
                    string newPass = textPasswordNew.Text;
                    newPass = Utility.EncodePassword(newPass);

                    currentUser.Password = newPass;
                    biz.UpdateItem(currentUser);

                    this.DialogResult = System.Windows.Forms.DialogResult.OK;

                    MessageBox.Show(ElectronicStore.Common.Constants.Messages.ChangePasswordSuccessful);

                    this.Close();
                }                
            }
        }

        private void Cancel(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool CustomValidate()
        {
            bool result = true;
            bool isFocused = false;
            labelMessage.Text = string.Empty;
            errorProvider.Clear();

            if (string.IsNullOrEmpty(textUsername.Text))
            {
                errorProvider.SetError(textUsername, ElectronicStore.Common.Constants.Messages.RequireMessage);
                result = false;

                textUsername.Focus();
                isFocused = true;
            }

            if (string.IsNullOrEmpty(textPasswordOld.Text))
            {
                errorProvider.SetError(textPasswordOld, ElectronicStore.Common.Constants.Messages.RequireMessage);
                result = false;

                if (!isFocused)
                {
                    textPasswordOld.Focus();
                }
            }

            if (string.IsNullOrEmpty(textPasswordNew.Text))
            {
                errorProvider.SetError(textPasswordNew, ElectronicStore.Common.Constants.Messages.RequireMessage);
                result = false;

                if (!isFocused)
                {
                    textPasswordNew.Focus();
                }
            }

            if (string.IsNullOrEmpty(textConfirmPasswordNew.Text))
            {
                errorProvider.SetError(textConfirmPasswordNew, ElectronicStore.Common.Constants.Messages.RequireMessage);
                result = false;

                if (!isFocused)
                {
                    textConfirmPasswordNew.Focus();
                }
            }

            if (!string.IsNullOrEmpty(textPasswordNew.Text) && !string.IsNullOrEmpty(textConfirmPasswordNew.Text) &&
                !string.Equals(textPasswordNew.Text, textConfirmPasswordNew.Text, StringComparison.InvariantCultureIgnoreCase))
            {
                result = false;
                labelMessage.Text = "Mật khẩu mới và nhập lại không giống nhau.";

                if (!isFocused)
                {
                    textConfirmPasswordNew.Focus();
                }
            }

            return result;
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {

        }
    }
}
