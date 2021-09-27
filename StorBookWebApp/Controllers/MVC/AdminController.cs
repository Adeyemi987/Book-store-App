using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StorBookWebApp.Core.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace StorBookWebApp.Controllers.MVC
{

    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Dashboard()
        {
            using var httpClient = new HttpClient();
            var auth = HttpContext.Session.GetString("TokenAdmin");

            if (auth == null)
            {
                return RedirectToAction("error");

            }
            var customer = await GetUser();
            var books = await GetBook();

            ViewData["GetUser"] = customer;
            ViewData["GetBook"] = books;
            return View();
        }

        public async Task<IActionResult> Book()
        {
            var book = await GetBook();

            return View(book);
        }

        public async Task<IActionResult> UserContact()
        {
            var customer = await GetUser();
            var books = await GetBook();

            ViewData["GetBook"] = books;
            return View(customer);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var Getuser = await GetUser();
            var check = Getuser.FirstOrDefault(x => x.Id == id);
            ViewData["getUser"] = check;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, UpdateUser user)
        {
            using (var _httpClient = new HttpClient())
            {

                var uri = $"http://storgroup.herokuapp.com/api/User/update/{id}";

                var serializedDoc = JsonConvert.SerializeObject(user);
                var requestContent = new StringContent(serializedDoc, Encoding.UTF8, "application/json-patch+json");
                var auth = HttpContext.Session.GetString("TokenAdmin");
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + auth);
                var response = await _httpClient.PutAsync(uri, requestContent);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> EditBook(string id)
        {
            var book = await GetBook();
            var check = book.FirstOrDefault(x => x.Id == id);
            ViewData["getBook"] = check;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(string id, BookUpdateDTo book)
        {
            using (var _httpClient = new HttpClient())
            {

                var uri = $"http://storgroup.herokuapp.com/api/Book/update/{id}";

                var serializedDoc = JsonConvert.SerializeObject(book);
                var requestContent = new StringContent(serializedDoc, Encoding.UTF8, "application/json-patch+json");
                var auth = HttpContext.Session.GetString("TokenAdmin");
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + auth);
                var response = await _httpClient.PutAsync(uri, requestContent);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> DeleteBook(string id)
        {
            var getBook = await GetBook();
            var check = getBook.FirstOrDefault(x => x.Id == id);
            ViewData["getBook"] = check;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(string id, string s)
        {
            using (var _httpClient = new HttpClient())
            {

                var uri = $"http://storgroup.herokuapp.com/api/book/delete/{id}";

                var auth = HttpContext.Session.GetString("TokenAdmin");
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + auth);
                var response = await _httpClient.DeleteAsync(uri);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index");
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserDto login)
        {
            using (var _httpClient = new HttpClient())
            {
                _httpClient.BaseAddress = new Uri("https://storgroup.herokuapp.com/api/Account/");

                var adduser = await _httpClient.PostAsJsonAsync<LoginUserDto>("login", login);
                var json = await adduser.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserModelDTo>(json);

                /*IDictionary<string, string> resultJson = JsonConvert.DeserializeObject<IDictionary<string, string>>(json);
                string token = "";
                resultJson.TryGetValue("token", out token);*/
                HttpContext.Session.SetString("TokenAdmin", user.Token);


                if (adduser.IsSuccessStatusCode)
                {
                    ViewData["loggedUser"] = user;
                    return RedirectToAction("Dashboard");
                }
                ModelState.AddModelError(string.Empty, "error");
            }

            return RedirectToAction("error");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserDto user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://storgroup.herokuapp.com/api/Account/");
                var addUser = await client.PostAsJsonAsync<RegisterNewUserDto>("Register", user);

                if (addUser.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "error");
            }
            return RedirectToAction("error");

        }
        public IActionResult AddUser()
        {
            return View();
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var Getuser = await GetUser();
            var check = Getuser.FirstOrDefault(x => x.Id == id);
            ViewData["getUser"] = check;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id, string s)
        {
            using (var _httpClient = new HttpClient())
            {

                var uri = $"http://storgroup.herokuapp.com/api/User/delete/{id}";

                var auth = HttpContext.Session.GetString("TokenAdmin");
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + auth);
                var response = await _httpClient.DeleteAsync(uri);
                response.EnsureSuccessStatusCode();

                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Search(string input)
        {
            var customer = await GetUser();
            var check = customer.Where(x => x.Email.Contains(input)
            || x.FirstName.Contains(input)
            || x.LastName.Contains(input)
            || x.PhoneNumber.Contains(input)).ToList();
            return View(customer);
        }

        private async Task<string> GetReleases(string url)
        {

            using var httpClient = new HttpClient();
            var auth = HttpContext.Session.GetString("TokenAdmin");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + auth);
            var response = await httpClient.GetStringAsync(new Uri(url));

            return response;
        }

        private async Task<IEnumerable<UserModelDTo>> GetUser()
        {
            IEnumerable<UserModelDTo> customer = null;
            string url = "https://storgroup.herokuapp.com/api/User/";
            var responseTask = await GetReleases(url);

            if (responseTask != null)
            {
                var list = JsonConvert.DeserializeObject<IEnumerable<UserModelDTo>>(responseTask);
                customer = list;
            }
            else
            {
                customer = Enumerable.Empty<UserModelDTo>();
            }
            return customer;
        }

        private async Task<IEnumerable<BookDTo>> GetBook()
        {
            IEnumerable<BookDTo> book = null;
            string url = "http://storgroup.herokuapp.com/api/Book";
            var responseTask = await GetReleases(url);

            if (responseTask != null)
            {
                var list = JsonConvert.DeserializeObject<IEnumerable<BookDTo>>(responseTask);
                book = list;
            }
            else
            {
                book = Enumerable.Empty<BookDTo>();
            }
            return book;
        }

    }
}
