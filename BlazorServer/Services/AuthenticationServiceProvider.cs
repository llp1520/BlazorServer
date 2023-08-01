using Blazored.LocalStorage;
using BlazorServer.Model;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorServer.Services
{
    public class AuthenticationServiceProvider : AuthenticationStateProvider, IAuthService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorageService;

        public AuthenticationServiceProvider(HttpClient client, ILocalStorageService localStorageService)
        {
            _client = client;
            _localStorageService = localStorageService;
        }

        //登录
        public async Task SignInAsync(LoginViewModel user)
        {
            var response = await _client.PostAsJsonAsync("/api/authentication", user);
            response.EnsureSuccessStatusCode();
            var token = await response.Content.ReadAsStringAsync();//获取token

            //存取token redis,cookie,localstorage,...
            await StoreTokenAsync(token);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        //退出
        public async Task SignOutAsync()
        {
            // 清除token，你可以使用 LocalStorage 或者其他方式进行清除
            await _localStorageService.RemoveItemAsync("token");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        //存储token
        protected virtual async Task StoreTokenAsync(string token)
        {
            await _localStorageService.SetItemAsStringAsync("token", token);
        }

        //恢复token
        protected virtual async Task<string> RestoreTokenAsync()
        {
            return await _localStorageService.GetItemAsStringAsync("token");
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            ClaimsPrincipal principal = new();
            var token = await _localStorageService.GetItemAsStringAsync("token");
            if (!string.IsNullOrWhiteSpace(token))
            {
                //解析token
                JwtSecurityTokenHandler handler = new();
                var jwtToken = handler.ReadJwtToken(token);

                var claims = jwtToken.Claims;
                //验证模式
                var identity = new ClaimsIdentity(claims, "Admin");
                principal.AddIdentity(identity);
            }
            return new(principal);
            /*var claimsIdetity = new ClaimsIdentity("Customer");
            claimsIdetity.AddClaim(new(ClaimTypes.Name, "admin"));
            claimsIdetity.AddClaim(new(ClaimTypes.Email, "llp1520@163.com"));

            var claimsPricial = new ClaimsPrincipal(claimsIdetity);
            var state = new AuthenticationState(claimsPricial);
            var taskState = Task.FromResult(state);
            NotifyAuthenticationStateChanged(taskState);
            return taskState.Result;*/
        }

    }
}
