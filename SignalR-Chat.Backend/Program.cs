using Microsoft.EntityFrameworkCore;
using SignalR_Chat.Backend.Data;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

WebApplication app = builder.Build();

app.MapControllers();

app.Run();