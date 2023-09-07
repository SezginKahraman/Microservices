using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

#region [ Configuring application ]

#region [ Make secure the gateway by adding jwt ]

builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationSchema", o =>
{
    o.Authority = builder.Configuration["IdentityServerURL"];
    o.Audience = "resource_gateway";
    o.RequireHttpsMetadata = false;
});

#endregion

#endregion [ Configuring application ]

builder.Services.AddOcelot();

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName.ToLower()}.json").AddEnvironmentVariables();
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

#region [ Add authentication ]

app.UseAuthentication();

#endregion [ Authentication ]

await app.UseOcelot();

app.Run();
