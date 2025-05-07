using System.Security.Cryptography;
using System.Text;
using AmazingCalculatorLibrary.Models;

namespace AmazingCalculatorLibrary.Services
{
    public class AuthServices
    {
        private readonly FitnessDbContext _context;

        public AuthServices(FitnessDbContext context)
        {
            _context = context;
        }

        public bool RegisterUser(string username, string password, string email)
        {
            if (_context.UserProfiles.Any(u => u.UserName == username))
                return false; // user already exists

            var hashedPassword = HashPassword(password);

            var newUser = new UserProfiles
            {
                UserName = username,
                PasswordHash = hashedPassword,
                Email = email,
                DateOfBirth = DateTime.Now // placeholder; you can update this to collect DOB
            };

            _context.UserProfiles.Add(newUser);
            _context.SaveChanges();
            return true;
        }

        public bool LoginUser(string username, string password)
        {
            var user = _context.UserProfiles.FirstOrDefault(u => u.UserName == username);
            if (user == null) return false;

            var hashedInput = HashPassword(password);
            return user.PasswordHash == hashedInput;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
