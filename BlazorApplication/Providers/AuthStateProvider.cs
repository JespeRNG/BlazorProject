using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorApplication.Model;
using BlazorApplication.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApplication.Providers
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accService;
        private readonly ILocalStorageService _localStorage;

        private const string AccessTokenKey = "access_token";

        public AuthStateProvider(IAccountService accService, ILocalStorageService localStorage, IUserService userService)
        {
            _accService = accService ?? throw new ArgumentNullException(nameof(accService));
            _localStorage = localStorage ?? throw new ArgumentNullException(nameof(localStorage));
            _userService = userService ?? throw new ArgumentNullException(nameof(accService));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            var userModel = await GetCurrentUser();
            if (userModel != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, userModel.Username),
                    new Claim(ClaimTypes.Role, userModel.Role)
                };
                identity = new ClaimsIdentity(claims, "Server authentication");
            }
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        private async Task<UserModel> GetCurrentUser()
        {
            if (await _localStorage.GetItem<string>(AccessTokenKey) == null) return null;
            var userModel = await _userService.GetCurrentUserInfoAsync();
            return userModel;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItem(AccessTokenKey);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<string> SignInAsync(LoginUserInputModel loginParameters)
        {
            var token = await _accService.AuthorizeUserAsync(loginParameters);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return token;
        }

        public async Task<bool> RegisterAsync(RegistrationUserInputModel registerParameters)
        {
            var statusCode = await _accService.RegisterUserAsync(registerParameters);
            if (statusCode != HttpStatusCode.OK) return false;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return true;
        }
    }
}