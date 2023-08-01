using BlazorServer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost]
        public IActionResult GenerateTokenAsync(LoginViewModel model)
        {
            //查库 获取用户信息
            if (model.UserName.Equals("admin"))
            {

            }


            //JWT:Header Payload  Credential
            var key = "ajgiaerognvasdgjwedgahrtehadbghaerbghaerrgj5465";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var header = new JwtHeader(new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));
            var userClaims = new List<Claim>();
            userClaims.Add(new(ClaimTypes.Name, model.UserName));
            userClaims.Add(new(ClaimTypes.Role, "admin"));
            userClaims.Add(new(ClaimTypes.Email, "llp1520@163.com"));

            var payload = new JwtPayload(userClaims);

            var token = new JwtSecurityToken(header, payload);

            var handler = new JwtSecurityTokenHandler();

            string jwtToken = handler.WriteToken(token);
            return Ok(jwtToken);
        }
    }
}
