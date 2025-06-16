using Poliedro.Report.Api;
using Poliedro.Billing.Api.Common.Configurations;
using Poliedro.Report.Application;
using Poliedro.Report.Application.Ports.Redis;
using Poliedro.Report.Infraestructure.Persistence.Mysql;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Configura el logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services
    .AddWebApi()
    .AddApplication()
    .AddPersistence(builder.Configuration);
builder.Services.Configure<RedisConfig>(builder.Configuration.GetSection("Redis"));
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionConfiguration>();
});

builder.Services.AddRouting(routing => routing.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("PoliedroReport", policy =>
    {
        var allowedOrigins = config.GetSection("AllowedOrigins").Get<List<string>>();
        policy.WithOrigins(allowedOrigins.ToArray())
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();
app.UseCors("PoliedroReport");
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.MapControllers();
app.Run();
