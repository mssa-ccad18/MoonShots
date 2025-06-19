using AmazingCalculatorLibrary.MilitaryPhysicalTraining;
using AmazingCalculatorLibrary.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Globalization;

public class FitnessCalculatorsModel : PageModel
{
    private readonly FitnessDbContext _context;
    private readonly AmazingCalculatorLibrary.MilitaryPhysicalTraining.IWebHostEnvironment _env;

    private readonly IConfiguration _configuration;

    public FitnessCalculatorsModel(IConfiguration configuration)
    {

        _configuration = configuration;
        CalculatorOptions = new List<string> { "USMC", "USN", "USA", "USAF", "USCG" };
    }

    [BindProperty]
    public string SelectedCalculator { get; set; }
    public List<string> CalculatorOptions { get; set; }

    [BindProperty]
    public int Age { get; set; }
    [BindProperty]
    public bool IsMale { get; set; }
    [BindProperty]
    public int PushupReps { get; set; }
    [BindProperty]
    public int PullUpReps { get; set; }
    [BindProperty]
    public int CrunchesReps { get; set; }
    [BindProperty]
    public string PlankTimeString { get; set; }
    [BindProperty]
    public string RunTimeString { get; set; }
    public string? Result { get; set; }
    public int? TotalPoints { get; set; }


    public void OnGet() 
    {
    }

    public void OnPost()
    {
        try
        {
            int plankTime = ParseTimeToSeconds(PlankTimeString);
            int runTime = ParseTimeToSeconds(RunTimeString);

            if (SelectedCalculator == "USMC")
            {
                var usmc = new USMC(_context, _env);
                if (IsMale)
                {
                    TotalPoints = usmc.USMCMalePRT(IsMale, Age, PushupReps, PullUpReps, CrunchesReps, plankTime, runTime);
                }
                else
                {
                    TotalPoints = usmc.USMCFemalePRT(IsMale, Age, PushupReps, PullUpReps, CrunchesReps, plankTime, runTime);
                    
                }
                Result = $"USMC Calculation complete. You scored {TotalPoints}.";
            }
            else if (SelectedCalculator == "USN")
            {
                var usn = new USN(_context, _env);
                TotalPoints = usn.USNPRT(IsMale, Age, PushupReps, plankTime, runTime);
                Result = $"USN Calculation complete. You scored {TotalPoints}.";
            }
        }
        catch (Exception ex)
        {
            Result = $"Error: {ex.Message}";
        }
    }

    private int ParseTimeToSeconds(string time)
    {
        if (TimeSpan.TryParseExact(time, @"m\:ss", CultureInfo.InvariantCulture, out var result))
        {
            return (int)result.TotalSeconds;
        }
        return 0;
    }
}