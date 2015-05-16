using Business;
using ElectronicStore.Common;
using Model;
using System;
using System.Windows.Forms;

namespace ElectronicStore.Administration
{
    public partial class RoleForm : Form
    {
        private int itemId;

        private void InitForm()
        {
            buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public RoleForm()
        {
            InitializeComponent();

            textName.Focus();
            itemId = 0;

            InitForm();

            this.Text = "Thêm quyền sử dụng";
        }

        public RoleForm(int id)
        {
            InitializeComponent();

            textName.Focus();
            itemId = id;

            InitForm();

            this.Text = "Cập nhật quyền sử dụng";
        }

        private void SaveItem(object sender, EventArgs e)
        {
            if (CustomValidation())
            {
                var item = new Role();
                item.Name = textName.Text;

                var biz = new RoleBiz();
                biz.SaveItem(item);

                this.Close();
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

            if (string.IsNullOrEmpty(textName.Text))
            {
                errorProvider.SetError(textName, Constants.Messages.RequireMessage);
                hasError = false;

                textName.Focus();
            }

            return hasError;
        }
    }
}
