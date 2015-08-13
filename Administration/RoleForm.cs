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
        private string functions;

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

            this.Text = "Thêm quyền truy cập";

            treeFunctions.ExpandAll();
        }

        public RoleForm(int id, User user)
        {
            InitializeComponent();

            textName.Focus();
            itemId = id;

            var biz = new RoleBiz();
            var item = biz.LoadItem(id);

            textName.Text = item.Name;

            LoadFunctions(item.Functions);
            InitForm(user);

            this.Text = "Sửa quyền truy cập";

            treeFunctions.ExpandAll();
        }

        private void SaveItem(object sender, EventArgs e)
        {
            if (CustomValidation())
            {
                var item = new Role();
                item.Name = textName.Text;
                item.Functions = GetFunctions();

                if (itemId > 0)
                {
                    try
                    {
                        item.Id = itemId;

                        var biz = new RoleBiz();

                        biz.UpdateItem(item);
                    }
                    catch (Exception ex)
                    {
                        logger.LogInfoMessage("----Update Role ERROR----");
                        logger.LogException(ex);
                    }
                }
                else
                {
                    try
                    {
                        var biz = new RoleBiz();
                        biz.SaveItem(item);
                    }
                    catch (Exception ex)
                    {
                        logger.LogInfoMessage("----Create Role ERROR----");
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
                var biz = new RoleBiz();
                var result = biz.SearchByName(textName.Text.Trim(), itemId);

                if (result)
                {
                    errorProvider.SetError(textName, Constants.Messages.CityExist);
                    hasError = false;

                    textName.Focus();
                }
            }

            return hasError;
        }

        private void SelectedNode(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                foreach (TreeNode child in e.Node.Nodes)
                {
                    child.Checked = e.Node.Checked;
                }
            }
            //else
            //{
            //    if (!e.Node.Checked)
            //    {
            //        e.Node.Parent.Checked = false;
            //    }
            //    else
            //    {
            //        bool isAll = true;
            //        foreach (TreeNode childNode in e.Node.Parent.Nodes)
            //        {
            //            if (!childNode.Checked)
            //            {
            //                isAll = false;
            //            }
            //        }
            //        e.Node.Parent.Checked = isAll;                    
            //    }
            //}
        }

        private string GetFunctions()
        {
            string content = string.Empty;
            
            foreach(TreeNode node in treeFunctions.Nodes)
            {                
                foreach (TreeNode childNode in node.Nodes)
                {
                    if (childNode.Checked)
                    {
                        content = string.Concat(content, ";", childNode.Text);
                    }
                   
                }                
            }

            return content;
        }

        private void LoadFunctions(string function)
        {
            foreach (TreeNode node in treeFunctions.Nodes)
            {
                bool isAll = true;
                foreach (TreeNode childNode in node.Nodes)
                {
                    string text = childNode.Text;
                    if(function.IndexOf(text) != -1)
                    {
                        childNode.Checked = true;
                    }
                    else
                    {
                        isAll = false;
                    }
                }
                //node.Checked = isAll;
            }
        }
    }
}
