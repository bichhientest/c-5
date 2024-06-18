var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".YourApp.Session"; // Sets the name of the session cookie
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Sets the idle timeout of the session (30 minutes in this case)
    options.Cookie.HttpOnly = true; // Ensures the session cookie is accessible only through HTTP
    options.Cookie.IsEssential = true; // Marks the session cookie as essential for GDPR compliance
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=ViewList}/{id?}");

app.Run();
