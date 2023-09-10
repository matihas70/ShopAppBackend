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

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AngularOrigin",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .WithExposedHeaders("Myheader");
    });
});
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCors("AngularOrigin");

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseWhen(
    httpContext => httpContext.Request.Path.StartsWithSegments("/Cart"),
    subApp => subApp.UseMiddleware<CheckSessionHomeMiddleware>()
    );

app.UseWhen(
    httpContext => httpContext.Request.Path.StartsWithSegments("/Account"),
    subApp => subApp.UseMiddleware<CheckSessionLoginMiddleware>()
    );

//app.UseMiddleware<CheckSessionMiddleware>();

//app.MapControllerRoute(
//    name: "register",
//    pattern: "{controller=Account}/{action=Register}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
