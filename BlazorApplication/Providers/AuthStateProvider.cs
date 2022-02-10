using System;
using System.Net;
using AutoMapper;
using BlazorApplication.DTO;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorApplication.Model;
using BlazorApplication.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApplication.Providers
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IAccountService _accService;
        private readonly ILocalStorageService _localStorage;

        private const string AccessTokenKey = "access_token";

        public AuthStateProvider(IAccountService accService,ILocalStorageService localStorage,
            IUserService userService, IMapper mapper)
        {
            _accService = accService ?? throw new ArgumentNullException(nameof(accService));
            _localStorage = localStorage ?? throw new ArgumentNullException(nameof(localStorage));
            _userService = userService ?? throw new ArgumentNullException(nameof(accService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            var userDTO = await GetCurrentUser();
            if (userDTO != null)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, userDTO.Username),
                    new Claim(ClaimTypes.Role, userDTO.Role)
                };
                identity = new ClaimsIdentity(claims, "Server authentication");
            }
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        private async Task<UserDTO> GetCurrentUser()
        {
            if (await _localStorage.GetItem<string>(AccessTokenKey) == null) return null;
            var userDTO = await _userService.GetCurrentUserInfoAsync();
            return userDTO;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItem(AccessTokenKey);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<string> SignInAsync(LoginUserInputModel loginParameters)
        {
            var userDTO = _mapper.Map<UserDTO>(loginParameters);
            var token = await _accService.AuthorizeUserAsync(userDTO);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return token;
        }

        public async Task<bool> RegisterAsync(RegistrationUserInputModel registerParameters)
        {
            var userDTO = _mapper.Map<UserDTO>(registerParameters);
            var statusCode = await _accService.RegisterUserAsync(userDTO);
            if (statusCode != HttpStatusCode.OK) return false;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return true;
        }
    }
}