# 🏋️‍♂️ AmazingCalcRazorPage

A modular Razor Pages fitness calculator that benchmarks user health and workout data against official U.S. military standards (PT, PFT, PRT). Designed for extensibility, future dashboard integration, and personalized fitness tracking.

---
## 🧭 Project Structure

```text  
📦 AmazingCalculatorSolution
├── 📁 AmazingCalcRazorPage/              # Razor Pages frontend
│   ├── 📁 Pages/
│   │   ├── CaloriesBurned.cshtml (+ .cs)        # Burned calorie estimator
│   │   ├── RealTimeProgress.cshtml (+ .cs)      # Real-time PT feedback
│   │   ├── UserProfilePage.cshtml (+ .cs)       # Profile and metrics viewer
│   │   ├── Register.cshtml, Login.cshtml, etc.  # Auth and navigation
│   │   └── Shared/ _ViewImports, _ViewStart
│   ├── wwwroot/                           # Static assets (CSS, JS, images)
│   ├── appsettings.json                   # App configuration
│   └── Program.cs                         # App startup
│
├── 📁 AmazingCalculatorLibrary/           # Core logic and data models
│   ├── 📁 Models/
│   │   ├── BMI.cs, BMRCalculator.cs
│   │   ├── CalculationResult.cs, CalorieBurnedTracker.cs
│   │   ├── UserProfiles.cs, UserCredentials.cs
│   │   └── WorkoutSession.cs, FitnessDbContext.cs
│   ├── 📁 Services/
│   │   ├── AuthServices.cs, FitnessService.cs
│   ├── 📁 MilitaryPhysicalTraining/
│   │   ├── USMC.cs, USAF.cs, USCG.cs, USN.cs
│   │   └── USMCjson.json, USAFjson.json
│   └── 📁 AdvancedTrackingFeatures/
│       ├── RealTimeProgressDashboard.cs
│       └── PersonalizedWorkoutSuggestions.cs
│
├── 📁 AmazingCalculatorsTest/             # Unit and integration tests
│   ├── BMI/BMR/CaloriesBurned tests
│   ├── FitnessTest.cs, UserProfile.cs
│   ├── RealTimeProgressDashboardTest.cs
│   └── USMC/USA Branch-specific logic tests


  ```
---

## 🔑 Features

- 🔐 **Secure User Profiles** – Lightweight auth (custom `UserProfiles`)
- 🧮 **BMI/BMR Calculators** – Live feedback and thresholds
- 🪖 **Military Fitness Standards** – JSON-driven comparison engine (USMC, USAF, USCG, USN, etc.)
- ⚙️ **Extensible Dashboard Logic** – `RealTimeProgressDashboard.cs` planned for integration with Power BI/Tableau
- 🧠 **Workout Suggestions** – Auto-generated via user history and performance
- 🧪 **Full Test Coverage** – Unit-tested core logic

---

## 📦 Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022+
- SQL Server Express or SQLite (lightweight dev)
- Razor Pages & Entity Framework experience recommended

### Clone and Run

```bash
git clone https://github.com/<your-org>/MonnShots/AmazingCalculators.git
cd AmazingCalculators
dotnet build
dotnet run --project AmazingCalculators
```
---

🧠 Planned Enhancements
- Power BI or Chart.js dashboard integration
- Expand workout suggestions using ML.NET
- UI polish for mobile responsiveness
- Export-to-PDF reports and fitness summaries
- Service-specific toggles (e.g., USMC vs Army options)


 ---
## 👥 Team – MSSA CAD Cohort 18

- **Kyle Griffitts** – [GitHub Profile](https://github.com/Kyle-Griffitts)
- **Luis Moran** – [GitHub Profile](https://github.com/lmoran291)
- **Rob Pegram** – [GitHub Profile](https://github.com/itsASweater)
- **Mike Katzer** – [GitHub Profile](https://github.com/MikeK-215)
- **JustJay** – [GitHub Profile](https://github.com/JustJaysRepo)

---

 📌 Notes
This app is a work in progress. Designed with modularity in mind — new branches, test types, or logic engines can be added without rewriting the core. JSON files make military standards swappable without backend redeployment.
