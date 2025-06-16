using Microsoft.AspNetCore.Identity;
using WebAppProject.Models;

namespace WebAppProject.Data
{
    public static class AdminInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<UserModel>>();

            // Dodawanie przykładowego admina
            if (await userManager.FindByNameAsync("admin@example.com") == null) 
            {
                var adminUser = new UserModel
                {
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    PhoneNumber = "555888999",
                    EmailConfirmed = false,
                    FirstName = "Default",
                    LastName = "Admin",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PESEL = "12345678901",
                    Address = "Admin Street 1, Admin City"
                }; 
                
                var passwordHasher = new PasswordHasher<UserModel>();
                adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123"); 
                await userManager.CreateAsync(adminUser); 
                await userManager.AddToRoleAsync(adminUser, "admin"); 
            }
        }
    }
}
