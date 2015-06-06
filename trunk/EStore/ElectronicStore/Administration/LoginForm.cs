using Business;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ElectronicStore.Administration
{
    public partial class LoginForm : Form
    {
        public User Result { get; set; }

        public LoginForm()
        {
            InitializeComponent();

            textUsername.Focus();
        }

        private void Login(object sender, EventArgs e)
        {
            if (CustomValidate())
            {
                var biz = new UserBiz();
                var currentUser = biz.Login(textUsername.Text, textPassword.Text);

                if (currentUser == null)
                {
                    labelMessage.Text = ElectronicStore.Common.Constants.Messages.CannotLogin;
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    Result = currentUser;
                    this.Close();
                }                
            }
        }

        private void Cancel(object sender, EventArgs e)
        {
            Application.Exit();
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

            if (string.IsNullOrEmpty(textPassword.Text))
            {
                errorProvider.SetError(textPassword, ElectronicStore.Common.Constants.Messages.RequireMessage);
                result = false;

                if (!isFocused)
                {
                    textPassword.Focus();
                }
            }

            return result;
        }
    }
}
