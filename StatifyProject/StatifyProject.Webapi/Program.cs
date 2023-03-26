using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StatifyProject.Application.Infrastructure;
using StatifyProject.Application.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.Extensions.Logging;
using Webapi;

var builder = WebApplication.CreateBuilder(args);
// SpengernewsContext ist der DbContext, der im Application Project angelegt wurde.
// Aktiviere diese Zeile, wenn du den DB Context definiert hat.

builder.Services.AddDbContext<StatifyContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
        o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)));

// Wir wollen automatisch nach Controllern im Ordner Controllers suchen.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddHttpContextAccessor();

byte[] secret = Convert.FromBase64String(builder.Configuration["Secret"]);
builder.Services
    .AddAuthentication(options => options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secret),
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });

// Der Vue.JS Devserver läuft auf einem anderen Port, deswegen brauchen wir diese Konfiguration
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
        options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
}

// *************************************************************************************************
// APPLICATION
// *************************************************************************************************
var app = builder.Build();
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    // We will create a fresh sql server container in development mode. For performance reasons,
    // you can disable deleteAfterShutdown because in development mode the database is deleted
    // before it is generated.
    try
    {
        
        await app.UseSqlServerContainer(
            containerName: "Statify_sqlserver", version: "latest",
            connectionString: app.Configuration.GetConnectionString("Default"),
            deleteAfterShutdown: true);
    }
    catch (Exception e)
    {
        app.Logger.LogError(e.Message);
        return;
    }
    app.UseCors();
}

// Creating the database.
using (var scope = app.Services.CreateScope())
{
    using (var db = scope.ServiceProvider.GetRequiredService<StatifyContext>())
    {
        db.CreateDatabase(isDevelopment: app.Environment.IsDevelopment());
    }
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");
app.Run();