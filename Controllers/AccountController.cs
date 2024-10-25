using DoAnPM_TH_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace DoAnPM_TH_.Controllers
{
    public class AccountController : Controller
    {
        private readonly LongChauStoreContext _context;

        public AccountController(LongChauStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password, bool rememberMe)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            // Nếu tìm thấy người dùng
            if (user != null)
            {
                // Thiết lập session
                HttpContext.Session.SetString("username", user.UserName);
                HttpContext.Session.SetString("role", user.Role);

                // Nếu nhớ tôi, có thể thiết lập cookie hoặc session
                if (rememberMe)
                {
                    // Lưu thông tin người dùng vào cookie nếu cần
                }

                // Chuyển hướng đến trang chính hoặc trang admin
                if (user.Role == "user")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (user.Role == "admin")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
            }

            ViewBag.LoginError = "Email hoặc mật khẩu không đúng";
            return View("Index");
        }

        [HttpPost]
        public IActionResult Register(string userName, string email, string password, string confirmPassword, string phoneNumber)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);
            if (existingUser != null)
            {
                ViewBag.RegisterError = "Email đã được sử dụng. Vui lòng chọn email khác.";
                return View("Index");
            }

            // Kiểm tra độ dài và sự khớp của mật khẩu
            if (!IsValidPassword(password))
            {
                ViewBag.RegisterError = "Mật khẩu phải từ 8-16 ký tự, bao gồm chữ hoa, chữ thường và số.";
                return View("Index");
            }

            if (password != confirmPassword)
            {
                ViewBag.RegisterError = "Mật khẩu và xác nhận mật khẩu không khớp.";
                return View("Index");
            }

            if (!IsValidPhoneNumber(phoneNumber))
            {
                ViewBag.RegisterError = "Số điện thoại phải có đúng 10 chữ số.";
                return View("Index");
            }

            // Tạo đối tượng PasswordHasher để mã hóa mật khẩu
            var passwordHasher = new PasswordHasher<User>();

            // Tạo người dùng mới
            var newUser = new User
            {
                UserName = userName,
                Email = email,
                Phone = phoneNumber,
                Role = "user"
            };

            // Mã hóa mật khẩu và gán cho người dùng
            newUser.Password = passwordHasher.HashPassword(newUser, password);

            // Lưu người dùng vào cơ sở dữ liệu
            _context.Users.Add(newUser);
            _context.SaveChanges();

            HttpContext.Session.SetString("username", newUser.UserName);
            HttpContext.Session.SetString("role", newUser.Role);

            // Chuyển hướng đến trang đăng nhập hoặc trang chính sau khi đăng ký thành công
            return RedirectToAction("Index", "Home");
        }

        private bool IsValidPassword(string password)
        {
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,16}$");
            return regex.IsMatch(password);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Biểu thức chính quy để kiểm tra số điện thoại có đúng 10 số
            var regex = new Regex(@"^\d{10}$");
            return regex.IsMatch(phoneNumber);
        }

        public IActionResult Logout()
        {
            // Xóa thông tin người dùng trong session
            HttpContext.Session.Remove("username");
            // Chuyển hướng về trang chủ hoặc trang đăng nhập
            return RedirectToAction("Index", "Home");
        }
    }
}
