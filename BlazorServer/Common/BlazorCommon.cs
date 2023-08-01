using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using NJsonSchema.Annotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazorServer.Common
{
    public class BlazorCommon : ComponentBase
    {
        [Inject]
        [NotNull]
        AuthenticationStateProvider? _authenticationStateProvider { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState>? authenticationState { get; set; }

        public async Task<string> GetIP()
        {
            //_authenticationStateProvider=
            var user = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;
            //var UserId = user.FindFirst(u => u.Type.Contains("nameidentifier"))?.Value;
            return user.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Country).Value;


        }

        private static ClaimsIdentity identity;
        public static ClaimsIdentity Identity
        {
            get
            {
                if (identity == null)
                {
                    identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                }
                return identity;
            }
        }
    }
}
