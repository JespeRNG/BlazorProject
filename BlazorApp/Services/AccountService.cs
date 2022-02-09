using System;
using System.Net.Http;
using BlazorApp.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlazorApp.Services
{
    public class AccountService : IAccountService
    {    
        private readonly HttpClient _httpClient;
        private const string BaseUri = "https://localhost:44333/api/";
         
        public AccountService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        
        public async Task RegisterUserAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:44333/api/authentication/test");
            /*var values = new Dictionary<string, string>
            {
                { "username", "hellouser" },
                { "password", "string" },
                { "firstname", "Firstname" },
                { "lastname", "Lastname" }
            };
            var content = new FormUrlEncodedContent(values);
            var response = await _httpClient.PostAsync(BaseUri + "registration", content);*/
        }
    }
}