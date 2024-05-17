using Microsoft.AspNetCore.Authentication.Cookies;
using University.Shared.Interfaces.AuthInterfaces;
using University.Shared.Interfaces.TeacherInterfaces;
using University.Shared.Services.AuthServices;
using University.Shared.Services.TeacherServices;
using University.Shared.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// HttpClient
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IAuthService, AuthService>();

//Scopes
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<ITeacherInterface, TeacherService>();

// Authentication
builder.Services.AddAuthentication("TeacherCookie")
    .AddCookie("TeacherCookie", options =>
    {
        options.Cookie.Name = "TeacherAuthCookie";
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

// API Urls
SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];
SD.CourseAPIBase = builder.Configuration["ServiceUrls:CourseAPI"];
SD.TeacherAPIBase = builder.Configuration["ServiceUrls:TeacherAPI"];

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
