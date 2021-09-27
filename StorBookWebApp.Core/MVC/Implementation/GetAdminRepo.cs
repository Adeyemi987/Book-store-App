using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StorBookWebApp.Core.MVC
{
    public class GetAdminRepo : IGetAdminRepo
    {
        private readonly HttpClient _httpClient;

        public GetAdminRepo(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterNewUser(RegisterNewUserDto user)
        {
            var adduser = await _httpClient.PostAsJsonAsync<RegisterNewUserDto>("api/Authentication/Register", user);

            if (adduser.IsSuccessStatusCode)
            {
                return true;
            }
            throw new HttpRequestException();
        }

        public async Task<bool> LoginUser(LoginUserDto user)
        {
            _httpClient.BaseAddress = new Uri("http://localhost:50306/api/Authentication/");
            var adduser = await _httpClient.PostAsJsonAsync<LoginUserDto>("login", user);

            if (adduser.IsSuccessStatusCode)
            {
                return true;
            }
            throw new HttpRequestException();
        }

        private async Task<string> GetReleases(string url)
        {

            using var httpClient = new HttpClient();
            //var auth = HttpContext.Session.GetString("Token");
            //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + auth);
            var response = await httpClient.GetStringAsync(new Uri(url));

            return response;
        }

        private async Task<IEnumerable<UserModelDTo>> GetUser()
        {
            IEnumerable<UserModelDTo> customer = null;
            string url = "http://localhost:60938/api/User";
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
            string url = "http://localhost:60938/api/Book";
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
