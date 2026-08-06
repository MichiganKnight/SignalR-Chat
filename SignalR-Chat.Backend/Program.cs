using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using SignalR_Chat.Backend.Data;
using SignalR_Chat.Backend.Entities;
using SignalR_Chat.Backend.Hubs;
using SignalR_Chat.Backend.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

const string frontendPolicy = "FrontendPolicy";

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(frontendPolicy, policy =>
    {
        policy.WithOrigins("https://localhost:7137").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });    
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddScoped<JwtService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            StringValues accessToken = context.Request.Query["access_token"];
            PathString path = context.HttpContext.Request.Path;

            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat"))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        }
    };
});

builder.Services.AddSignalR();

builder.Services.AddSingleton<OnlineUserService>();

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(frontendPolicy);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHub<ChatHub>("/chat").RequireCors(frontendPolicy);

app.Run();