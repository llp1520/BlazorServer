using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using BlazorServer;
using BlazorServer.Shared;
using MudBlazor;
using BlazorServer.Model;

namespace BlazorServer.Components.Login
{
    public partial class Login
    {
        LoginViewModel Model { get; set; } = new() { UserName = "admin", Password = "123" };

        string message = "���¼";
        async Task SubmitAsync(EditContext context)
        {
            try
            {
                //await AuthenticationService.SignInAsync(Model);

                //��¼�ɹ�
                NavigationManager.NavigateTo("/");
            }
            catch (InvalidOperationException ex)//��¼ʧ��
            {

            }
        }
    }
}