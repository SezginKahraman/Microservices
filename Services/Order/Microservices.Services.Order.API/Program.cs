using MediatR;
using Microservices.Services.Order.Infrastructure;
using Microservices.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region [ Configuring application ]

#region [ IoC ]

#region [ MediatR ]

builder.Services.AddMediatR(typeof(Microservices.Services.Order.Application.Handlers.CreateOrderCommandHandler).Assembly);

#endregion [ MediatR ]

#region [ Context Accessor and Scoped ones ]

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
builder.Services.AddHttpContextAccessor();

#endregion

#region [ Add DbContext to IoC ]

builder.Services.AddDbContext<OrderDbContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), configure =>
    {
        configure.MigrationsAssembly("Microservices.Services.Order.Infrastructure");
    });
});

#endregion [ Add DbContext to IoC ]

#endregion [ IoC ]

#region [ Make secure the microservice by adding jwt ]

// when a client make request, the 'sub' keyword must be exist.
var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

//In order to avoid mapping for the userCalims that has been send by the jwt token
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.Authority = builder.Configuration["IdentityServerURL"];
    o.Audience = "resource_order";
    o.RequireHttpsMetadata = false;
});

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});

#endregion

#endregion [ Configuring application ]

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

#region [ Add authentication ]

app.UseAuthentication();

#endregion [ Authentication ]

app.MapControllers();

app.Run();
