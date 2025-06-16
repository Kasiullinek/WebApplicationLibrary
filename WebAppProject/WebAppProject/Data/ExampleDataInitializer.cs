using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAppProject.Models;

namespace WebAppProject.Data
{
    public static class ExampleDataInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var _context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Dodaj przykładowych użytkowników
                var userManager = serviceProvider.GetRequiredService<UserManager<UserModel>>();

                var passwordHasher = new PasswordHasher<UserModel>();

                if (await userManager.FindByNameAsync("jane.doe@example.com") == null)
                {
                    var user1 = new UserModel
                    {
                        UserName = "jane.doe@example.com",
                        NormalizedUserName = "JANE.DOE@EXAMPLE.COM",
                        Email = "jane.doe@example.com",
                        NormalizedEmail = "JANE.DOE@EXAMPLE.COM",
                        PhoneNumber = "555123456",
                        EmailConfirmed = false,
                        FirstName = "Jane",
                        LastName = "Doe",
                        DateOfBirth = new DateTime(1985, 4, 15),
                        PESEL = "85041512345",
                        Address = "123 Maple Street, Springfield"
                    };

                    user1.PasswordHash = passwordHasher.HashPassword(user1, "Jane@123");
                    await userManager.CreateAsync(user1);
                    await userManager.AddToRoleAsync(user1, "client");
                }

                if (await userManager.FindByNameAsync("john.smith@example.com") == null)
                {
                    var user2 = new UserModel
                    {
                        UserName = "john.smith@example.com",
                        NormalizedUserName = "JOHN.SMITH@EXAMPLE.COM",
                        Email = "john.smith@example.com",
                        NormalizedEmail = "JOHN.SMITH@EXAMPLE.COM",
                        PhoneNumber = "555987654",
                        EmailConfirmed = false,
                        FirstName = "John",
                        LastName = "Smith",
                        DateOfBirth = new DateTime(1978, 7, 23),
                        PESEL = "78072367890",
                        Address = "456 Oak Street, Shelbyville"
                    };

                    user2.PasswordHash = passwordHasher.HashPassword(user2, "John@123");
                    await userManager.CreateAsync(user2);
                    await userManager.AddToRoleAsync(user2, "client");
                }

                if (await userManager.FindByNameAsync("alice.jones@example.com") == null)
                {
                    var user3 = new UserModel
                    {
                        UserName = "alice.jones@example.com",
                        NormalizedUserName = "ALICE.JONES@EXAMPLE.COM",
                        Email = "alice.jones@example.com",
                        NormalizedEmail = "ALICE.JONES@EXAMPLE.COM",
                        PhoneNumber = "555654321",
                        EmailConfirmed = false,
                        FirstName = "Alice",
                        LastName = "Jones",
                        DateOfBirth = new DateTime(1992, 3, 11),
                        PESEL = "92031123456",
                        Address = "789 Birch Avenue, Capital City"
                    };

                    user3.PasswordHash = passwordHasher.HashPassword(user3, "Alice@123");
                    await userManager.CreateAsync(user3);
                    await userManager.AddToRoleAsync(user3, "client");
                }

                if (await userManager.FindByNameAsync("michael.brown@example.com") == null)
                {
                    var user4 = new UserModel
                    {
                        UserName = "michael.brown@example.com",
                        NormalizedUserName = "MICHAEL.BROWN@EXAMPLE.COM",
                        Email = "michael.brown@example.com",
                        NormalizedEmail = "MICHAEL.BROWN@EXAMPLE.COM",
                        PhoneNumber = "555333222",
                        EmailConfirmed = false,
                        FirstName = "Michael",
                        LastName = "Brown",
                        DateOfBirth = new DateTime(1982, 9, 2),
                        PESEL = "82090234567",
                        Address = "101 Pine Lane, Ogdenville"
                    };

                    user4.PasswordHash = passwordHasher.HashPassword(user4, "Michael@123");
                    await userManager.CreateAsync(user4);
                    await userManager.AddToRoleAsync(user4, "client");
                }

