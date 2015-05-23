using Business;
using ElectronicStore.Common;
using Model;
using System;
using System.Windows.Forms;

namespace ElectronicStore.Administration
{
    public partial class EmailForm : Form
    {
        private int itemId;

        private DateTime? created;
        private int? createdBy;
        private DateTime? modified;
        private int? modifiedBy;

        private int currentUser;

        private void InitForm()
        {
            buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            //TODO: Get Id from Login page
            currentUser = 6;
        }

        public EmailForm()
        {
            InitializeComponent();

            textName.Focus();
            itemId = 0;

            InitForm();

            this.Text = "Thêm email";
        }

        public EmailForm(int id)
        {
            InitializeComponent();

            textName.Focus();
            itemId = id;

            var biz = new EmailBiz();
            var item = biz.LoadItem(id);

            textName.Text = item.Name;
            textContent.Text = item.Content;
            textSubject.Text = item.Subject;
            created = item.Created;
            createdBy = item.CreatedByUserId;
            modified = item.Modified;
            modifiedBy = item.ModifiedByUserId;

            InitForm();

            this.Text = "Sửa email";
        }

        private void SaveItem(object sender, EventArgs e)
        {
            if (CustomValidation())
            {
                var item = new Email();
                item.Name = textName.Text;
                item.Content = textContent.Text;
                item.Subject = textSubject.Text;

                if (itemId > 0)
                {
                    item.Id = itemId;
                    item.Created = created;
                    item.CreatedByUserId = createdBy;

                    item.Modified = DateTime.Now;
                    item.ModifiedByUserId = currentUser;

                    var biz = new EmailBiz();
                    biz.UpdateItem(item);
                }
                else
                {
                    item.Created = DateTime.Now;
                    item.CreatedByUserId = currentUser;

                    item.Modified = DateTime.Now;
                    item.ModifiedByUserId = currentUser;

                    var biz = new EmailBiz();
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

            if (string.IsNullOrEmpty(textSubject.Text))
            {
                errorProvider.SetError(textSubject, Constants.Messages.RequireMessage);

                if (hasError)
                {
                    textName.Focus();
                    hasError = false;
                }
            }

            if (string.IsNullOrEmpty(textContent.Text))
            {
                errorProvider.SetError(textContent, Constants.Messages.RequireMessage);                

                if (hasError)
                {
                    textContent.Focus();
                    hasError = false;
                }
            }

            return hasError;
        }
    }
}
