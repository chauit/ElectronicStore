using Business;
using ElectronicStore.Common;
using Model;
using System;
using System.Windows.Forms;

namespace ElectronicStore.Reference
{
    public partial class ProductLDForm : Form
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

            var biz = new ProductTypeBiz();
            var types = biz.LoadItems();
            types.Insert(0, new ProductType());

            drlType.Items.Clear();
            drlType.DataSource = types;
            drlType.DisplayMember = "Name";
            drlType.ValueMember = "Id";            
        }

        public ProductLDForm(User user)
        {
            InitializeComponent();

            InitForm(user);

            textName.Focus();
            itemId = 0;

            

            this.Text = "Thêm sản phẩm";
        }

        public ProductLDForm(int id, User user)
        {
            InitializeComponent();

            InitForm(user);

            textName.Focus();
            itemId = id;

            var biz = new ProductLDBiz();
            var item = biz.LoadItem(id);

            textName.Text = item.Name;
            if (item.ProductTypeId.HasValue)
            {
                drlType.SelectedValue = item.ProductTypeId.Value;
            }
            numberPrice.Text = item.Price.ToString();

            created = item.Created;
            createdBy = item.CreatedByUserId;
            modified = item.Modified;
            modifiedBy = item.ModifiedByUserId;

            this.Text = "Sửa sản phẩm";
        }

        private void SaveItem(object sender, EventArgs e)
        {
            if (CustomValidation())
            {
                var item = new ProductLD();
                item.Name = textName.Text;
                item.ProductTypeId = Convert.ToInt32(drlType.SelectedValue);
                item.Price = Convert.ToDecimal(numberPrice.Text);

                if (itemId > 0)
                {
                    item.Id = itemId;
                    item.Created = created;
                    item.CreatedByUserId = createdBy;

                    item.Modified = DateTime.Now;
                    item.ModifiedByUserId = currentUser;

                    var biz = new ProductLDBiz();
                    biz.UpdateItem(item);
                }
                else
                {
                    item.Created = DateTime.Now;
                    item.CreatedByUserId = currentUser;

                    item.Modified = DateTime.Now;
                    item.ModifiedByUserId = currentUser;

                    var biz = new ProductLDBiz();
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

            if (Convert.ToInt32(drlType.SelectedValue) == 0)
            {
                errorProvider.SetError(drlType, Constants.Messages.RequireMessage);
                hasError = false;

                drlType.Focus();
            }

            if (string.IsNullOrEmpty(numberPrice.Text))
            {
                errorProvider.SetError(numberPrice, Constants.Messages.RequireMessage);
                hasError = false;

                numberPrice.Focus();
            }

            return hasError;
        }
    }
}
