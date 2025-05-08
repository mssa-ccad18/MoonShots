using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AmazingCalcRazorPage.Pages
{
    public class HomePageModel : PageModel
    {
        public string? LoggedInUser { get; set; }

        public IActionResult OnGet()
        {
            // Check if user is logged in
            if (TempData["LoggedInUser"] is string username)
            {
                LoggedInUser = username;
                TempData.Keep("LoggedInUser"); // Preserve across requests
                return Page();
            }

            return RedirectToPage("/Index");
        }

        public IActionResult OnPost(string action)
        {
            if (action == "logout")
            {
                TempData.Remove("LoggedInUser");
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
