using AmazingCalculatorLibrary.Models;
using AmazingCalculatorLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AmazingCalcRazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AuthServices _authService;

        public IndexModel(ILogger<IndexModel> logger, AuthServices authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string action)
        {
            if (action == "register")
            {
                var result = _authService.RegisterUser(UserName, Password, Email);
                Message = result ? "Registration successful!" : "Username already exists.";
            }
            else if (action == "login")
            {
                var result = _authService.LoginUser(UserName, Password);
                Message = result ? $"Welcome back, {UserName}!" : "Invalid username or password.";
            }

            return Page(); // redisplay form with message
        }
    }
}
