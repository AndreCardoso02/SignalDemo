using UpdateUIWithSignalR.BL;
using UpdateUIWithSignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// adding signal Hub into the services
builder.Services.AddSignalR();

// Injection of dependency
builder.Services.AddSingleton<AdminHub>();
builder.Services.AddSingleton<AdminJobs>();

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
// Mapping signal r hub class
app.MapHub<AdminHub>("/adminHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Index}/{id?}");

app.Run();
