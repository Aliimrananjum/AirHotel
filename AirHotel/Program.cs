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


builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<HotelDbContext>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddRazorPages(); // order of adding services does not matter
builder.Services.AddSession();

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
