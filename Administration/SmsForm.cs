using Business;
using ElectronicStore.Common;
using Model;
using System;
using System.Windows.Forms;

namespace ElectronicStore.Administration
{
    public partial class SmsForm : Form
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

        public SmsForm(User user)
        {
            InitializeComponent();

            textName.Focus();
            itemId = 0;

            InitForm(user);

            this.Text = "Thêm Sms";
        }

        public SmsForm(int id, User user)
        {
            InitializeComponent();

            textName.Focus();
            itemId = id;

            var biz = new SmsBiz();
            var item = biz.LoadItem(id);

            textName.Text = item.Name;
            textContent.Text = item.Content;
            created = item.Created;
            createdBy = item.CreatedByUserId;
            modified = item.Modified;
            modifiedBy = item.ModifiedByUserId;

            InitForm(user);

            this.Text = "Sửa Sms";
        }

        private void SaveItem(object sender, EventArgs e)
        {
            if (CustomValidation())
            {
                var item = new Sms();
                item.Name = textName.Text;
                item.Content = textContent.Text;

                if (itemId > 0)
                {
                    item.Id = itemId;
                    item.Created = created;
                    item.CreatedByUserId = createdBy;

                    item.Modified = DateTime.Now;
                    item.ModifiedByUserId = currentUser;

                    var biz = new SmsBiz();
                    biz.UpdateItem(item);
                }
                else
                {
                    item.Created = DateTime.Now;
                    item.CreatedByUserId = currentUser;

                    item.Modified = DateTime.Now;
                    item.ModifiedByUserId = currentUser;

                    var biz = new SmsBiz();
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

            if (string.IsNullOrEmpty(textName.Text))
            {
                errorProvider.SetError(textName, Constants.Messages.RequireMessage);
                hasError = false;

                textName.Focus();
            }

            if (string.IsNullOrEmpty(textContent.Text))
            {
                errorProvider.SetError(textContent, Constants.Messages.RequireMessage);
                hasError = false;

                textContent.Focus();
            }

            return hasError;
        }
    }
}
