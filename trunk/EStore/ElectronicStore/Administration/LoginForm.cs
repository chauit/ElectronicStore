using System;
using System.Windows.Forms;
using Business;
using ElectronicStore.Common;
using Model;

namespace ElectronicStore.Administration
{
    public partial class LoginForm : Form
    {
        ElectronicStore.Common.ILogger logger = new ElectronicStore.Common.Logger();
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
                    labelMessage.Text = Constants.Messages.CannotLogin;
                }
                else
                {
                    DialogResult = DialogResult.OK;
                    Result = currentUser;
                    
                    Close();
                    
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
                errorProvider.SetError(textUsername, Constants.Messages.RequireMessage);
                result = false;

                textUsername.Focus();
                isFocused = true;
            }

            if (string.IsNullOrEmpty(textPassword.Text))
            {
                errorProvider.SetError(textPassword, Constants.Messages.RequireMessage);
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
