using Microservices.Services.Basket.Services;
using Microservices.Services.Basket.Settings;
using Microservices.Shared.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("RedisSettings"));

// A connection to the redis will be opened entire life time of the application.
builder.Services.AddSingleton<RedisService>(serviceProvider =>
{
    var redisSettings = serviceProvider.GetRequiredService<IOptions<RedisSettings>>().Value;

    var redis = new RedisService(redisSettings.Host, redisSettings.Port);

    redis.Connect();

    return redis;
});

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
