using System.ComponentModel.DataAnnotations;

namespace AmazingCalcRazorPage.ViewModels
{
    public class UserCredentials
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; } // optional for login, required for registration
    }
}
