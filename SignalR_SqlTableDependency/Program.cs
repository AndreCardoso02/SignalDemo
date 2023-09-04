using SignalR_SqlTableDependency.Hubs;
using SignalR_SqlTableDependency.MiddlewareExtensions;
using SignalR_SqlTableDependency.SubscribeTableDependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add services SignalR into the program
builder.Services.AddSignalR();

// DI - Dependency Injection
builder.Services.AddSingleton<DashboardHub>();
builder.Services.AddSingleton<SubscribeProductTableDependency>();
builder.Services.AddSingleton<SubscribeSaleTableDependency>();
builder.Services.AddSingleton<SubscribeCustomerTableDependency>();

var app = builder.Build();

// connection string var
var connectionString = app.Configuration.GetConnectionString("DefaultConnection");

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
// Registering Hub into app
app.MapHub<DashboardHub>("/dashboardHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

/*
 * 
 * We must call SubscribeTableDependency() here
 * We create one middleware and call SubscribeTableDependency() method in the middleware
 * 
 */

app.UseSqlTableDependency<SubscribeProductTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribeSaleTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribeCustomerTableDependency>(connectionString);

app.Run();
