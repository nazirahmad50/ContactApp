using API.Middleware;
using Application;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(ListContacts.Handler).Assembly));
builder.Services.AddAutoMapper(typeof(ListContacts.Handler).Assembly);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
    });
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    // Seed Data
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await SeedData.Initialize(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
