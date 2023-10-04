using Microsoft.EntityFrameworkCore;
using AirHotel.DAL;
using AirHotel.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("HotelDbContextConnection") ?? throw new InvalidOperationException("Connection string 'HotelDbContextConnection' not found.");

builder.Services.AddControllersWithViews();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<HotelDbContext>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:HotelDbContextConnection"]);
});

/*

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<HotelDbContext>();
*/

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequiredUniqueChars = 6;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
    options.Lockout.MaxFailedAccessAttempts = 5;
   options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<HotelDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddRazorPages(); // order of adding services does not matter

builder.Services.AddDistributedMemoryCache();


builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(1800); // 30 minutes
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    DBInit.Seed(app);
}

app.UseStaticFiles();

app.UseSession();
app.UseAuthorization();
app.UseAuthentication();

app.MapDefaultControllerRoute();

app.MapRazorPages();

app.Run();
