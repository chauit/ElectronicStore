using System;
using System.Windows.Forms;
using Business;
using ElectronicStore.Common;
using Model;

namespace ElectronicStore.Administration
{
    public partial class CustomerForm : Form
    {
        private int itemId;

        private DateTime? created;
        private int? createdBy;
        private DateTime? modified;
        private int? modifiedBy;

        private int currentUser;

        private void InitForm(User user)
        {
            buttonSave.DialogResult = DialogResult.OK;
            button2.DialogResult = DialogResult.Cancel;

            drlCity.Items.Clear();
            drlCity.DataSource = Utilities.GetCities();

            drlSegment.Items.Clear();
            drlSegment.DataSource = Utilities.GetSegments();
            currentUser = user.Id;
        }

        public CustomerForm(User user)
        {
            InitializeComponent();

            textFullName.Focus();
            itemId = 0;

            InitForm(user);

            this.Text = "Thêm khách hàng";
        }

        public CustomerForm(int id, User user)
        {
            InitializeComponent();
            InitForm(user);

            textFullName.Focus();
            itemId = id;

            var biz = new CustomerBiz();
            var item = biz.LoadItem(id);

            textFullName.Text = item.FullName;
            textAddress1.Text = item.Address1;
            textAddress2.Text = item.Address2;
            drlCity.SelectedItem = item.City;
            textPostalCode.Text = item.PostalCode;
            textTel.Text = item.Tel;
            textMobile1.Text = item.Mobile1;
            textMobile2.Text = item.Mobile2;
            textEmail1.Text = item.Email1;
            textEmail2.Text = item.Email2;
            drlSegment.SelectedItem = item.Segment;
            txtCompany.Text = item.Company;

            drlMr.SelectedItem = item.Mr;

            if (item.Delivery.HasValue)
            {
                numberDelivery.Text = Convert.ToString(item.Delivery.Value);
            }
            textOtherInformation.Text = item.OtherInformation;

            created = item.Created;
            createdBy = item.CreatedByUserId;
            modified = item.Modified;
            modifiedBy = item.ModifiedByUserId;

            this.Text = "Sửa khách hàng";
        }

        private void SaveItem(object sender, EventArgs e)
        {
            if (CustomValidation())
            {
                var item = new Customer();
                item.FullName = textFullName.Text;
                item.Address1 = textAddress1.Text;
                item.Address2 = textAddress2.Text;                
                item.City = Convert.ToString(drlCity.SelectedItem);
                item.Segment = Convert.ToString(drlSegment.SelectedItem);
                item.Mr = Convert.ToString(drlMr.SelectedItem);

                item.PostalCode = textPostalCode.Text;
                item.Tel = textTel.Text;
                item.Mobile1 = textMobile1.Text;
                item.Mobile2 = textMobile2.Text;
                item.Email1 = textEmail1.Text;
                item.Email2 = textEmail2.Text;
                if (!string.IsNullOrEmpty(numberDelivery.Text))
                {
                    item.Delivery = Convert.ToInt32(numberDelivery.Text);
                }
                item.OtherInformation = textOtherInformation.Text;
                item.Company = txtCompany.Text;

                if (itemId > 0)
                {
                    item.Id = itemId;
                    item.Created = created;
                    item.CreatedByUserId = createdBy;

                    item.Modified = DateTime.Now;
                    item.ModifiedByUserId = currentUser;

                    var biz = new CustomerBiz();
                    biz.UpdateItem(item);
                }
                else
                {
                    item.Created = DateTime.Now;
                    item.CreatedByUserId = currentUser;

                    item.Modified = DateTime.Now;
                    item.ModifiedByUserId = currentUser;

                    var biz = new CustomerBiz();
                    biz.SaveItem(item);
                }

                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.None;
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

            if (string.IsNullOrEmpty(Convert.ToString(drlMr.SelectedItem)))
            {
                errorProvider.SetError(drlMr, Constants.Messages.RequireMessage);
                hasError = false;

                drlMr.Focus();
            }

            if (string.IsNullOrEmpty(textFullName.Text))
            {
                errorProvider.SetError(textFullName, Constants.Messages.RequireMessage);
                hasError = false;

                textFullName.Focus();
            }

            if (string.IsNullOrEmpty(textAddress1.Text))
            {
                errorProvider.SetError(textAddress1, Constants.Messages.RequireMessage);
                hasError = false;

                textAddress1.Focus();
            }

            if (string.IsNullOrEmpty(Convert.ToString(drlCity.SelectedItem)))
            {
                errorProvider.SetError(drlCity, Constants.Messages.RequireMessage);
                hasError = false;

                drlCity.Focus();
            }

            if (string.IsNullOrEmpty(Convert.ToString(drlSegment.SelectedItem)))
            {
                errorProvider.SetError(drlSegment, Constants.Messages.RequireMessage);
                hasError = false;

                drlSegment.Focus();
            }

            if (string.IsNullOrEmpty(textMobile1.Text))
            {
                errorProvider.SetError(textMobile1, Constants.Messages.RequireMessage);
                hasError = false;

                textMobile1.Focus();
            }

            if (string.IsNullOrEmpty(textEmail1.Text))
            {
                errorProvider.SetError(textEmail1, Constants.Messages.RequireMessage);
                hasError = false;

                textEmail1.Focus();
            } 
            
            return hasError;
        }

        

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
