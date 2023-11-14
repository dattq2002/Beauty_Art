namespace Beauty_Art.API.Constants;

public static class MessageConstant
{
    public static class LoginMessage
    {
        public const string InvalidUsernameOrPassword = "Tên đăng nhập hoặc mật khẩu không chính xác";
        public const string DeactivatedAccount = "Tài khoản đang bị vô hiệu hoá";
    }

    public static class Account
    {
        public const string CreateAccountWithWrongRoleMessage = "Please create with acceptent role";
        public const string CreateBrandAccountFailMessage = "Tạo tài khoản mới cho nhãn hiệu thất bại";
        public const string CreateStaffAccountFailMessage = "Tạo tài khoản nhân viên thất bại";
        public const string UserUnauthorizedMessage = "Bạn không được phép cập nhật status cho tài khoản này";
        public const string CreateAccountFailMessage = "Tạo tài khoản mới thất bại";
        public const string GetUserFailMessage = "Lấy thông tin user thất bại";
        public const string GetListAccountFailMessage = "Lấy danh sách tài khoản thất bại";
        public const string UpdateAccountFailMessage = "Cập nhật thông tin tài khoản thất bại";
        public const string DeleteAccountFailMessage = "Xóa tài khoản thất bại";

        public const string UpdateAccountStatusRequestWrongFormatMessage =
            "Cập nhật status tài khoản request sai format";

        public const string AccountNotFoundMessage = "Không tìm thấy tài khoản";
        public const string UpdateAccountStatusSuccessfulMessage = "Cập nhật status tài khoản thành công";
        public const string UpdateAccountStatusFailedMessage = "Cập nhật status tài khoản thất bại";
        public const string EmptyAccountId = "Account id bị trống";
    }

    public static class Order
    {
        public const string UserNotInSessionMessage = "Tài khoản không trong ca làm để tạo Order";
        public const string NoProductsInOrderMessage = "Không thể tạo order khi order không đính kèm sản phẩm bên trong";
        public const string CreateOrderFailedMessage = "Tạo mới order thất bại";
        public const string EmptyOrderIdMessage = "Id của order không hợp lệ";
        public const string OrderNotFoundMessage = "Order không tồn tại trong hệ thống";
        public const string PaymentFailMessage = "Thanh toán thất bại";
    }

    public static class User
    {
        public const string CreateNewUserFailedMessage = "Tạo mới User thất bại";
        public const string UpdateUserFailedMessage = "Cập nhật thông tin User thất bại";
        public const string UpdateUserSuccessfulMessage = "Cập nhật thông tin User thành công";
        public const string EmptyUserId = "Id của User bị trống";
        public const string UserNotFound = "User không tồn tại trong hệ thống";

    }
    public static class Course
    {
        public const string CreateCourseFailMessage = "Tạo mới Course thất bại";
        public const string DeleteCourseFailMessage = "Xóa Course thất bại";
        public const string UpdateCourseFailMessage = "Cập nhật Course thất bại";
        public const string GetCourseFailMessage = "Lấy thông tin Course thất bại";
    }
    public static class Post
    {
        public const string CreatePostFailMessage = "Tạo mới Post thất bại";
        public const string DeletePostFailMessage = "Xóa Post thất bại";
        public const string UpdatePostFailMessage = "Cập nhật Post thất bại";
        public const string GetPostFailMessage = "Lấy thông tin Post thất bại";
    }
    public class Instructor
    {
        public const string CreateInstructorFailMessage = "Tạo mới Instructor thất bại";
        public const string DeleteInstructorFailMessage = "Xóa Instructor thất bại";
        public const string UpdateInstructorFailMessage = "Cập nhật Instructor thất bại";
        public const string GetInstructorFailMessage = "Lấy thông tin Instructor thất bại";
    }
    public class WalletType { 
        public const string CreateWalletFailMessage = "Tạo mới Wallet thất bại";
        public const string DeleteWalletFailMessage = "Xóa Wallet thất bại";
    }
}