using System.Collections.Generic;
using System.Linq;
using TimeSheetApp.API.Models;
using Newtonsoft.Json;

namespace TimeSheetApp.API.Data
{
    public class Seed
    {
        public static void SeedUsers (DataContext context)
        {
            if (!context.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    byte[] passwordhash, passwordsalt;
                    CreatePasswordHash("password", out passwordhash, out passwordsalt);

                    user.PasswordHash = passwordhash;
                    user.PasswordSalt = passwordsalt;
                    user.UserName = user.UserName.ToLower();
                    context.Users.Add (user);
                }

                context.SaveChanges();
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static void SeedProjects (DataContext context)
        {
            if (!context.Projects.Any())
            {
                var projectData = System.IO.File.ReadAllText("Data/ProjectSeedData.json");
                var projects = JsonConvert.DeserializeObject<List<Project>>(projectData);
                foreach (var project in projects)
                {
                    context.Projects.Add (project);
                }

                context.SaveChanges();
            }
        }
    }
}