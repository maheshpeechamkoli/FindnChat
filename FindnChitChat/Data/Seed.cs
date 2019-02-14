using System.Collections.Generic;
using System.Linq;
using FindnChitChat.Model;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace FindnChitChat.Data
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        public Seed(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public void SeedUsers()
        {
            if (!_userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    _userManager.CreateAsync(user, "password").Wait();
                    // byte[] passwordHash , passwordSalt;
                    // CreatePasswordHash("password",out passwordHash,out passwordSalt);

                    // user.PasswordHash = passwordHash;
                    // user.PasswordSalt = passwordSalt;
                    // user.UserName = user.UserName.ToLower();

                    // _context.Users.Add(user);
                }
            }
            // _context.SaveChanges();
        }

        // private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        // {
        //     using(var hmac = new System.Security.Cryptography.HMACSHA512())
        //     {
        //         passwordSalt = hmac.Key;
        //         passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //     }
        // }

        
    }
}