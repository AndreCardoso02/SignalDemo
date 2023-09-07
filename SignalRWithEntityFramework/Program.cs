using Microsoft.EntityFrameworkCore;
using SignalRWithEntityFramework.Hubs;
using SignalRWithEntityFramework.MiddlewareExtensions;
using SignalRWithEntityFramework.Models;
using SignalRWithEntityFramework.Repository;
using SignalRWithEntityFramework.SubscribeTableDependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add Signal R
builder.Services.AddSignalR();

// Adding Db Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var sqlDepConnectionString = builder.Configuration.GetConnectionString("SqlTableConnection");
builder.Services.AddDbContext<SignalRnotificationDbContext>(options =>
    options.UseSqlServer(connectionString),
    ServiceLifetime.Singleton
);

// Dependency Injection
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<NotificationHub>();
builder.Services.AddSingleton<SubscribeNotificationTableDependency>();

// Configuring Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

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

app.UseRouting();

app.UseAuthorization();

// Session
app.UseSession();

// Map Signal R with Route
app.MapHub<NotificationHub>("/notificationHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=SignIn}/{id?}");

app.UseSqlTableDependency<SubscribeNotificationTableDependency>(sqlDepConnectionString);

app.Run();
