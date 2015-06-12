using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Business;
using ElectronicStore.Views;
using Model;

namespace ElectronicStore.Administration
{
    public partial class CustomerImportPreviewForm : Form
    {
        readonly List<CustomerItem> _customerList;
        private int currentUser;

        #region "Delegate"

        public delegate void LoadData();

        public event LoadData OnLoadData = null;

        protected virtual void FireEvent()
        {
            if (OnLoadData != null)
            {
                OnLoadData();
            }
        }

        #endregion
        public CustomerImportPreviewForm(List<CustomerItem> lst,User user)
        {
            InitializeComponent();
            _customerList = lst;
            currentUser = user.Id;
        }

        private void CustomerImportPreviewForm_Load(object sender, EventArgs e)
        {
            dataGridView.DataSource = _customerList.Select(p => new
            {
                p.Message,
                p.FirstName,
                p.LastName,
                p.Address1,
                p.Address2,
                p.City,
                p.PostalCode,
                p.Tel,
                p.Mobile1,
                p.Mobile2,
                p.Email1,
                p.Email2,
                p.Delivery,
                p.OtherInformation,
                p.Segment
            }).ToList();
            dataGridView.Refresh();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                var isChecked = Convert.ToBoolean(row.Cells["cbo"].Value);
                if (isChecked)
                {
                    var item = new Customer();
                    item.FirstName = Convert.ToString(row.Cells["FirstName"].Value);
                    item.LastName = Convert.ToString(row.Cells["LastName"].Value);
                    item.Address1 = Convert.ToString(row.Cells["Address1"].Value);
                    item.Address2 = Convert.ToString(row.Cells["Address2"].Value);
                    item.FullName = string.Concat(item.FirstName, " ", item.LastName);

                    item.City = Convert.ToString(row.Cells["City"].Value);
                    item.Segment = Convert.ToString(row.Cells["Segment"].Value);

                    item.PostalCode = Convert.ToString(row.Cells["PostalCode"].Value);
                    item.Tel = Convert.ToString(row.Cells["Tel"].Value);
                    item.Mobile1 = Convert.ToString(row.Cells["Mobile1"].Value);
                    item.Mobile2 = Convert.ToString(row.Cells["Mobile2"].Value);
                    item.Email1 = Convert.ToString(row.Cells["Email1"].Value);
                    item.Email2 = Convert.ToString(row.Cells["Email2"].Value);

                    var delivery = Convert.ToString(row.Cells["Delivery"].Value);

                    if (!string.IsNullOrEmpty(delivery))
                    {
                        item.Delivery = Convert.ToInt32(delivery);
                    }

                    item.OtherInformation = Convert.ToString(row.Cells["OtherInformation"].Value);

                    item.Created = DateTime.Now;
                    item.CreatedByUserId = currentUser;

                    item.Modified = DateTime.Now;
                    item.ModifiedByUserId = currentUser;

                    var biz = new CustomerBiz();
                    biz.SaveItem(item);
                }
            }
            FireEvent();
            Close();
        }
    }
}
