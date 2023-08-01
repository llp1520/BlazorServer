using BlazorServer.Model;

namespace BlazorServer.Services
{
    internal interface IAuthService
    {
        //登录
        Task SignInAsync(LoginViewModel user);

        //退出
        Task SignOutAsync();

        //当前登录用户信息
        // Task<CurrentUser> CurrentUserInfo();
    }
}
