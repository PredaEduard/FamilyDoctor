using FamilyDoctor.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()  // Adăugăm suport pentru roluri
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

    string[] roleNames = { "Patient", "Doctor", "Admin" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Seed default admin user
    var adminUser = new IdentityUser
    {
        UserName = "admin@familydoctor.com",
        Email = "admin@familydoctor.com",
        EmailConfirmed = true
    };
    var adminPassword = "Admin123!";
    var user = await userManager.FindByEmailAsync(adminUser.Email);
    if (user == null)
    {
        var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
        if (createAdminUser.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

    // Seed default doctor users
    var doctorUsers = new List<(string UserName, string Email, string Password)>
    {
        ("doctor1@familydoctor.com", "doctor1@familydoctor.com", "Doctor123!"),
        ("doctor2@familydoctor.com", "doctor2@familydoctor.com", "Doctor123!"),
        ("doctor3@familydoctor.com", "doctor3@familydoctor.com", "Doctor123!")
    };

    foreach (var (UserName, Email, Password) in doctorUsers)
    {
        var doctorUser = new IdentityUser
        {
            UserName = UserName,
            Email = Email,
            EmailConfirmed = true
        };
        var doctor = await userManager.FindByEmailAsync(Email);
        if (doctor == null)
        {
            var createDoctorUser = await userManager.CreateAsync(doctorUser, Password);
            if (createDoctorUser.Succeeded)
            {
                await userManager.AddToRoleAsync(doctorUser, "Doctor");
            }
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
