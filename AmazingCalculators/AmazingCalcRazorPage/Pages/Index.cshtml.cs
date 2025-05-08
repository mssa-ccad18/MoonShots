using AmazingCalcRazorPage.ViewModels;
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
        public UserCredentials Credentials { get; set; }

        public string Message { get; set; }

        public IActionResult OnPost(string action)
        {
            if (!ModelState.IsValid)
            {
                Message = "Please complete all required fields.";
                return Page();
            }

            if (action == "register")
            {
                var result = _authService.RegisterUser(
                    Credentials.UserName,
                    Credentials.Password,
                    Credentials.Email ?? ""
                );
                Message = result ? "Registration successful!" : "Username already exists.";
            }
            else if (action == "login")
            {
                var result = _authService.LoginUser(Credentials.UserName, Credentials.Password);
                Message = result ? $"Welcome back, {Credentials.UserName}!" : "Invalid username or password.";
            }

            return Page();
        }
    }
}
