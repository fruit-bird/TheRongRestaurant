using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheRongRestaurant.Data;

// async void createRolesandUsers()
// {
//     var options = new DbContextOptions<ApplicationDbContext>();
//     var context = new ApplicationDbContext(options);
//     var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context), null, null, null, null);
//     var UserManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context), null, null, null, null, null, null, null, null);
//     if (!await roleManager.RoleExistsAsync("Administrator"))
//     {
//         var role = new IdentityRole("Administrator");
//         await roleManager.CreateAsync(role);
//         var user = new IdentityUser("system");
//         var chkUser = await UserManager.CreateAsync(user, "Qwerty_12345");
//         if (chkUser.Succeeded)
//         {
//             var result1 = UserManager.AddToRoleAsync(user, "Administroator");
//         }
//     }
//     if (!await roleManager.RoleExistsAsync("Manager"))
//     {
//         var role = new IdentityRole("Manager");
//         await roleManager.CreateAsync(role);
//     }
//     if (!await roleManager.RoleExistsAsync("Employee"))
//     {
//         var role = new IdentityRole("Employee");
//         await roleManager.CreateAsync(role);
//     }
// }
// createRolesandUsers();

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
    // .AddRoles<IdentityRole>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
