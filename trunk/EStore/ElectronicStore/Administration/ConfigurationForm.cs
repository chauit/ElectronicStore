using Business;
using ElectronicStore.Common;
using Model;
using System;
using System.Windows.Forms;

namespace ElectronicStore.Administration
{
    public partial class ConfigurationForm : Form
    {
        private int itemId;

        private DateTime? created;
        private int? createdBy;
        private DateTime? modified;
        private int? modifiedBy;

        private int currentUser;

        private void InitForm(User user)
        {
            buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            
            currentUser = user.Id;
        }

        public ConfigurationForm(User user)
        {
            InitializeComponent();

            textKey.Focus();
            itemId = 0;

            InitForm(user);

            this.Text = "Thêm cấu hình hệ thống";
        }

        public ConfigurationForm(int id, User user)
        {
            InitializeComponent();

            textKey.Focus();
            itemId = id;

            var biz = new ConfigurationBiz();
            var item = biz.LoadItem(id);

            textKey.Text = item.Key;
            textValue.Text = item.Value;
            created = item.Created;
            createdBy = item.CreatedByUserId;
            IsEncryption.Checked = item.Encryption;
            modified = item.Modified;
            modifiedBy = item.ModifiedByUserId;

            InitForm(user);

            this.Text = "Sửa cấu hình hệ thống";
        }

        private void SaveItem(object sender, EventArgs e)
        {
            if (CustomValidation())
            {
                var item = new Configuration();
                item.Key = textKey.Text;
                item.Encryption = IsEncryption.Checked;
                if (IsEncryption.Checked)
                {
                    item.Value = Utility.EncodePassword(textValue.Text);
                }

                if (itemId > 0)
                {
                    item.Id = itemId;
                    item.Created = created;
                    item.CreatedByUserId = createdBy;

                    item.Modified = DateTime.Now;
                    item.ModifiedByUserId = currentUser;

                    var biz = new ConfigurationBiz();
                    biz.UpdateItem(item);
                }
                else
                {
                    item.Created = DateTime.Now;
                    item.CreatedByUserId = currentUser;

                    item.Modified = DateTime.Now;
                    item.ModifiedByUserId = currentUser;

                    var biz = new ConfigurationBiz();
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

            if (string.IsNullOrEmpty(textKey.Text))
            {
                errorProvider.SetError(textKey, Constants.Messages.RequireMessage);
                hasError = false;

                textKey.Focus();
            }

            if (string.IsNullOrEmpty(textValue.Text))
            {
                errorProvider.SetError(textValue, Constants.Messages.RequireMessage);
                hasError = false;

                textKey.Focus();
            }

            return hasError;
        }
    }
}
