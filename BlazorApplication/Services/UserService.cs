﻿using System;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorApplication.Model;
using System.Net.Http.Headers;
using System.Collections.Generic;
using BlazorApplication.Interfaces;
using BlazorApplication.DTO;

namespace BlazorApplication.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        private const string BaseUri = "https://localhost:44333/api/user/";
        private const string AccessTokenKey = "access_token";

        public UserService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _localStorageService = localStorageService ?? throw new ArgumentNullException(nameof(localStorageService));
        }

        public async Task<UserDTO> GetCurrentUserInfoAsync()
        {
            await PutTokenInAuthorizationHeader();
            var content = await (await _httpClient.GetAsync(BaseUri + "info")).Content.ReadAsStringAsync();
            var userDTO = JsonConvert.DeserializeObject<UserDTO>(content);
            return userDTO;
        }

        public async Task<List<UserDTO>> GetListOfUsersAsync()
        {
            await PutTokenInAuthorizationHeader();
            var content = await (await _httpClient.GetAsync(BaseUri + "list")).Content.ReadAsStringAsync();
            var userDTOList = JsonConvert.DeserializeObject<List<UserDTO>>(content);
            return userDTOList;
        }

        public async Task<HttpStatusCode> DeleteUser(long id)
        {
            await PutTokenInAuthorizationHeader();
            var response = await _httpClient.DeleteAsync(BaseUri + $"remove?id={id}");
            return response.StatusCode;
        }

        private async Task PutTokenInAuthorizationHeader()
        {
            var token = await _localStorageService.GetItem<string>(AccessTokenKey);
            //if (token == null) throw Exception(); if token == null do bad things
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
