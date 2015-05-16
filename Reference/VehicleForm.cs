using Business;
using ElectronicStore.Common;
using Model;
using System;
using System.Windows.Forms;

namespace ElectronicStore.Reference
{
    public partial class VehicleForm : Form
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

        public VehicleForm()
        {
            InitializeComponent();

            textName.Focus();
            itemId = 0;

            InitForm();

            this.Text = "Thêm phương tiện";
        }

        public VehicleForm(int id)
        {
            InitializeComponent();

            textName.Focus();
            itemId = id;

            var biz = new VehicleBiz();
            var item = biz.LoadItem(id);

            textName.Text = item.Name;
            drlType.SelectedItem = item.Type;
            textLicensePlate.Text = item.LicensePlate;

            created = item.Created;
            createdBy = item.CreatedByUserId;
            modified = item.Modified;
            modifiedBy = item.ModifiedByUserId;

            InitForm();

            this.Text = "Sửa phương tiện";
        }

        private void SaveItem(object sender, EventArgs e)
        {
            if (CustomValidation())
            {
                var item = new Vehicle();
                item.Name = textName.Text;
                item.LicensePlate = textLicensePlate.Text;
                item.Type = Convert.ToString(drlType.SelectedItem);

                if (itemId > 0)
                {
                    item.Id = itemId;
                    item.Created = created;
                    item.CreatedByUserId = createdBy;

                    item.Modified = DateTime.Now;
                    item.ModifiedByUserId = currentUser;

                    var biz = new VehicleBiz();
                    biz.UpdateItem(item);
                }
                else
                {
                    item.Created = DateTime.Now;
                    item.CreatedByUserId = currentUser;

                    item.Modified = DateTime.Now;
                    item.ModifiedByUserId = currentUser;

                    var biz = new VehicleBiz();
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

            if (string.IsNullOrEmpty(Convert.ToString(drlType.SelectedItem)))
            {
                errorProvider.SetError(drlType, Constants.Messages.RequireMessage);
                hasError = false;

                drlType.Focus();
            }

            if (string.IsNullOrEmpty(textLicensePlate.Text))
            {
                errorProvider.SetError(textLicensePlate, Constants.Messages.RequireMessage);
                hasError = false;

                textLicensePlate.Focus();
            }

            return hasError;
        }
    }
}
