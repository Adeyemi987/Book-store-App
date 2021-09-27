using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StorBookWebApp.Core.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StorBookWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
                HttpContext.Session.SetString("TokenHome", user.Token);


                if (adduser.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "error");
            }

            return RedirectToAction("error");
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult Download()
        {
            using var httpClient = new HttpClient();
            var auth = HttpContext.Session.GetString("TokenHome");

            if (auth == null)
            {
                return RedirectToAction("Login");

            }
            return RedirectToAction("Detail");
        }

        private async Task<IEnumerable<BookDTo>> GetBook()
        {
            IEnumerable<BookDTo> book = null;
            using var httpClient = new HttpClient();
            string url = "http://storgroup.herokuapp.com/api/Book";
            var response = await httpClient.GetStringAsync(new Uri(url));

            if (response != null)
            {
                var list = JsonConvert.DeserializeObject<IEnumerable<BookDTo>>(response);
                book = list;
            }
            else
            {
                book = Enumerable.Empty<BookDTo>();
            }
            return book;
        }

        private async Task<string> GetReleases(string url)
        {
            using var httpClient = new HttpClient();
            var auth = HttpContext.Session.GetString("TokenHome");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + auth);
            var response = await httpClient.GetStringAsync(new Uri(url));

            return response;
        }
    }
}