                if (await userManager.FindByNameAsync("emma.wilson@example.com") == null)
                {
                    var user5 = new UserModel
                    {
                        UserName = "emma.wilson@example.com",
                        NormalizedUserName = "EMMA.WILSON@EXAMPLE.COM",
                        Email = "emma.wilson@example.com",
                        NormalizedEmail = "EMMA.WILSON@EXAMPLE.COM",
                        PhoneNumber = "555111444",
                        EmailConfirmed = false,
                        FirstName = "Emma",
                        LastName = "Wilson",
                        DateOfBirth = new DateTime(1995, 11, 30),
                        PESEL = "95113045678",
                        Address = "202 Cedar Street, North Haverbrook"
                    };

                    user5.PasswordHash = passwordHasher.HashPassword(user5, "Emmma@123");
                    await userManager.CreateAsync(user5);
                    await userManager.AddToRoleAsync(user5, "client");
                }

                await _context.SaveChangesAsync();

                // Dodaj przykładowych autorów
                if (!_context.Authors.Any())
                {
                    _context.Authors.AddRange(
                        new AuthorModel { Name = "John Doe" },
                        new AuthorModel { Name = "Jane Smith" },
                        new AuthorModel { Name = "Emily Johnson" },
                        new AuthorModel { Name = "David Wilson" },
                        new AuthorModel { Name = "Sarah Davis" },
                        new AuthorModel { Name = "Michael Brown" },
                        new AuthorModel { Name = "Sarah Woodward" },
                        new AuthorModel { Name = "Jonathan Blake" },
                        new AuthorModel { Name = "Olivia Bennett" },
                        new AuthorModel { Name = "James Dawson" },
                        new AuthorModel { Name = "Anthony Adams" }
                    );
                    _context.SaveChanges();
                }

                // Dodaj przykładowych kategori wiekowych
                if (!_context.Categories.Any())
                {
                    _context.Categories.AddRange(
                        new CategoryModel { Name = "Kids" },
                        new CategoryModel { Name = "Teenagers" },
                        new CategoryModel { Name = "Adults" }
                    );
                    _context.SaveChanges();
                }

                // Dodaj przykładowych gatunków
                if (!_context.Genres.Any())
                {
                    _context.Genres.AddRange(
                        new GenreModel { Name = "Action" },
                        new GenreModel { Name = "Fantasy" },
                        new GenreModel { Name = "Mystery" },
                        new GenreModel { Name = "Romance" },
                        new GenreModel { Name = "Adventure" },
                        new GenreModel { Name = "Psychological Fiction" },
                        new GenreModel { Name = "Educational" },
                        new GenreModel { Name = "Thriller" },
                        new GenreModel { Name = "Biography" },
                        new GenreModel { Name = "Horror" },
                        new GenreModel { Name = "Historical Fiction" }
                    );
                    _context.SaveChanges();
                }

                // Dodaj przykładowych języków
                if (!_context.Languages.Any())
                {
                    _context.Languages.AddRange(
                        new LanguageModel { Name = "Polish" },
                        new LanguageModel { Name = "English" },
                        new LanguageModel { Name = "German" },
                        new LanguageModel { Name = "Russian" },
                        new LanguageModel { Name = "French" },
                        new LanguageModel { Name = "Spanish" },
                        new LanguageModel { Name = "Italian" }
                    );
                    _context.SaveChanges();
                }

                // Dodaj przykładowych wydawców
                if (!_context.Publishers.Any())
                {
                    _context.Publishers.AddRange(
                        new PublisherModel { Name = "Penquin Random House" },
                        new PublisherModel { Name = "HarperCollins" },
                        new PublisherModel { Name = "Simon & Schuster" },
                        new PublisherModel { Name = "Macmillan Publishers" },
                        new PublisherModel { Name = "Hachette Book Group" },
                        new PublisherModel { Name = "Hachette Livre" },
                        new PublisherModel { Name = "Wiley" },
                        new PublisherModel { Name = "Bloomsbury Publishing" }
                    );
                    _context.SaveChanges();
                }

