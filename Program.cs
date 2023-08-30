using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.FileProviders;
using ShopApp.Entites;
using ShopApp.Interfaces;
using ShopApp.Middlewares;
using ShopApp.Services;
using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<ShopContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopDB")));
builder.Services.AddDbContextFactory<ShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShopDB")));

builder.Services.AddScoped<IUserAccount, UserAccount>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<CheckSessionMiddleware>();

app.MapControllerRoute(
    name: "register",
    pattern: "{controller=Account}/{action=Register}");

app.MapControllerRoute(
    name: "login",
    pattern: "{controller=Account}/{action=Login}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapFallbackToController("Index", "Home");
//});

app.Run();
