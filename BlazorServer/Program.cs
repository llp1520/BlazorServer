using Blazored.LocalStorage;
using BlazorServer.Data;
using BlazorServer.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddHttpClient();
builder.Services.AddMudServices();
builder.Services.AddMvc();
builder.Services.AddSwaggerDocument();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(sp.GetService<NavigationManager>().BaseUri) });
builder.Services.AddTransient<IAuthService,AuthenticationServiceProvider>();
builder.Services.AddTransient<AuthenticationStateProvider,AuthenticationServiceProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();


app.UseRouting();
app.MapDefaultControllerRoute();
app.UseOpenApi().UseSwaggerUi3();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.Run();
