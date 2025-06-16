using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Models;

namespace WebAppProject.Data
{
    // Klasa kontekstu aplikacji, dziedzicząca po IdentityDbContext
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Konfiguracja modelu przy tworzeniu modelu
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Stałe identyfikatory ról
            string adminRoleId = "01a683b3-8d2b-4e5a-bafa-31ebdab54fd7";
            string clientRoleId = "bdd6e0b5-3115-4267-b0b1-3fcfe92cb88e";

            // Dodanie roli "admin" do bazy danych
            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";
            admin.Id = adminRoleId;

            // Dodanie roli "client" do bazy danych
            var client = new IdentityRole("client");
            client.NormalizedName = "client";
            client.Id = clientRoleId;

            // Dodanie predefiniowanych ról do modelu
            builder.Entity<IdentityRole>().HasData(admin, client);
        }

        // DbSet reprezentujący zamówienia
        public DbSet<OrderModel> Orders { get; set; }

        // DbSet reprezentujący koszyki
        public DbSet<CartModel> Carts { get; set; }

        // DbSet reprezentujący użytkowników
        public DbSet<UserModel> userModel { get; set; }

        // DbSet reprezentujący książki
        public DbSet<BookModel> Books { get; set; }

        // DbSet reprezentujący autorów
        public DbSet<AuthorModel> Authors { get; set; }

        // DbSet reprezentujący kategorie
        public DbSet<CategoryModel> Categories { get; set; }

        // DbSet reprezentujący gatunki
        public DbSet<GenreModel> Genres { get; set; }

        // DbSet reprezentujący języki
        public DbSet<LanguageModel> Languages { get; set; }

        // DbSet reprezentujący wydawców
        public DbSet<PublisherModel> Publishers { get; set; }
    }
}
