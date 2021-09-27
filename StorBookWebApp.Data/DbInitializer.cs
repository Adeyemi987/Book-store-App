using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using StorBookWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StorBookWebApp.Data
{
    public class DbInitializer
    {
        public DbInitializer(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment webHostEnvironment { get; }

        public string GetPath(string folderName, string fileName)
        {
            return Path.Combine(webHostEnvironment.WebRootPath, folderName, fileName);
        }
        public async Task SeedData(ApplicationDbContext context,
            UserManager<AppUser> userManager)
        {
            context.Database.EnsureCreated();
            if (!context.Books.Any())
            {
                var boookData = File.ReadAllText(GetPath("Data", "Books.json"));
                var listOfBooks = JsonConvert.DeserializeObject<List<Book>>(boookData);
                await context.Books.AddRangeAsync(listOfBooks);
            }
            if (!context.Authors.Any())
            {
                var authorData = File.ReadAllText(GetPath("Data", "Authors.json"));
                var listOfAuthors = JsonConvert.DeserializeObject<List<Author>>(authorData);
                await context.Authors.AddRangeAsync(listOfAuthors);
            }
            if (!context.Categories.Any())
            {
                var categoriesData = File.ReadAllText(GetPath("Data", "Categories.json"));
                var listOfCategories = JsonConvert.DeserializeObject<List<Category>>(categoriesData);
                await context.Categories.AddRangeAsync(listOfCategories);
            }
            if (!context.Genres.Any())
            {
                var genresData = File.ReadAllText(GetPath("Data", "Genres.json"));
                var listOfGenres = JsonConvert.DeserializeObject<List<Genre>>(genresData);
                await context.Genres.AddRangeAsync(listOfGenres);
            }
            if (!context.BookGenres.Any())
            {
                var data = File.ReadAllText(GetPath("Data", "BookGenres.json"));
                var listOfBookGenres = JsonConvert.DeserializeObject<List<BookGenre>>(data);
                await context.BookGenres.AddRangeAsync(listOfBookGenres);
            }
            if (!context.BookAuthors.Any())
            {
                var data = File.ReadAllText(GetPath("Data", "BookAuthors.json"));
                var listOfBookAuthors = JsonConvert.DeserializeObject<List<BookAuthor>>(data);
                await context.BookAuthors.AddRangeAsync(listOfBookAuthors);
            }
            if (!userManager.Users.Any())
            {
                List<AppUser> users = new()
                {
                    new AppUser
                    {
                        FirstName = "Obinna",
                        LastName = "Asiegbulam",
                        Email = "obinna@gmail.com",
                        UserName = "obinna@gmail.com",
                        PhoneNumber = "08036021321",
                        PhotoUrl = "default.jpg",
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    },
                    new AppUser
                    {
                        FirstName = "Samuel",
                        LastName = "Adeosun",
                        Email = "samuel@gmail.com",
                        UserName = "samuel@gmail.com",
                        PhoneNumber = "08036022221",
                        PhotoUrl = "default.jpg",
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    },
                    new AppUser
                    {
                        FirstName = "Reagan",
                        LastName = "Reuben",
                        Email = "reagan@gmail.com",
                        UserName = "reagan@gmail.com",
                        PhoneNumber = "08036024421",
                        PhotoUrl = "default.jpg",
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    },
                    new AppUser
                    {
                        FirstName = "Tubosun",
                        LastName = "Adeyemi",
                        Email = "tubosun@gmail.com",
                        UserName = "tubosun@gmail.com",
                        PhoneNumber = "08035431321",
                        PhotoUrl = "default.jpg",
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    }
                };
                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Asd@123");
                    if (user == users[0])
                    {
                        await userManager.AddToRoleAsync(user, "SuperAdmin");
                    }
                    else
                        await userManager.AddToRoleAsync(user, "Admin");
                }

            }


            context.SaveChanges();
        }
    }
}
