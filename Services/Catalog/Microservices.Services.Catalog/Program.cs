using Amazon.Auth.AccessControlPolicy;
using Microservices.Services.Catalog.Mapping;
using Microservices.Services.Catalog.Services;
using Microservices.Services.Catalog.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(x=>
{
    // Adds authorize attribute to all controllers
    x.Filters.Add(new AuthorizeFilter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new EntityMapping()));

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.Authority = builder.Configuration["IdentityServerURL"];
    o.Audience = "resource_catalog";
    o.RequireHttpsMetadata = false;
    //o.TokenValidationParameters = new TokenValidationParameters
    //{
    //    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    //    ValidAudience = builder.Configuration["Jwt:Audience"],
    //    IssuerSigningKey = new SymmetricSecurityKey
    //    (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
    //    ValidateIssuer = true,
    //    ValidateAudience = true,
    //    ValidateLifetime = false,
    //    ValidateIssuerSigningKey = true
    //};
});

#region [ Get Service Provider and create default categories ]

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    // get proiver
    var serviceProiver = scope.ServiceProvider;

    // get category service
    var categoryService = serviceProiver.GetRequiredService<ICategoryService>();

    // if there is no category, create defaults,
    if (!categoryService.GetAllAsync().Result.Data.Any())
    {
        categoryService.CreateAsync(new Microservices.Services.Catalog.Dtos.CategoryDto() { Name = "Asp.Net" }).Wait();
        categoryService.CreateAsync(new Microservices.Services.Catalog.Dtos.CategoryDto() { Name = "C#" }).Wait();
        categoryService.CreateAsync(new Microservices.Services.Catalog.Dtos.CategoryDto() { Name = "Docker" }).Wait();
    }

}

#endregion 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
