using Business;
using ElectronicStore.Common;
using Model;
using System;
using System.Windows.Forms;

namespace ElectronicStore.Administration
{
    public partial class RoleForm : Form
    {
        ElectronicStore.Common.ILogger logger = new ElectronicStore.Common.Logger();
        private int itemId;

        private int currentUser;

        private void InitForm(User user)
        {
            buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            
            currentUser = user.Id;
        }

        public RoleForm(User user)
        {
            InitializeComponent();

            textName.Focus();
            itemId = 0;

            InitForm(user);

            this.Text = "Thêm cấu thành phố";
        }

        public RoleForm(int id, User user)
        {
            InitializeComponent();

            textName.Focus();
            itemId = id;

            var biz = new CityBiz();
            var item = biz.LoadItem(id);

            textName.Text = item.Name;

            InitForm(user);

            this.Text = "Sửa thông tin thành phố";
        }

        private void SaveItem(object sender, EventArgs e)
        {
            if (CustomValidation())
            {
                var item = new City();
                item.Name = textName.Text;

                if (itemId > 0)
                {
                    try
                    {
                        item.Id = itemId;

                        var biz = new CityBiz();

                        biz.UpdateItem(item);
                    }
                    catch (Exception ex)
                    {
                        logger.LogInfoMessage("----Update City ERROR----");
                        logger.LogException(ex);
                    }
                }
                else
                {
                    try
                    {
                        var biz = new CityBiz();
                        biz.SaveItem(item);
                    }
                    catch(Exception ex)
                    {
                        logger.LogInfoMessage("----Create City ERROR----");
                        logger.LogException(ex);
                    }
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
            else
            {
                var biz = new CityBiz();
                var result = biz.SearchByName(textName.Text.Trim());

                if(result)
                {
                    errorProvider.SetError(textName, Constants.Messages.CityExist);
                    hasError = false;

                    textName.Focus();
                }
            }
            
            return hasError;
        }
    }
}
