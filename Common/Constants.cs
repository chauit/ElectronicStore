using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Common
{
    public class Constants
    {
        public class Messages
        {
            public const string RequireMessage = "Thông tin cần nhập";
        }

        public const string DefaultPassword = "1";
        public const string DefaultDateTimeFormat = "dd/MM/yyyy hh:mm tt";

        public const string OrderStatusDraft = "Nhận đơn hàng từ khách";
        public const string OrderStatusDelivered = "Đã chuyển hàng";
        public const string DeliveryStatusDraft = "Chưa chuyển hàng";
        public const string DeliveryStatusDelivered = "Đã chuyển hàng";
        
    }
}
