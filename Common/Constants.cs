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
            public const string ConflictOrderMessage = "Đơn hàng đã có người cập nhật. Bạn không thể cập nhật thông tin của đơn hàng này";
            public const string DependentOrderMessage = "Bạn không xóa được đơn hàng do đơn hàng đã được chuyển sang đơn vị vận chuyển. ";
            public const string ConflictDeliveryOrderMessage = "Có đơn hàng đã được đưa vào phiếu giao hàng khác.";
            public const string CannotLogin = "Tên đăng nhập hoặc mật khẩu không đúng.";
            public const string AccountExist = "Tên đăng nhập đã tồn tại";

            public const string CannotChangePassword = "Tên đăng nhập hoặc mật khẩu cũ không đúng.";

            public const string ChangePasswordSuccessful = "Bạn đã đổi mật khẩu thành công.";
        }

        public const string DefaultPassword = "1";
        public const string DefaultDateTimeFormat = "dd/MM/yyyy hh:mm tt";

        public const string OrderStatusDraft = "Nhận đơn hàng từ khách";
        public const string OrderStatusDelivered = "Đã chuyển hàng";
        public const string DeliveryStatusDraft = "Chưa chuyển hàng";
        public const string DeliveryStatusDelivered = "Đã chuyển hàng";
        public const string DeliverySentSms = "Đã gửi";
        public const string DeliverySendSms = "Chưa gửi";
        public const string DeliverySentEmail = "Đã gửi";
        public const string DeliverySendEmail = "Chưa gửi";

        public const string CityInternal = "Hà Nội";       
    }
}
