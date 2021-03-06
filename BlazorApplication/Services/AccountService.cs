using System;
using System.Net;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BlazorApplication.DTO;
using System.Threading.Tasks;
using BlazorApplication.Interfaces;

namespace BlazorApplication.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        private const string BaseUri = "https://localhost:44333/api/authentication/";
        private const string RegistrationEndpoint = "registration";
        private const string AuthorizeEndpoint = "token";

        public AccountService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _localStorageService = localStorageService ?? throw new ArgumentNullException(nameof(localStorageService));
        }

        public async Task<HttpStatusCode> RegisterUserAsync(UserDTO userDTO)
        {
            if (userDTO == null) throw new ArgumentNullException(nameof(userDTO));

            var content = new StringContent(JsonConvert.SerializeObject(userDTO), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(BaseUri + RegistrationEndpoint, content);
            return response.StatusCode;
        }

        public async Task<string> AuthorizeUserAsync(UserDTO userDTO)
        {
            if (userDTO == null) throw new ArgumentNullException(nameof(userDTO));

            var content = new StringContent(JsonConvert.SerializeObject(userDTO), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(BaseUri + AuthorizeEndpoint, content);

            if (response.StatusCode == HttpStatusCode.Conflict) return null;

            var json = await response.Content.ReadAsStringAsync();
            var token = JObject.Parse(json)["access_token"].ToString();
            await _localStorageService.SetItem<string>("access_token", token);
            return token;
        }
    }
}
