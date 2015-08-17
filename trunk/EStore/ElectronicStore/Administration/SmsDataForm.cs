using Business;
using ElectronicStore.Common;
using Model;
using System;
using System.Windows.Forms;

namespace ElectronicStore.Administration
{
    public partial class SmsDataForm : Form
    {
        private int itemId;

        private DateTime? created;
        private int? createdBy;
        private DateTime? modified;
        private int? modifiedBy;

        private int currentUser;

        private void InitForm(User  user)
        {
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            currentUser = user.Id;
        }

        public SmsDataForm(int id, User user)
        {
            InitializeComponent();

            textSendOn.Focus();
            itemId = id;

            var biz = new SmsDataBiz();
            var item = biz.LoadItem(id);

            textSendOn.Text = item.SendOn.ToString(Constants.DefaultDateTimeFormat);
            textContent.Text = item.Content;
            textSendFrom.Text = item.SendFrom;
            textSendTo.Text = item.SendTo;
            textNumber.Text = item.SendToNumber;

            InitForm(user);

            this.Text = "Xem thông tin tin nhắn";
        }
        
        private void CancelItem(object sender, EventArgs e)
        {            
            this.Close();
        }
    }
}
