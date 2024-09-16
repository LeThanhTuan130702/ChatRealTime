using ChatRealTime.Data;
using ChatRealTime.Helper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddDbContext<ChatAppContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("ChatApp")));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    //option.Events.OnRedirectToLogin = context =>
    //{
    //    var isAdminArea = context.Request.Path.StartsWithSegments("/Admin");
    //    if (isAdminArea)
    //    {
    //        context.Response.Redirect("/Admin/account/Login");
    //    }
    //    else
    //    {
    //        context.Response.Redirect("/KhachHang/Login");
    //    }
    //    return Task.CompletedTask;
    //};

    option.LoginPath = "/User/Login";
    option.AccessDeniedPath = "/AccessDenied";
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapHub<ChatHub>("/chatHub");
app.Run();