                //Dodanie przykładowych książek
                if (!_context.Books.Any())
                {
                    _context.Books.AddRange(
                        new BookModel
                        {
                            Title = "The Great Adventure",
                            AuthorID = _context.Authors.First(a => a.Name == "John Doe").ID,
                            Description = "An epic adventure story.",
                            PublishDate = new DateTime(2015, 6, 10),
                            PublisherID = _context.Publishers.First(p => p.Name == "Penquin Random House").ID,
                            CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                            GenreID = _context.Genres.First(g => g.Name == "Adventure").ID,
                            LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                            ISBN = "9781234567897",
                            ImgUrl = "972bd317-d662-41c1-bf76-aff8e0959424_cover3.png"
                        },
                        new BookModel
                        {
                            Title = "Fantasy World",
                            AuthorID = _context.Authors.First(a => a.Name == "Jane Smith").ID,
                            Description = "A captivating fantasy novel.",
                            PublishDate = new DateTime(2018, 3, 15),
                            PublisherID = _context.Publishers.First(p => p.Name == "HarperCollins").ID,
                            CategoryID = _context.Categories.First(c => c.Name == "Teenagers").ID,
                            GenreID = _context.Genres.First(g => g.Name == "Fantasy").ID,
                            LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                            ISBN = "9787532410613",
                            ImgUrl = "43d70980-b319-4b72-8929-15293eb80451_cover4.png"
                        },
                        new BookModel
                        {
                            Title = "Mystery of the Lost City",
                            AuthorID = _context.Authors.First(a => a.Name == "Emily Johnson").ID,
                            Description = "A thrilling mystery novel.",
                            PublishDate = new DateTime(2020, 9, 20),
                            PublisherID = _context.Publishers.First(p => p.Name == "Simon & Schuster").ID,
                            CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                            GenreID = _context.Genres.First(g => g.Name == "Mystery").ID,
                            LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                            ISBN = "9783456789012",
                            ImgUrl = "fd933037-3c4e-412e-8627-bac2bae4a85e_cover1.png"
                        },
                        new BookModel
                        {
                            Title = "Romantic Escapade",
                            AuthorID = _context.Authors.First(a => a.Name == "David Wilson").ID,
                            Description = "A heartwarming romance story.",
                            PublishDate = new DateTime(2017, 2, 14),
                            PublisherID = _context.Publishers.First(p => p.Name == "Macmillan Publishers").ID,
                            CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                            GenreID = _context.Genres.First(g => g.Name == "Romance").ID,
                            LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                            ISBN = "9784567890123",
                            ImgUrl = "534ee60b-7877-4cf6-a8c0-25012148d32f_cover5.png"
                        },
                        new BookModel
                        {
                            Title = "Psychological Thriller",
                            AuthorID = _context.Authors.First(a => a.Name == "Sarah Davis").ID,
                            Description = "A gripping psychological fiction.",
                            PublishDate = new DateTime(2016, 8, 5),
                            PublisherID = _context.Publishers.First(p => p.Name == "Hachette Book Group").ID,
                            CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                            GenreID = _context.Genres.First(g => g.Name == "Psychological Fiction").ID,
                            LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                            ISBN = "9785678901234",
                            ImgUrl = "df55e3f3-c158-472e-a4fd-752118b34278_cover8.png"
                        },
                        new BookModel
                        {
                            Title = "Educational Insights",
                            AuthorID = _context.Authors.First(a => a.Name == "Michael Brown").ID,
                            Description = "An educational book on various topics.",
                            PublishDate = new DateTime(2019, 11, 11),
                            PublisherID = _context.Publishers.First(p => p.Name == "Hachette Livre").ID,
                            CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                            GenreID = _context.Genres.First(g => g.Name == "Educational").ID,
                            LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                            ISBN = "9786789012345",
                            ImgUrl = "cee1b024-5e25-47dd-b3cf-dfd4d71fee64_cover7.png"
                        },
                        new BookModel
                        {
                            Title = "Biography of a Legend",
                            AuthorID = _context.Authors.First(a => a.Name == "Sarah Woodward").ID,
                            Description = "The inspiring biography of a legendary figure.",
                            PublishDate = new DateTime(2013, 4, 10),
                            PublisherID = _context.Publishers.First(p => p.Name == "Wiley").ID,
                            CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                            GenreID = _context.Genres.First(g => g.Name == "Biography").ID,
                            LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                            ISBN = "9787890123456",
                            ImgUrl = "21769be9-3e07-4760-97cc-dd5bcb3c9f18_cover2.png"
                        },
                        new BookModel
                        {
                            Title = "Historical Saga",
                            AuthorID = _context.Authors.First(a => a.Name == "Jonathan Blake").ID,
                            Description = "A historical fiction novel.",
                            PublishDate = new DateTime(2021, 12, 25),
                            PublisherID = _context.Publishers.First(p => p.Name == "Bloomsbury Publishing").ID,
                            CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                            GenreID = _context.Genres.First(g => g.Name == "Historical Fiction").ID,
                            LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                            ISBN = "9788901234567",
                            ImgUrl = "f0c09d24-d440-4f6c-9320-3bf87c94c1d3_cover6.png"
                        },
                        new BookModel
                        {
                            Title = "Thriller Nights",
                            AuthorID = _context.Authors.First(a => a.Name == "Olivia Bennett").ID,
                            Description = "A spine-chilling thriller.",
                            PublishDate = new DateTime(2014, 10, 31),
                            PublisherID = _context.Publishers.First(p => p.Name == "Penquin Random House").ID,
                            CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                            GenreID = _context.Genres.First(g => g.Name == "Thriller").ID,
                            LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                            ISBN = "9789012345678",
                            ImgUrl = "257d672a-0908-47f8-8514-2419268409ae_cover10.png"
                        },
                        new BookModel
                        {
                            Title = "Horror Tales",
                            AuthorID = _context.Authors.First(a => a.Name == "James Dawson").ID,
                            Description = "A collection of horror stories.",
                            PublishDate = new DateTime(2012, 3, 22),
                            PublisherID = _context.Publishers.First(p => p.Name == "Macmillan Publishers").ID,
                            CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                            GenreID = _context.Genres.First(g => g.Name == "Horror").ID,
                            LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                            ISBN = "9780123456790",
                            ImgUrl = "e077b866-d2b9-47b8-9382-baa0c3a33de3_cover9.png"
                        },
                        new BookModel
                        {
                            Title = "The Last Frontier",
                            AuthorID = _context.Authors.First(a => a.Name == "Anthony Adams").ID,
                            Description = "An action-packed story of survival.",
                            PublishDate = new DateTime(2022, 5, 10),
                            PublisherID = _context.Publishers.First(p => p.Name == "HarperCollins").ID,
                            CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                            GenreID = _context.Genres.First(g => g.Name == "Action").ID,
                            LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                            ISBN = "9789876543210",
                            ImgUrl = "ef4fbb01-7130-452c-948b-f2b3fdfd2e64_cover11.png"
                        },
                        new BookModel
                        {
                            Title = "Mystery in the Mountains",
                            AuthorID = _context.Authors.First(a => a.Name == "John Doe").ID,
                            Description = "A gripping mystery set in the mountains.",
                            PublishDate = new DateTime(2018, 7, 15),
                            PublisherID = _context.Publishers.First(p => p.Name == "Simon & Schuster").ID,
                            CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                            GenreID = _context.Genres.First(g => g.Name == "Mystery").ID,
                            LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                            ISBN = "9780123456791",
                            ImgUrl = "c718ea82-cb32-44dc-b8f0-eb5954e454e3_cover12.png"
                        },
                        new BookModel
                        {
                            Title = "Journey to the Stars",
                            AuthorID = _context.Authors.First(a => a.Name == "Jane Smith").ID,
                            Description = "An adventurous journey through space.",
                            PublishDate = new DateTime(2020, 2, 20),
                            PublisherID = _context.Publishers.First(p => p.Name == "Macmillan Publishers").ID,
                            CategoryID = _context.Categories.First(c => c.Name == "Teenagers").ID,
                            GenreID = _context.Genres.First(g => g.Name == "Adventure").ID,
                            LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                            ISBN = "9782345678902",
                            ImgUrl = "232e4b79-d36e-4c50-a222-3a0b5195261e_cover13.png"
                        },
                        new BookModel
                        {
                            Title = "The Haunted House",
                            AuthorID = _context.Authors.First(a => a.Name == "Emily Johnson").ID,
                            Description = "A horror story about a haunted house.",
                            PublishDate = new DateTime(2017, 10, 31),
                            PublisherID = _context.Publishers.First(p => p.Name == "HarperCollins").ID,
                            CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                            GenreID = _context.Genres.First(g => g.Name == "Horror").ID,
                            LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                            ISBN = "9783456789013",
                            ImgUrl = "c26b9e62-e85b-4cba-8bcd-0da9c050d12c_cover14.png"
                        },
                        new BookModel
                        {
                            Title = "Secrets of the Ocean",
                            AuthorID = _context.Authors.First(a => a.Name == "David Wilson").ID,
                            Description = "A thrilling tale of underwater adventure.",
                            PublishDate = new DateTime(2019, 5, 22),
                            PublisherID = _context.Publishers.First(p => p.Name == "Hachette Book Group").ID,
                            CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                            GenreID = _context.Genres.First(g => g.Name == "Action").ID,
                            LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                            ISBN = "9784567890124",
                            ImgUrl = "e05279a3-7bf2-41d5-aad3-328d9ed675c4_cover15.png"
                        },
                         new BookModel
                         {
                             Title = "Love in Paris",
                             AuthorID = _context.Authors.First(a => a.Name == "Sarah Davis").ID,
                             Description = "A romance story set in Paris.",
                             PublishDate = new DateTime(2016, 2, 14),
                             PublisherID = _context.Publishers.First(p => p.Name == "Penquin Random House").ID,
                             CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                             GenreID = _context.Genres.First(g => g.Name == "Romance").ID,
                             LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                             ISBN = "9785678901235",
                             ImgUrl = "904752cf-5045-4ab1-b5a1-9c857e286dc6_cover16.png"
                         },
                         new BookModel
                         {
                             Title = "The Science of Everything",
                             AuthorID = _context.Authors.First(a => a.Name == "Michael Brown").ID,
                             Description = "An educational book on various scientific topics.",
                             PublishDate = new DateTime(2021, 9, 18),
                             PublisherID = _context.Publishers.First(p => p.Name == "Hachette Livre").ID,
                             CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                             GenreID = _context.Genres.First(g => g.Name == "Educational").ID,
                             LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                             ISBN = "9786789012346",
                             ImgUrl = "bd632a59-d7b3-49fb-b54b-f9b50348d35a_cover17.png"
                         },
                         new BookModel
                         {
                             Title = "The Last Hero",
                             AuthorID = _context.Authors.First(a => a.Name == "Sarah Woodward").ID,
                             Description = "An inspiring biography of a war hero.",
                             PublishDate = new DateTime(2014, 4, 10),
                             PublisherID = _context.Publishers.First(p => p.Name == "Wiley").ID,
                             CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                             GenreID = _context.Genres.First(g => g.Name == "Biography").ID,
                             LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                             ISBN = "9787890123457",
                             ImgUrl = "7067998a-9530-4a0d-bb8d-58706ac1df79_cover18.png"
                         },
                         new BookModel
                         {
                             Title = "The War Chronicles",
                             AuthorID = _context.Authors.First(a => a.Name == "Jonathan Blake").ID,
                             Description = "A historical fiction novel about war.",
                             PublishDate = new DateTime(2023, 1, 30),
                             PublisherID = _context.Publishers.First(p => p.Name == "Bloomsbury Publishing").ID,
                             CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                             GenreID = _context.Genres.First(g => g.Name == "Historical Fiction").ID,
                             LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                             ISBN = "9788901234568",
                             ImgUrl = "d7652fde-6f2f-445e-a3f7-4cce8e6c9cfd_cover19.png"
                         },
                         new BookModel
                         {
                             Title = "Psychological Warfare",
                             AuthorID = _context.Authors.First(a => a.Name == "Olivia Bennett").ID,
                             Description = "A psychological thriller.",
                             PublishDate = new DateTime(2018, 6, 12),
                             PublisherID = _context.Publishers.First(p => p.Name == "Penquin Random House").ID,
                             CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                             GenreID = _context.Genres.First(g => g.Name == "Psychological Fiction").ID,
                             LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                             ISBN = "9789012345679",
                             ImgUrl = "19b6d9ad-5cf1-4fe7-b00e-98a238cf82e9_cover20.png"
                         },
                         new BookModel
                         {
                             Title = "Adventures in Space",
                             AuthorID = _context.Authors.First(a => a.Name == "James Dawson").ID,
                             Description = "A sci-fi adventure novel.",
                             PublishDate = new DateTime(2015, 5, 27),
                             PublisherID = _context.Publishers.First(p => p.Name == "Macmillan Publishers").ID,
                             CategoryID = _context.Categories.First(c => c.Name == "Adults").ID,
                             GenreID = _context.Genres.First(g => g.Name == "Action").ID,
                             LanguageID = _context.Languages.First(l => l.Name == "English").ID,
                             ISBN = "9780123456792",
                             ImgUrl = "229189ee-61c4-4654-8564-ec91da6afb87_cover21.png"
                         }

                    );
                    _context.SaveChanges();
                }

