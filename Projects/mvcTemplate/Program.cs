using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using mvc.Data;
using mvc.Models;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var serverVersion = new MySqlServerVersion(new Version(11, 5, 2));
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion)
    );


builder.Services.AddIdentity<Teacher, IdentityRole>(options =>
{

    options.SignIn.RequireConfirmedAccount = false;
    options.Stores.MaxLengthForKeys = 128;
    // Password settings.
    options.Password.RequiredLength = 3;

}).AddEntityFrameworkStores<ApplicationDbContext>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
