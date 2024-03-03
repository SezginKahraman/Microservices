using FluentValidation.AspNetCore;
using Microservices.Shared.Services;
using Microservices.UI.Extensions;
using Microservices.UI.Handler;
using Microservices.UI.Helpers;
using Microservices.UI.Models;
using Microservices.UI.Services;
using Microservices.UI.Services.Interfaces;
using Microservices.UI.Validators;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.          // it is enough to give 1 class for validator, is is going to scan assembly which has that abstract validators.
builder.Services.AddControllersWithViews().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CourseCreateInputValidator>());

#region [ IoC Containers ]

#region [ Scoping ]

#region Helpers

builder.Services.AddSingleton<PhotoHelper>();

#endregion

#region [ Handlers ]

builder.Services.AddScoped<ResourceOwnerPasswordTokenHandler>();

builder.Services.AddScoped<ClientCredentialTokenHandler>();

#endregion [ Handlers ]

#region [ Identity Service ]

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

#endregion [ Identity Service  ]

#endregion [ Scoping ]

#region [ AppSettings ]

builder.Services.Configure<ServiceApiSettings>(builder.Configuration.GetSection("ServiceApiSettings"));

builder.Services.Configure<ClientSettings>(builder.Configuration.GetSection("ClientSettings"));

#endregion [ AppSettings ]

#region [ Http ]

builder.Services.AddHttpClientServices(builder.Configuration);

#region [ Context ]

builder.Services.AddHttpContextAccessor();

#endregion [ Context ]

#region [ Auth ]

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opts =>
{
    opts.LoginPath = "/Auth/SignIn";
    opts.ExpireTimeSpan = TimeSpan.FromDays(60);
    opts.SlidingExpiration = true;
    opts.Cookie.Name = "webcookie";
});

#endregion [ Auth ]

#endregion [ Http ]

#endregion [ IoC Containers ]

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