                // Dodaj nowe zamówienia
                if (!_context.Orders.Any())
                {
                    _context.Orders.AddRange(
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "The Great Adventure").ID,
                        UserID = _context.Users.First(u => u.UserName == "jane.doe@example.com").Id,
                        OrderDate = new DateTime(2024, 12, 1, 14, 30, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Fantasy World").ID,
                        UserID = _context.Users.First(u => u.UserName == "john.smith@example.com").Id,
                        OrderDate = new DateTime(2024, 12, 3, 10, 15, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Mystery of the Lost City").ID,
                        UserID = _context.Users.First(u => u.UserName == "alice.jones@example.com").Id,
                        OrderDate = new DateTime(2024, 12, 5, 16, 45, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Romantic Escapade").ID,
                        UserID = _context.Users.First(u => u.UserName == "michael.brown@example.com").Id,
                        OrderDate = new DateTime(2024, 12, 7, 13, 20, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Psychological Thriller").ID,
                        UserID = _context.Users.First(u => u.UserName == "emma.wilson@example.com").Id,
                        OrderDate = new DateTime(2024, 12, 10, 18, 50, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Educational Insights").ID,
                        UserID = _context.Users.First(u => u.UserName == "jane.doe@example.com").Id,
                        OrderDate = new DateTime(2024, 12, 12, 9, 40, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Biography of a Legend").ID,
                        UserID = _context.Users.First(u => u.UserName == "john.smith@example.com").Id,
                        OrderDate = new DateTime(2024, 12, 14, 15, 10, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Historical Saga").ID,
                        UserID = _context.Users.First(u => u.UserName == "alice.jones@example.com").Id,
                        OrderDate = new DateTime(2024, 12, 16, 11, 25, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Thriller Nights").ID,
                        UserID = _context.Users.First(u => u.UserName == "michael.brown@example.com").Id,
                        OrderDate = new DateTime(2024, 12, 18, 17, 35, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Love in Paris").ID,
                        UserID = _context.Users.First(u => u.UserName == "emma.wilson@example.com").Id,
                        OrderDate = new DateTime(2024, 12, 20, 20, 5, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "The Haunted House").ID,
                        UserID = _context.Users.First(u => u.UserName == "jane.doe@example.com").Id,
                        OrderDate = new DateTime(2024, 12, 22, 14, 00, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Secrets of the Ocean").ID,
                        UserID = _context.Users.First(u => u.UserName == "john.smith@example.com").Id,
                        OrderDate = new DateTime(2024, 12, 24, 11, 30, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "The Science of Everything").ID,
                        UserID = _context.Users.First(u => u.UserName == "alice.jones@example.com").Id,
                        OrderDate = new DateTime(2024, 12, 26, 9, 15, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "The Last Hero").ID,
                        UserID = _context.Users.First(u => u.UserName == "michael.brown@example.com").Id,
                        OrderDate = new DateTime(2024, 12, 28, 13, 45, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "The War Chronicles").ID,
                        UserID = _context.Users.First(u => u.UserName == "emma.wilson@example.com").Id,
                        OrderDate = new DateTime(2024, 12, 30, 16, 20, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Psychological Warfare").ID,
                        UserID = _context.Users.First(u => u.UserName == "jane.doe@example.com").Id,
                        OrderDate = new DateTime(2024, 1, 2, 14, 10, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Adventures in Space").ID,
                        UserID = _context.Users.First(u => u.UserName == "john.smith@example.com").Id,
                        OrderDate = new DateTime(2023, 1, 4, 10, 00, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Thriller Nights").ID,
                        UserID = _context.Users.First(u => u.UserName == "alice.jones@example.com").Id,
                        OrderDate = new DateTime(2023, 1, 6, 18, 30, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Journey to the Stars").ID,
                        UserID = _context.Users.First(u => u.UserName == "michael.brown@example.com").Id,
                        OrderDate = new DateTime(2024, 1, 8, 12, 50, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Mystery in the Mountains").ID,
                        UserID = _context.Users.First(u => u.UserName == "emma.wilson@example.com").Id,
                        OrderDate = new DateTime(2024, 1, 10, 15, 25, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Love in Paris").ID,
                        UserID = _context.Users.First(u => u.UserName == "jane.doe@example.com").Id,
                        OrderDate = new DateTime(2023, 1, 12, 14, 45, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Secrets of the Ocean").ID,
                        UserID = _context.Users.First(u => u.UserName == "john.smith@example.com").Id,
                        OrderDate = new DateTime(2024, 1, 14, 11, 30, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "The Haunted House").ID,
                        UserID = _context.Users.First(u => u.UserName == "alice.jones@example.com").Id,
                        OrderDate = new DateTime(2024, 1, 16, 9, 15, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "The Science of Everything").ID,
                        UserID = _context.Users.First(u => u.UserName == "michael.brown@example.com").Id,
                        OrderDate = new DateTime(2023, 1, 18, 13, 45, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "The Last Hero").ID,
                        UserID = _context.Users.First(u => u.UserName == "emma.wilson@example.com").Id,
                        OrderDate = new DateTime(2024, 1, 20, 16, 20, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "The War Chronicles").ID,
                        UserID = _context.Users.First(u => u.UserName == "jane.doe@example.com").Id,
                        OrderDate = new DateTime(2023, 1, 22, 14, 10, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Psychological Warfare").ID,
                        UserID = _context.Users.First(u => u.UserName == "john.smith@example.com").Id,
                        OrderDate = new DateTime(2023, 1, 24, 10, 00, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Adventures in Space").ID,
                        UserID = _context.Users.First(u => u.UserName == "alice.jones@example.com").Id,
                        OrderDate = new DateTime(2023, 1, 26, 18, 30, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Thriller Nights").ID,
                        UserID = _context.Users.First(u => u.UserName == "michael.brown@example.com").Id,
                        OrderDate = new DateTime(2023, 1, 28, 12, 50, 0)
                    },
                    new OrderModel
                    {
                        BookID = _context.Books.First(b => b.Title == "Journey to the Stars").ID,
                        UserID = _context.Users.First(u => u.UserName == "emma.wilson@example.com").Id,
                        OrderDate = new DateTime(2023, 1, 30, 15, 25, 0)
                    }

                    );
                    _context.SaveChanges();
                }
                
            }
        }
    }
}
