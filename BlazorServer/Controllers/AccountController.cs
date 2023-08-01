using BlazorServer.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            //var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            BlazorCommon.Identity.AddClaim(new Claim(ClaimTypes.Name, userName));
            var IP = HttpContext.Connection.RemoteIpAddress.ToString();
            BlazorCommon.Identity.AddClaim(new Claim(ClaimTypes.Country, IP));
            BlazorCommon.Identity.AddClaim(new Claim(ClaimTypes.Role, "Administrator"));
            BlazorCommon.Identity.AddClaim(new Claim(ClaimTypes.Role, "PrintGroup"));

            //HttpContext.Response.WriteAsync("<script>用户名或密码错误</script>");
            await HttpContext.SignInAsync(new ClaimsPrincipal(BlazorCommon.Identity));
            if (userName == "admin")
            {
                return Redirect("/");
            }
            else
            {
                return Redirect("/Account/Login?Err=1&userName=" + userName);
                //return null;
            }

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Account/Login");
        }
    }
}
