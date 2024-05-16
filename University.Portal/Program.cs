using Microsoft.AspNetCore.Authentication.Cookies;
using University.Shared.Interfaces.AuthInterfaces;
using University.Shared.Interfaces.CourseInterfaces;
using University.Shared.Services.AuthServices;
using University.Shared.Services.CourseServices;
using University.Shared.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// HttpClient
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddHttpClient<ICourseService, CourseService>();
builder.Services.AddHttpClient<IFacultyService, FacultyService>();

//Scopes
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IFacultyService, FacultyService>();

// Authentication
builder.Services.AddAuthentication("PortalCookie")
    .AddCookie("PortalCookie", options =>
    {
        options.Cookie.Name = "PortalAuthCookie";
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

builder.Services.AddControllersWithViews();

// API Urls
SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];
SD.CourseAPIBase = builder.Configuration["ServiceUrls:CourseAPI"];

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
