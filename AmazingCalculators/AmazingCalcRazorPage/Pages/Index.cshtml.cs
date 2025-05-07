using AmazingCalculatorLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AmazingCalcRazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly FitnessDbContext fitnessDbContext;

        public IndexModel(ILogger<IndexModel> logger,FitnessDbContext fitnessDbContext)
        {
            _logger = logger;
            this.fitnessDbContext = fitnessDbContext;
        }

        public void OnGet()
        {
           
            var userCount = fitnessDbContext.UserProfiles.Count();
        }
    }
}
